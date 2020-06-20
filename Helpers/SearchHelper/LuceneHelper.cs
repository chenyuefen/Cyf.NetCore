using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Cn.Smart;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers.Classic;
using Lucene.Net.Search;
using Lucene.Net.Store;
using Lucene.Net.Util;
using Serilog;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Text.RegularExpressions;

namespace Helpers
{
    public class LuceneHelper : IDisposable
    {
        private readonly string _indexLocation;
        private readonly LuceneVersion _matchVersion;
        private readonly Analyzer _analyzer;
        private readonly IndexWriter _indexWriter;
        private readonly FSDirectory _fSDirectory;

        public LuceneHelper(string indexLocation, LuceneVersion matchVersion, Analyzer analyzer)
        {
            try
            {
                _indexLocation = indexLocation;
                _matchVersion = matchVersion;
                _analyzer = analyzer;
                _fSDirectory = FSDirectory.Open(_indexLocation, new NativeFSLockFactory());
                if (IndexWriter.IsLocked(_fSDirectory))
                {
                    IndexWriter.Unlock(_fSDirectory);
                }
                var indexConfig = new IndexWriterConfig(_matchVersion, _analyzer)
                {
                };
                _indexWriter = new IndexWriter(_fSDirectory, indexConfig);
            }
            catch (Lucene.Net.Store.LockObtainFailedException)
            {
                try
                {
                    _indexLocation = indexLocation + "2";
                    _matchVersion = matchVersion;
                    _analyzer = analyzer;
                    _fSDirectory?.Dispose();
                    _fSDirectory = FSDirectory.Open(_indexLocation, new NativeFSLockFactory());
                    if (IndexWriter.IsLocked(_fSDirectory))
                    {
                        IndexWriter.Unlock(_fSDirectory);
                    }
                    var indexConfig = new IndexWriterConfig(_matchVersion, _analyzer)
                    {
                    };
                    _indexWriter = new IndexWriter(_fSDirectory, indexConfig);
                }
                catch (Exception ex)
                {
                    Log.Error(ex, "索引失败2");
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "索引失败");
            }
        }

        public void CreateIndex(List<string> data)
        {
            try
            {
                if (data == null || data.Count == 0) return;
                ConcurrentQueue<Document> queue = new ConcurrentQueue<Document>();
                Parallel.ForEach(data, item =>
                {
                    Document document = new Document();
                    var kw = Format(item);
                    //document.Add(new StringField("id", kw, Field.Store.NO));
                    document.Add(new TextField("msg", kw, Field.Store.YES));
                    queue.Enqueue(document);
                });
                _indexWriter.DeleteAll();
                _indexWriter.AddDocuments(queue);
                _indexWriter.Flush(triggerMerge: false, applyAllDeletes: true);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "索引失败");
            }
        }

        /// <summary>
        /// 搜索结果,并按得分顺序从高到底返回
        /// </summary>
        public List<(string, float)> Search(string kw, int numHits = 10)
        {
            if (string.IsNullOrEmpty(kw)) return null;
            kw = Format(kw);
            var ret = new List<(string, float)>();
            try
            {
                var parser = new QueryParser(_matchVersion, "msg", new SmartChineseAnalyzer(_matchVersion));
                var query = parser.Parse(kw);

                TopScoreDocCollector collector = TopScoreDocCollector.Create(numHits, true);
                using IndexReader reader = _indexWriter.GetReader(applyAllDeletes: true);
                IndexSearcher searcher = new IndexSearcher(reader);
                searcher.Search(query, null, collector);
                ScoreDoc[] docs = collector.GetTopDocs(0, collector.TotalHits).ScoreDocs;
                for (int i = 0; i < docs.Length; i++)
                {
                    Document hitDoc = searcher.Doc(docs[i].Doc);
                    ret.Add((hitDoc.Get("msg"), docs[i].Score));
                }
            }
            catch (Lucene.Net.QueryParsers.Classic.ParseException)
            {
                //忽略含有特殊字符的字符串
            }
            catch (Exception ex)
            {
                Log.Error(ex, "索引失败");
            }
            return ret;
        }

        /// <summary>
        /// 获取相似度最高的数据
        /// </summary>
        /// <param name="kw"></param>
        /// <returns></returns>
        public (string record, float score, int similar)? SimilarSearch(string kw)
        {
            var data = Search(kw, 1);
            if (data?.Count > 0)
            {
                var similarity = GetSimilar(data[0].Item1, kw);
                return (data[0].Item1, data[0].Item2, similarity);
            }
            return null;
        }

        private string Format(string kw)
        {
            return Regex.Replace(kw ?? "", @"/|~|\*|\?|\[|\]|:", "");
            //return kw?.Replace("/", "").Replace("~", "").Replace("*", "").Replace("?", "").Replace("]", "").Replace(":", "");
        }

        public static int GetSimilar(string text1, string text2)
        {
            if (CheckSame(text1, text2, out int val))
            {
                return val;
            }

            List<char> sl1 = new List<char>(text1);
            List<char> sl2 = new List<char>(text2);

            //并集
            List<char> sl = sl1.Union(sl2).ToList<char>();

            List<int> arrA = new List<int>();
            List<int> arrB = new List<int>();
            foreach (var str in sl)
            {
                arrA.Add(sl1.Where(x => x == str).Count());
                arrB.Add(sl2.Where(x => x == str).Count());
            }
            //计算商
            double num = 0;
            //被除数
            double numA = 0;
            double numB = 0;
            for (int i = 0; i < sl.Count; i++)
            {
                num += arrA[i] * arrB[i];
                numA += Math.Pow(arrA[i], 2);
                numB += Math.Pow(arrB[i], 2);
            }
            double cos = num / (Math.Sqrt(numA) * Math.Sqrt(numB));
            var n = (int)(cos * 100);

            if (n == 100)
            {
                n -= 1;
            }
            return n;

            bool CheckSame(string text1, string text2, out int rate)
            {
                rate = 0;
                if (string.IsNullOrEmpty(text1) || string.IsNullOrEmpty(text2))
                {
                    return true;
                }
                if (text1 == text2)
                {
                    rate = 100;
                    return true;
                }
                return false;
            }
        }

        public void Dispose()
        {
            try
            {
                _indexWriter?.Dispose();
                _fSDirectory?.Dispose();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "索引失败");
            }
        }
    }
}

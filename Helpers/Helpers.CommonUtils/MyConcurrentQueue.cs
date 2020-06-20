using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Concurrent;
using System.Threading;

public class MyConcurrentStack<T> : ConcurrentStack<T> where T : class
{
    public void Clean()
    {
        while (this.TryPop(out var _))
        {
        }
    }
}

/// <summary>
/// 并行队列
/// </summary>
/// <typeparam name="T"></typeparam>
public static class ConcurrentQueueExt
{
    /// <summary>
    /// 尝试移除并返回并发队列开头处的对象
    /// </summary>
    /// <returns></returns>
    public static T Dequeue<T>(this ConcurrentQueue<T> queue)
    {
        if (queue.TryDequeue(out var result))
        {
            return result;
        }
        else
        {
            return default;
        }
    }

    /// <summary>
    /// 尝试移除并返回并发队列开头处的对象
    /// </summary>
    /// <param name="count"></param>
    /// <returns></returns>
    public static bool TryDequeue<T>(this ConcurrentQueue<T> queue, int count, out List<T> ret)
    {
        ret = new List<T>();
        for (int i = 0; i < count; i++)
        {
            if (queue.TryDequeue(out var result))
            {
                ret.Add(result);
            }
            else
            {
                break;
            }
        }
        return ret.Count > 0;
    }

    /// <summary>
    /// 将对象添加到 System.Collections.Concurrent.ConcurrentQueue`1 的结尾处
    /// </summary>
    /// <param name="items"></param>
    public static void Enqueue<T>(this ConcurrentQueue<T> queue, IEnumerable<T> items)
    {
        if (items!=null && items.Count()>0)
        {
            items.ToList().ForEach(it => queue.Enqueue(it));
        }
    }

    public static void Clean<T>(this ConcurrentQueue<T> queue)
    {
        while (queue.TryDequeue(out var _))
        {
        }
    }
}

public class MyBlockingCollection<T> : BlockingCollection<T>
{
    public MyBlockingCollection() : base()
    {
    }

    public MyBlockingCollection(int boundedCapacity) : base(boundedCapacity)
    {
    }

    public MyBlockingCollection(IProducerConsumerCollection<T> collection) : base(collection)
    {
    }

    public MyBlockingCollection(IProducerConsumerCollection<T> collection, int boundedCapacity) : base(collection, boundedCapacity)
    {
    }
}

public static class BlockingCollectionExt
{
    /// <summary>
    /// 批量添加
    /// </summary>
    /// <param name="data"></param>
    public static void Add<T>(this BlockingCollection<T> @this, List<T> data)
    {
        data.AsParallel().ForAll((it) =>
        {
            @this.Add(it);
        });
    }

    /// <summary>
    /// 尝试获取多个实体
    /// </summary>
    /// <param name="count">数量</param>
    /// <returns></returns>
    public static bool TryTake<T>(this BlockingCollection<T> @this, int count, out List<T> items)
    {
        items = Enumerable.Range(0, count)
            .Select(it => @this.TryTake(out var item) ? item : default(T))
            .Where(it => it != null)
            .ToList();
        return items.Count > 0;
    }


    /// <summary>
    /// 尝试获取多个实体
    /// </summary>
    /// <param name="count">数量</param>
    /// <returns></returns>
    public static bool TryTake<T>(this BlockingCollection<T> @this, int count, out List<T> items, TimeSpan millisecondsTimeout, CancellationToken cancellationToken)
    {
        return @this.TryTake<T>(count, out items, (int)millisecondsTimeout.TotalMilliseconds, cancellationToken);
    }

    /// <summary>
    /// 尝试获取多个实体
    /// </summary>
    /// <param name="count">数量</param>
    /// <returns></returns>
    public static bool TryTake<T>(this BlockingCollection<T> @this, int count, out List<T> items, int millisecondsTimeout, CancellationToken cancellationToken)
    {
        items = Enumerable.Range(0, count)
            .Select(it => @this.TryTake(out var item, millisecondsTimeout / count, cancellationToken) ? item : default(T))
            .Where(it => it != null)
            .ToList();
        return items.Count > 0;
    }

    public static bool TryTakeFromAny<T>(this BlockingCollection<T> @this, int count, out List<T> items, int millisecondsTimeout, CancellationToken cancellationToken, params BlockingCollection<T>[] collections)
    {
        collections = collections.Union(new BlockingCollection<T>[] { @this }).ToArray();
        items = Enumerable.Range(0, count)
            .Select(it => BlockingCollection<T>.TryTakeFromAny(collections, out var item, millisecondsTimeout / count, cancellationToken) != -1 ? item : default(T))
            .Where(it => it != null)
            .ToList();
        return items.Count > 0;
    }

    public static bool TryTakeFromAny<T>(this BlockingCollection<T> @this, out T item, int millisecondsTimeout, CancellationToken cancellationToken, params BlockingCollection<T>[] collections)
    {
        collections = collections.Union(new BlockingCollection<T>[] { @this }).ToArray();
        return BlockingCollection<T>.TryTakeFromAny(collections, out item, millisecondsTimeout, cancellationToken) != -1;
    }

    public static void Clean<T>(this BlockingCollection<T> @this)
    {
        while (@this.TryTake(out var _))
        {
        }
    }
}


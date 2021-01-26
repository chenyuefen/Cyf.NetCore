using SqlSugar;
using System.Threading.Tasks;
using TM.Core.Models.Dtos;

namespace TM.Core.Data.SqlSugar
{
    public static class QueryableExtension
    {
        /// <summary>
        /// 读取列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="isOrderBy"></param>
        /// <returns></returns>
        public static async Task<PagerList<T>> ToPageAsync<T>(this ISugarQueryable<T> query,
            int pageIndex,
            int pageSize,
            bool isOrderBy = false)
        {
            RefAsync<int> totalCount = 0;
            var page = new PagerList<T>();
            page.Data = await query.ToPageListAsync(pageIndex, pageSize, totalCount);
            var pageCount = totalCount != 0 ? (totalCount % pageSize) == 0 ? (totalCount / pageSize) : (totalCount / pageSize) + 1 : 0;
            page.Page = pageIndex;
            page.PageSize = pageSize;
            page.TotalCount = totalCount;
            page.PageCount = pageCount;
            return page;
        }

        /// <summary>
        /// 读取列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="isOrderBy"></param>
        /// <returns></returns>
        public static PagerList<T> ToPage<T>(this ISugarQueryable<T> query,
            int pageIndex,
            int pageSize,
            bool isOrderBy = false)
        {
            var page = new PagerList<T>();
            var totalCount = 0;
            page.Data = query.ToPageList(pageIndex, pageSize, ref totalCount);
            var pageCount = totalCount != 0 ? (totalCount % pageSize) == 0 ? (totalCount / pageSize) : (totalCount / pageSize) + 1 : 0;
            page.Page = pageIndex;
            page.PageSize = pageSize;
            page.TotalCount = totalCount;
            page.PageCount = pageCount;
            return page;
        }
    }
}

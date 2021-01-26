using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TM.Core.Models.Dtos;

namespace TM.Core.Data.EF.Extensions
{
    public static class QueryableExtension
    {
        /// <summary>
        /// PagedList
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="pageIndex">1为起始页</param>
        /// <param name="pageSize"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<PagerList<T>> ToPagedListAsync<T>(
            this IQueryable<T> query,
            int pageIndex,
            int pageSize,
            CancellationToken cancellationToken = default)
        {
            try
            {
                if (pageIndex < 1)
                {
                    throw new ArgumentOutOfRangeException(nameof(pageIndex));
                }

                int realIndex = pageIndex - 1;

                int count = await query.CountAsync(cancellationToken).ConfigureAwait(false);
                List<T> items = await query.Skip(realIndex * pageSize)
                    .Take(pageSize).ToListAsync(cancellationToken).ConfigureAwait(false);

                return new PagerList<T>(pageIndex, pageSize, count, items);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static PagerList<T> ToPagedList<T>(
            this IQueryable<T> query,
            int pageIndex,
            int pageSize,
            CancellationToken cancellationToken = default)
        {
            if (pageIndex < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(pageIndex));
            }
            int realIndex = pageIndex - 1;
            int count = query.Count();
            List<T> items = query.Skip(realIndex * pageSize)
                .Take(pageSize).ToList();

            return new PagerList<T>(pageIndex, pageSize, count, items);
        }
    }
}

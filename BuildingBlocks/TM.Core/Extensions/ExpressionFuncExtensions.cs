using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using TM.Infrastructure.Helpers;

namespace TM.Core.Extensions
{
    public static class ExpressionFuncExtensions
    {
        /// <summary>
        /// 以特定的条件运行组合两个Expression表达式
        /// </summary>
        /// <typeparam name="T">表达式的主实体类型</typeparam>
        /// <param name="first">第一个Expression表达式</param>
        /// <param name="second">要组合的Expression表达式</param>
        /// <param name="merge">组合条件运算方式</param>
        /// <returns>组合后的表达式</returns>
        public static Expression<T> Compose<T>(this Expression<T> first, Expression<T> second, Func<Expression, Expression, Expression> merge)
        {
            var map = first.Parameters.Select((f, i) => new { f, s = second.Parameters[i] }).ToDictionary(p => p.s, p => p.f);
            var secondBody = ParameterRebinder.ReplaceParameters(map, second.Body);
            return Expression.Lambda<T>(merge(first.Body, secondBody), first.Parameters);
        }

        /// <summary>
        /// 以 Expression.AndAlso 组合两个Expression表达式
        /// </summary>
        /// <typeparam name="T">表达式的主实体类型</typeparam>
        /// <param name="first">第一个Expression表达式</param>
        /// <param name="second">要组合的Expression表达式</param>
        /// <returns>组合后的表达式</returns>
        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
        {
            return first.Compose(second, Expression.AndAlso);
        }

        /// <summary>
        /// 以 Expression.OrElse 组合两个Expression表达式
        /// </summary>
        /// <typeparam name="T">表达式的主实体类型</typeparam>
        /// <param name="first">第一个Expression表达式</param>
        /// <param name="second">要组合的Expression表达式</param>
        /// <returns>组合后的表达式</returns>
        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
        {
            return first.Compose(second, Expression.OrElse);
        }

        private class ParameterRebinder : ExpressionVisitor
        {
            private readonly Dictionary<ParameterExpression, ParameterExpression> _map;

            private ParameterRebinder(Dictionary<ParameterExpression, ParameterExpression> map)
            {
                _map = map ?? new Dictionary<ParameterExpression, ParameterExpression>();
            }

            public static Expression ReplaceParameters(Dictionary<ParameterExpression, ParameterExpression> map,
                Expression exp)
            {
                return new ParameterRebinder(map).Visit(exp);
            }

            protected override Expression VisitParameter(ParameterExpression node)
            {
                ParameterExpression replacement;
                if (_map.TryGetValue(node, out replacement))
                    node = replacement;
                return base.VisitParameter(node);
            }
        }

        #region 自定义排序
        public static IQueryable<TSource> OrderExp<TSource>(this IQueryable<TSource> source, string sortName, int sortway)
        {
            if (Regexs.IsNull(sortName)) return source;

            //sortName 首字母大小写转换
            #region 首字母大小写转换
            char[] sortNameChar = sortName.ToCharArray();
            char firstLetter = sortNameChar[0];
            if ('a' <= firstLetter && firstLetter <= 'z')
            {
                firstLetter = (char)(firstLetter & ~0x20);
                sortNameChar[0] = firstLetter;
                sortName = new string(sortNameChar);
            }
            #endregion

            var orderWay = sortway == 0 ? "OrderBy" : "OrderByDescending";
            Type type = typeof(TSource);
            var property = type.GetProperty(sortName);
            if (property == null) return source;
            var parmarer = Expression.Parameter(type);
            var propertyAccess = Expression.MakeMemberAccess(parmarer, property);
            var orderByExpression = Expression.Lambda(propertyAccess, parmarer);
            var resultExpression = Expression.Call(typeof(Queryable), orderWay, new Type[] { type, property.PropertyType }, source.Expression, Expression.Quote(orderByExpression));

            return source.Provider.CreateQuery<TSource>(resultExpression);

        }
        #endregion
    }
}

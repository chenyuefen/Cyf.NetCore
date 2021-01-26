/*----------------------------------------------------------------
// Copyright (C) 2020 广州天美联盟网络科技有限公司
// 版权所有。
//
// 文件名：Mapper
// 文件功能描述： 表达式树对象克隆
// 注:对比AutoMapper 该帮助类第一次执行映射，第二次会缓存在服务器，性能更好
//
// 创建者：冼晓松
// 创建时间：2020-03-17 09:30
// 
//----------------------------------------------------------------*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;
using System.Reflection;

namespace TM.Infrastructure.Expressions
{
    /// <summary>
    /// 表达式树对象克隆
    /// </summary>
    /// <typeparam name="TSource">源对象</typeparam>
    /// <typeparam name="TTarget">目标对象</typeparam>
    public static class Mapper<TSource, TTarget>
    {
        /// <summary>
        /// 静态泛型类缓存泛型委托
        /// 调用一次之后，同一个映射关系时再调用时会用缓存里面的泛型委托执行
        /// </summary>
        private static readonly Func<TSource, TTarget> MapFunc = GetMapFunc();

        /// <summary>
        /// 将对象TSource的值赋给TTarget
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static TTarget Map(TSource source)
        {
            if (source == null)
                return default(TTarget);
            return MapFunc(source);
        }

        /// <summary>
        /// 将IEnumerable<TSource>的值赋给List<TTarget>
        /// MapFunc是静态泛型委托，速度很快，循环映射也不太影响性能
        /// </summary>
        /// <param name="sources"></param>
        /// <returns></returns>
        public static List<TTarget> MapList(IEnumerable<TSource> sources)
        {
            if (sources == null)
                return default(List<TTarget>);
            List<TTarget> result = new List<TTarget>();
            foreach (TSource item in sources)
            {
                TTarget data = MapFunc(item);
                result.Add(data);
            }
            return result;
        }

        /// <summary>
        /// 表达式树(Expression)动态创建lambda表达式
        /// 类似:lambda表达式的Select方法  list.Select(item => new Model { ID = item.ID, Name = item.Name }).ToList(); 这里的Model是泛型
        /// 注:  int ID => int? ID   转换不了 类型不一样   暂时没有解决办法
        ///      int ID => string ID 转换不了 类型不一样
        ///      int ID => int id    转换不了 名称不一样
        /// </summary>
        /// <returns></returns>
        private static Func<TSource, TTarget> GetMapFunc()
        {
            ParameterExpression sourceParameter = Expression.Parameter(typeof(TSource), "item"); //创建表达式树的参数,作为节点 item，类型为源对象
            List<MemberBinding> memberBindingList = new List<MemberBinding>();

            foreach (var item in typeof(TTarget).GetProperties())
            {
                if (!item.PropertyType.IsPublic || !item.CanWrite)
                    continue;
                var sourceItem = typeof(TSource).GetProperty(item.Name);
                if (sourceItem == null || !sourceItem.CanRead || sourceItem.PropertyType.IsNotPublic)
                    continue;
                if (sourceItem.GetCustomAttribute<NotMappedAttribute>() != null)
                    continue;

                MemberExpression property = Expression.Property(sourceParameter, sourceItem); //构造属性表达式 item.ID
                //当ID不是变量且源类型和目标类型不一致
                if (!sourceItem.PropertyType.IsValueType && sourceItem.PropertyType != item.PropertyType)
                {
                    if (sourceItem.PropertyType.IsClass && item.PropertyType.IsClass &&
                        !sourceItem.PropertyType.IsGenericType && !item.PropertyType.IsGenericType)
                    {
                        Expression expression = GetClassExpression(property, sourceItem.PropertyType, item.PropertyType);
                        MemberBinding memberBinding = Expression.Bind(item, expression);  //将源对象的属性绑定到目标对象属性上，ID = item.ID
                        memberBindingList.Add(memberBinding);   //添加表达式到List
                    }
                    if (typeof(IEnumerable).IsAssignableFrom(sourceItem.PropertyType) && typeof(IEnumerable).IsAssignableFrom(item.PropertyType))
                    {
                        if (sourceItem.PropertyType.IsArray == false && (sourceItem.PropertyType.GetGenericArguments() == null || sourceItem.PropertyType.GetGenericArguments().Length <= 0))
                            continue;
                        if (item.PropertyType.IsArray == false && (item.PropertyType.GetGenericArguments() == null || item.PropertyType.GetGenericArguments().Length <= 0))
                            continue;
                        Expression expression = GetListExpression(property, sourceItem.PropertyType, item.PropertyType);
                        memberBindingList.Add(Expression.Bind(item, expression));
                    }
                    continue;
                }
                if (item.PropertyType != sourceItem.PropertyType)
                    continue;
                memberBindingList.Add(Expression.Bind(item, property));
            }
            MemberInitExpression memberInitExpression = Expression.MemberInit(Expression.New(typeof(TTarget)), memberBindingList.ToArray());//调用构造函数并初始化新对象(反射得到的泛型目标对象)的一个或多个成员(List)：item => new Model { ID = item.ID, ... }
            Expression<Func<TSource, TTarget>> lambda = Expression.Lambda<Func<TSource, TTarget>>(memberInitExpression, new ParameterExpression[] { sourceParameter }); //创建一个表示lambda表达式的表达式树
            //编译生成委托并返回执行结果
            return lambda.Compile();
        }

        /// <summary>
        /// 类型是对象时赋值
        /// 处理复杂类型，对象里面还有对象
        /// </summary>
        /// <param name="sourceProperty"></param>
        /// <param name="sourceType"></param>
        /// <param name="targetType"></param>
        /// <returns></returns>
        private static Expression GetClassExpression(Expression sourceProperty, Type sourceType, Type targetType)
        {
            BinaryExpression binaryExpression = Expression.NotEqual(sourceProperty, Expression.Constant(null, sourceType));

            //构造回调 Mapper<TSource, TTarget>.Map()
            Type mapperType = typeof(Mapper<,>).MakeGenericType(sourceType, targetType);
            MethodCallExpression callExpression = Expression.Call(mapperType.GetMethod(nameof(Map), new[] { sourceType }), sourceProperty);
            return Expression.Condition(binaryExpression, callExpression, Expression.Constant(null, targetType));
        }

        /// <summary>
        /// 类型为对象集合或数组时赋值
        /// 处理复杂类型，对象里面还有对象集合或数组等
        /// </summary>
        /// <param name="sourceProperty"></param>
        /// <param name="sourceType"></param>
        /// <param name="targetType"></param>
        /// <returns></returns>
        private static Expression GetListExpression(Expression sourceProperty, Type sourceType, Type targetType)
        {
            //条件item.ID != null
            BinaryExpression binaryExpression = Expression.NotEqual(sourceProperty, Expression.Constant(null, sourceType));

            //构造回调 Mapper<TSource, TTarget>.MapList()
            Type sourceArg = sourceType.IsArray ? sourceType.GetElementType() : sourceType.GetGenericArguments()[0];
            Type targetArg = targetType.IsArray ? targetType.GetElementType() : targetType.GetGenericArguments()[0];
            Type mapperType = typeof(Mapper<,>).MakeGenericType(sourceArg, targetArg);

            MethodCallExpression mapperExpression = Expression.Call(mapperType.GetMethod(nameof(MapList), new[] { sourceType }), sourceProperty);

            Expression expression;
            if (targetType == mapperExpression.Type)
            {
                expression = mapperExpression;
            }
            else if (targetType.IsArray)//数组类型调用ToArray()方法
            {
                expression = Expression.Call(mapperExpression, mapperExpression.Type.GetMethod("ToArray"));
            }
            else if (typeof(IDictionary).IsAssignableFrom(targetType))
            {
                expression = Expression.Constant(null, targetType);//字典类型不转换
            }
            else
            {
                expression = Expression.Convert(mapperExpression, targetType);
            }
            return Expression.Condition(binaryExpression, expression, Expression.Constant(null, targetType));
        }
    }
}

using AspectCore.DynamicProxy.Parameters;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using TM.Core.Models;

namespace TM.Core.Helpers
{
    /// <summary>
    /// Some helper methods for entities.
    /// </summary>
    public static class EntityHelper
    {
        public static bool IsEntity([NotNull] Type type)
        {
            return typeof(IEntity).IsAssignableFrom(type);
        }

        public static bool HasDefaultId<TKey>(IEntity<TKey> entity)
               where TKey : IEquatable<TKey>
        {
            if (EqualityComparer<TKey>.Default.Equals(entity.Id, default))
            {
                return true;
            }

            //Workaround for EF Core since it sets int/long to min value when attaching to dbcontext
            if (typeof(TKey) == typeof(int))
            {
                return Convert.ToInt32(entity.Id) <= 0;
            }

            if (typeof(TKey) == typeof(long))
            {
                return Convert.ToInt64(entity.Id) <= 0;
            }

            return false;
        }

        /// <summary>
        /// Tries to find the primary key type of the given entity type.
        /// May return null if given type does not implement <see cref="IEntity{TKey}"/>
        /// </summary>
        public static Type FindPrimaryKeyType<TEntity>()
            where TEntity : IEntity
        {
            return FindPrimaryKeyType(typeof(TEntity));
        }

        /// <summary>
        /// Tries to find the primary key type of the given entity type.
        /// May return null if given type does not implement <see cref="IEntity{TKey}"/>
        /// </summary>
        public static Type FindPrimaryKeyType([NotNull] Type entityType)
        {
            if (!typeof(IEntity).IsAssignableFrom(entityType))
            {
                throw new Exception($"Given {nameof(entityType)} is not an entity. It should implement {typeof(IEntity).AssemblyQualifiedName}!");
            }

            foreach (var interfaceType in entityType.GetTypeInfo().GetInterfaces())
            {
                if (interfaceType.GetTypeInfo().IsGenericType && interfaceType.GetGenericTypeDefinition() == typeof(IEntity<>))
                {
                    return interfaceType.GenericTypeArguments[0];
                }
            }

            return null;
        }

        public static Expression<Func<TEntity, bool>> CreateEqualityExpressionForId<TEntity, TKey>(TKey id)
            where TEntity : IEntity<TKey>
            where TKey : IEquatable<TKey>
        {
            var lambdaParam = Expression.Parameter(typeof(TEntity));
            var leftExpression = Expression.PropertyOrField(lambdaParam, "Id");
            var idValue = Convert.ChangeType(id, typeof(TKey));
            Expression<Func<object>> closure = () => idValue;
            var rightExpression = Expression.Convert(closure.Body, leftExpression.Type);
            var lambdaBody = Expression.Equal(leftExpression, rightExpression);
            return Expression.Lambda<Func<TEntity, bool>>(lambdaBody, lambdaParam);
        }

        //public static void TrySetId<TKey>(
        //    IEntity<TKey> entity,
        //    Func<TKey> idFactory,
        //    bool checkForDisableGuidGenerationAttribute = false)
        //       where TKey : IEquatable<TKey>
        //{
        //    //TODO: Can be optimized (by caching per entity type)?
        //    var entityType = entity.GetType();
        //    var idProperty = entityType.GetProperty(
        //        nameof(entity.Id)
        //    );

        //    if (idProperty == null)
        //    {
        //        return;
        //    }

        //    if (checkForDisableGuidGenerationAttribute)
        //    {
        //        if (idProperty.IsDefined(typeof(DisableIdGenerationAttribute), true))
        //        {
        //            return;
        //        }
        //    }

        //    idProperty.SetValue(entity, idFactory());
        //}
    }
}

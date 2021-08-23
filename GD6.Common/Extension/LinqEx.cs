using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace GD6.Common
{
    public static class LinqEx
    {
        public static IOrderedQueryable<T> OrderBy<T>(
    this IQueryable<T> source,
    string property,
    string dir)
        {
            return ApplyOrder<T>(source, property, dir);
        }

        //public static IOrderedQueryable<T> OrderByDescending<T>(
        //    this IQueryable<T> source,
        //    string property)
        //{
        //    return ApplyOrder<T>(source, property, "OrderByDescending");
        //}

        public static IOrderedQueryable<T> ThenBy<T>(
            this IOrderedQueryable<T> source,
            string property)
        {
            return ApplyOrder<T>(source, property, "ThenBy");
        }

        public static IOrderedQueryable<T> ThenByDescending<T>(
            this IOrderedQueryable<T> source,
            string property)
        {
            return ApplyOrder<T>(source, property, "ThenByDescending");
        }

        static IOrderedQueryable<T> ApplyOrder<T>(
            IQueryable<T> source,
            string property,
            string methodName)
        {
            if (!string.IsNullOrEmpty(methodName) && methodName.ToUpper().Trim() == "DESC")
                methodName = "OrderByDescending";
            else
                methodName = "OrderBy";

            string[] props = property.Split('.');
            Type type = typeof(T);
            ParameterExpression arg = Expression.Parameter(type, "x");
            Expression expr = arg;
            foreach (string prop in props)
            {
                // use reflection (not ComponentModel) to mirror LINQ
                PropertyInfo pi = type.GetProperty(prop, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                if (pi != null)
                {
                    expr = Expression.Property(expr, pi);
                    type = pi.PropertyType;
                }
            }
            Type delegateType = typeof(Func<,>).MakeGenericType(typeof(T), type);
            LambdaExpression lambda = Expression.Lambda(delegateType, expr, arg);

            object result = typeof(Queryable).GetMethods().Single(
                    method => method.Name == methodName
                            && method.IsGenericMethodDefinition
                            && method.GetGenericArguments().Length == 2
                            && method.GetParameters().Length == 2)
                    .MakeGenericMethod(typeof(T), type)
                    .Invoke(null, new object[] { source, lambda });
            return (IOrderedQueryable<T>)result;
        }
        //public static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> source, string propertyName)
        //{
        //    System.Reflection.PropertyInfo prop = typeof(T).GetProperty(propertyName);

        //    return source.OrderBy(x => prop.GetValue(x));

        //    //public static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> source, string propertyName)
        //    //{
        //    //    return source.OrderBy(ToLambda<T>(propertyName));
        //    //}

        //    //return source.OrderBy(ToLambda<T>(propertyName));
        //}

        public static IEnumerable<TResult> LeftOuterJoin<TOuter, TInner, TKey, TResult>(
            this IQueryable<TOuter> outer,
            IQueryable<TInner> inner,
            Func<TOuter, TKey> outerKeySelector,
            Func<TInner, TKey> innerKeySelector,
            Func<TOuter, TInner, TResult> resultSelector)
        {
            return outer
                .GroupJoin(inner, outerKeySelector, innerKeySelector, (a, b) => new
                {
                    a,
                    b
                })
                .SelectMany(x => x.b.DefaultIfEmpty(), (x, b) => resultSelector(x.a, b));
        }

        public static IEnumerable<TResult> LeftOuterJoin<TOuter, TInner, TKey, TResult>(
        this IEnumerable<TOuter> outer,
        IEnumerable<TInner> inner,
        Func<TOuter, TKey> outerKeySelector,
        Func<TInner, TKey> innerKeySelector,
        Func<TOuter, TInner, TResult> resultSelector)
        {
            return outer
                .GroupJoin(inner, outerKeySelector, innerKeySelector, (a, b) => new
                {
                    a,
                    b
                })
                .SelectMany(x => x.b.DefaultIfEmpty(), (x, b) => resultSelector(x.a, b));
        }
    }

    //public static class LinqExQueryable
    //{
    //    public static IEnumerable<TResult> LeftOuterJoin<TOuter, TInner, TKey, TResult>(
    //        this IQueryable<TOuter> outer,
    //        IQueryable<TInner> inner,
    //        Func<TOuter, TKey> outerKeySelector,
    //        Func<TInner, TKey> innerKeySelector,
    //        Func<TOuter, TInner, TResult> resultSelector)
    //    {
    //        return outer.GroupJoin(inner, outerKeySelector, innerKeySelector, (a,b)=> new
    //        {
    //            a,
    //            b
    //        })
    //            //.GroupJoin(inner, outerKeySelector, innerKeySelector, (a, b) => new
    //            //{
    //            //    a,
    //            //    b
    //            //})
    //            //.SelectMany(x => x.b.DefaultIfEmpty(), (x, b) => resultSelector(x.a, b));
    //    }
    //}
}

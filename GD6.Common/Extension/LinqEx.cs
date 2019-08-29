using System;
using System.Collections.Generic;
using System.Linq;

namespace GD6.Common
{
    public static class LinqEx
    {
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

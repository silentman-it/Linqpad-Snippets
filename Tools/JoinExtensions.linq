<Query Kind="Program" />

void Main()
{
	int?[] a = { 1, 2, 3, 4};
	int?[] b = { 3, 4, 5, 6};
	int?[] c = { 3, 5, 6};
	
	a
		.LeftOuterJoin(b, o => o, i => i, (o, i) => new { A = o, B = i })
		//.LeftJoin(c, o => o.A, i => i, (o, i) => new { A = o.A, B = o.B, C = i })
		.Dump();
	
}

// Define other methods and classes here
public static class JoinExtensions
{
        public static IEnumerable<TResult> LeftOuterJoin<TSource, TInner, TKey, TResult>(this IEnumerable<TSource> source,
                                                                                         IEnumerable<TInner> inner,
                                                                                         Func<TSource, TKey> pk,
                                                                                         Func<TInner, TKey> fk,
                                                                                         Func<TSource, TInner, TResult> result)
        {
            IEnumerable<TResult> _result = Enumerable.Empty<TResult>();

            _result = from s in source
                      join i in inner
                      on pk(s) equals fk(i) into joinData
                      from left in joinData.DefaultIfEmpty()
                      select result(s, left);

            return _result;
        }


        public static IEnumerable<TResult> RightOuterJoin<TSource, TInner, TKey, TResult>(this IEnumerable<TSource> source,
                                                                                         IEnumerable<TInner> inner,
                                                                                         Func<TSource, TKey> pk,
                                                                                         Func<TInner, TKey> fk,
                                                                                         Func<TSource, TInner, TResult> result)
        {
            IEnumerable<TResult> _result = Enumerable.Empty<TResult>();

            _result = from i in inner
                      join s in source
                      on fk(i) equals pk(s) into joinData
                      from right in joinData.DefaultIfEmpty()
                      select result(right, i);

            return _result;
        }


        public static IEnumerable<TResult> FullOuterJoin<TSource, TInner, TKey, TResult>(this IEnumerable<TSource> source,
                                                                                         IEnumerable<TInner> inner,
                                                                                         Func<TSource, TKey> pk,
                                                                                         Func<TInner, TKey> fk,
                                                                                         Func<TSource, TInner, TResult> result)
        {

            var left = source.LeftOuterJoin(inner, pk, fk, result).ToList();
            var right = source.RightOuterJoin(inner, pk, fk, result).ToList();

            return left.Union(right);


        }


        public static IEnumerable<TResult> LeftExcludingJoin<TSource, TInner, TKey, TResult>(this IEnumerable<TSource> source,
                                                                                         IEnumerable<TInner> inner,
                                                                                         Func<TSource, TKey> pk,
                                                                                         Func<TInner, TKey> fk,
                                                                                         Func<TSource, TInner, TResult> result)
        {
            IEnumerable<TResult> _result = Enumerable.Empty<TResult>();

            _result = from s in source
                      join i in inner
                      on pk(s) equals fk(i) into joinData
                      from left in joinData.DefaultIfEmpty()
                      where left == null
                      select result(s, left);

            return _result;
        }

        public static IEnumerable<TResult> RightExcludingJoin<TSource, TInner, TKey, TResult>(this IEnumerable<TSource> source,
                                                                                 IEnumerable<TInner> inner,
                                                                                 Func<TSource, TKey> pk,
                                                                                 Func<TInner, TKey> fk,
                                                                                 Func<TSource, TInner, TResult> result)
        {
            IEnumerable<TResult> _result = Enumerable.Empty<TResult>();

            _result = from i in inner
                      join s in source
                      on fk(i) equals pk(s) into joinData
                      from right in joinData.DefaultIfEmpty()
                      where right == null
                      select result(right, i);

            return _result;
        }


        public static IEnumerable<TResult> FullExcludingJoin<TSource, TInner, TKey, TResult>(this IEnumerable<TSource> source,
                                                                                    IEnumerable<TInner> inner,
                                                                                    Func<TSource, TKey> pk,
                                                                                    Func<TInner, TKey> fk,
                                                                                    Func<TSource, TInner, TResult> result)
        {
            var left = source.LeftExcludingJoin(inner, pk, fk, result).ToList();
            var right = source.RightExcludingJoin(inner, pk, fk, result).ToList();

            return left.Union(right);
        }

}


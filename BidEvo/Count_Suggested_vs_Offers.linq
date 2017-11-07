<Query Kind="Program">
  <Connection>
    <ID>a4da0b26-e220-492d-a3ae-4b29358701ef</ID>
    <Persist>true</Persist>
    <Server>172.19.122.63\,3433</Server>
    <SqlSecurity>true</SqlSecurity>
    <UserName>sa</UserName>
    <Password>AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAAvm6QbRxlJUuLCV01lsuo5AAAAAACAAAAAAAQZgAAAAEAACAAAABCZX2NEJwBBxkMXKe3E6O8nnZCKWTREELMs7QORETUmQAAAAAOgAAAAAIAACAAAACnWDa1IoHM8yW+QOJTbPTIJ6KBEZGaGMuiRljo/BHB3BAAAACjSL7Cv48d/LckZxgcQjtJQAAAAK7cjXK/2lUepF0NlbJVxfFOPO52z69ENMR99XvafjoOrKt5x2io/dvDOEHjzmB/Qe1BHgF5Ux/lGJ26GwyzvKQ=</Password>
    <Database>BIDEVO_DEV</Database>
    <ShowServer>true</ShowServer>
  </Connection>
</Query>

void Main()
{
	var now = DateTime.Now;
	//OffersSuggesteds.GroupBy(x => new { Unit = x.Unit.UnitId, FlowDate = x.FlowDate, Market = x.MarketId }).Select(x => new { x.Key.Unit, x.Key.FlowDate, x.Key.Market, Count = x.Count() }).Dump();
	//Offers.GroupBy(x => new { Unit = x.Unit.UnitId, FlowDate = x.FlowDate, Market = x.MarketId }).Select(x => new { x.Key.Unit, x.Key.FlowDate, x.Key.Market, Count = x.Count() }).Dump();
	
	OffersSuggesteds
		.GroupBy(x => new { Unit = x.Unit.UnitId, FlowDate = x.FlowDate.Value, Market = x.MarketId })
		.Select(x => new { x.Key.Unit, x.Key.FlowDate, x.Key.Market, Count = x.Count() })
		.LeftOuterJoin(Offers
				.GroupBy(x => new { Unit = x.Unit.UnitId, FlowDate = x.FlowDate, Market = x.MarketId })
				.Select(x => new { x.Key.Unit, x.Key.FlowDate, x.Key.Market, Count = x.Count() }),
				o => new { o.Unit, o.FlowDate, o.Market },
				i => new { i.Unit, i.FlowDate, i.Market },
				(o, i) => new {
					o.Unit,
					o.FlowDate,
					o.Market,
					SuggOffers_Count = o.Count,
					Offers_Count = i != null? i.Count : 0
				})
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
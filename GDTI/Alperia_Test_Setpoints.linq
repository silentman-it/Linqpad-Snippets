<Query Kind="Program">
  <Output>DataGrids</Output>
  <Reference>C:\Work\TMS\DEV\TMS\Common\TMS.Core\bin\x64\Debug\TMS.Core.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.configuration.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Security.dll</Reference>
  <Namespace>TMS.Core.Data.Simple</Namespace>
  <Namespace>System.Configuration</Namespace>
</Query>

void Main()
{
	//AppDomain.CurrentDomain.SetupInformation.ConfigurationFile.Dump();
	SetConnString("svil4");

	var sd = SerieData.Instance.GetSerieData("GME", "UP_PROVAORARIA_3", "UnitSchedule", null, "MI5", DateTime.Parse("01/08/2016"));
	sd.Dump();
	
	var sp = sd.First().Data.ToDictionary(k => k.Key.TimeOfDay, v => Convert.ToDecimal(v.Value)).Dump("Plan").GetSetPoints().Dump("SetPoints");

}

// Define other methods and classes here
void SetConnString(string db)
{
	// Trick per modificare la lista delle connstring
	// http://stackoverflow.com/questions/360024/how-do-i-set-a-connection-string-config-programatically-in-net
	typeof(ConfigurationElementCollection).GetField("bReadOnly", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(ConfigurationManager.ConnectionStrings, false);

	ConfigurationManager.ConnectionStrings.Add(new ConnectionStringSettings("Eu",  string.Format("data source={0};User ID=ed_framework;Password=ed_framework", db)));
	ConfigurationManager.ConnectionStrings.Add(new ConnectionStringSettings("TMS", string.Format("data source={0};User ID=ed_tms;Password=ed_tms", db)));

}


    internal static class MyExtensions
    {
		public static Dictionary<TimeSpan, decimal> EnsureMinuteGranularity(this Dictionary<TimeSpan, decimal> inputCollection, TimeSpan tsMax)
        {
            inputCollection = inputCollection.EnsureMinuteGranularity();
			
            var startTs = inputCollection.Max(x => x.Key);
            var startVal = inputCollection[startTs];

            var missingTrailingPts = Enumerable
                        .Range((int)startTs.TotalMinutes + 1, (int)(tsMax.TotalMinutes - startTs.TotalMinutes) - 1)
                        .Select(x => new KeyValuePair<TimeSpan, decimal>(TimeSpan.FromMinutes(x), startVal));

            var filledCollection = inputCollection
				.ToList()
				.Union(missingTrailingPts)
				.OrderBy(x => x.Key)
				.ToDictionary(k => k.Key, v => v.Value);
				
			return filledCollection;
        }

		public static Dictionary<TimeSpan, decimal> EnsureMinuteGranularity(this Dictionary<TimeSpan, decimal> inputCollection)
		{
			var one = TimeSpan.FromMinutes(1);
            var breaks = new Dictionary<TimeSpan, decimal>();
            var missing = new List<KeyValuePair<TimeSpan, decimal>>();

            inputCollection.Aggregate((seed, aggr) =>
            {
                var diff = (aggr.Key - seed.Key) - one;

                if (diff > TimeSpan.Zero)
                {
                    breaks.Add(seed.Key, seed.Value);
                    var miss = Enumerable
                        .Range((int)(aggr.Key.TotalMinutes - diff.TotalMinutes), (int)(diff.TotalMinutes))
                        .Select(x => new KeyValuePair<TimeSpan, decimal>(TimeSpan.FromMinutes(x), seed.Value));
                    missing.AddRange(miss);
                }
                return aggr;
            });
			
			var filledCollection = inputCollection
				.ToList()
				.Union(missing)
				.OrderBy(x => x.Key)
				.ToDictionary(k => k.Key, v => v.Value);

            return filledCollection;
		}
		
        public static Dictionary<TimeSpan, decimal> GetSetPoints(this Dictionary<TimeSpan, decimal> inputCollection)
        {
            // Ensure proper order
            var pc = inputCollection.EnsureMinuteGranularity(TimeSpan.FromDays(1)).OrderBy(x => x.Key);

			var discontinuityPoints =
                pc.Skip(1)
                // Zip current with previous
                .Zip(pc, (c, p) => new { Prev_Key = p.Key, Prev_Val = p.Value, Curr_Key = c.Key, Curr_Val = c.Value })
                // Then zip with next
                .Zip(pc.Skip(2), (cp, n) => new { Prev_Key = cp.Prev_Key, Prev_Val = cp.Prev_Val, Curr_Key = cp.Curr_Key, Curr_Val = cp.Curr_Val, Next_Key = n.Key, Next_Val = n.Value })
                // Filter where the difference Curr-Prev is different than Next-Curr (curr is a vertex)
                .Where(x => x.Curr_Val - x.Prev_Val != x.Next_Val - x.Curr_Val)
                .Select(x => new { Key = x.Curr_Key, Value = x.Curr_Val })
                .ToList();


            // Add first & last
            if (discontinuityPoints.First().Key != pc.First().Key)
                discontinuityPoints.Insert(0, new { Key = pc.First().Key, Value = pc.First().Value });

            if (discontinuityPoints.Last().Key != pc.Last().Key)
                discontinuityPoints.Add(new { Key = pc.Last().Key, Value = pc.Last().Value });
				
			// Richiesta Tirreno Power: Eliminare i punti di partenza delle rampe
			if (true)
			{
				discontinuityPoints.Dump().RemoveRampStartingPoints(x => x.Value).Dump();
			}

            var o = discontinuityPoints.ToDictionary(k => k.Key, v => v.Value);

            return o;
        }


        public static string DumpAsString(this Dictionary<TimeSpan, decimal> pointCollection)
        {
            var s = string.Join(" / ", pointCollection.Select(x => string.Format("{0}={1}", x.Key.ToString(@"hh\:mm"), x.Value.ToString())));
            return s;
        }
		
		public static IEnumerable<T> RemoveRampStartingPoints<T>(this IEnumerable<T> input, Func<T, object> selector)
		{
			bool isFirst = true;
			T last = default(T);
			foreach (var item in input)
			{
				if (isFirst || !object.Equals(selector(item), selector(last)))
				{
					yield return item;
					last = item;
					isFirst = false;
				}
			}
		}
    }
<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.configuration.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Security.dll</Reference>
  <Reference>C:\Work\TMS\DEV\TMS\Common\TMS.Core\bin\x64\Debug\TMS.Common.dll</Reference>
  <Reference>C:\Work\TMS\DEV\TMS\Common\TMS.Core\bin\x64\Debug\TMS.Core.dll</Reference>
  <Namespace>System.Configuration</Namespace>
  <Namespace>TMS.Common.Extensions</Namespace>
  <Namespace>TMS.Core.Data.Simple</Namespace>
  <Namespace>System.Globalization</Namespace>
</Query>

void Main()
{
	SetConnString("win2008r2test1");
	

	DateTime flowDate = DateTime.Parse("19/07/2017");
	var sd = SerieData.Instance.GetSerieData("GME", "UP_TURBIGO_4", "BindingProgram", "PV", null, flowDate);
	sd = sd.Where(x => x.ExtendedData.Any());

	Stopwatch sw = Stopwatch.StartNew();

	var dic = sd.SelectMany(s => s.ExtendedData.Select(ed => new {
		Unit = s.Serie.Symbol,
		Market = s.Serie.Context,
		TS = ed.StartDate,
		Val = ed.DoubleValue
	}))
	.GroupBy(x => x.TS)
	.Select(x => new {
		Key = x.Key,
		MSD1 = x.TrySingleOrDefault(s => s.Market == "MSD1", s => s.Val),
		MSD2 = x.TrySingleOrDefault(s => s.Market == "MSD2", s => s.Val),
		MSD3 = x.TrySingleOrDefault(s => s.Market == "MSD3", s => s.Val),
		MSD4 = x.TrySingleOrDefault(s => s.Market == "MSD4", s => s.Val),
		MSD5 = x.TrySingleOrDefault(s => s.Market == "MSD5", s => s.Val),
		MSD6 = x.TrySingleOrDefault(s => s.Market == "MSD6", s => s.Val)
	})
	.Select(x => new {
		Key = x.Key,
		Value = x.MSD6 ?? x.MSD5 ?? x.MSD4 ?? x.MSD3 ?? x.MSD2 ?? x.MSD1 ?? null
	})	
	.RightOuterJoin(flowDate.GetAllMinutes(), o => o.Key, i => i, (o,i) => new {
		Key = i,
		Value = o != null ? o.Value : (double?)null
	})
	.Checkpoint(sw, "All")
	.Dump()
;
}

public static class IEnumerableExtensions
{
	public static TOutput TrySingleOrDefault<T, TOutput>(this IEnumerable<T> o, Func<T, bool> predicate, Func<T,TOutput> selector)
	{
		T item = o.SingleOrDefault(predicate);
		if(item == null)
			return default(TOutput);
		
		return selector.Invoke(item);
	}
}

public static class CheckpointExtension
{
	public static T Checkpoint<T>(this T o, Stopwatch sw, string msg = null)
	{
		var elapsed = sw.Elapsed;
		msg = msg ?? "<null>";
		Console.WriteLine ("[Checkpoint] {0} = {1}", msg, elapsed.ToString());
		sw.Restart();
		return o;
	}
}

void SetConnString(string db)
{
	// Trick per modificare la lista delle connstring
	// http://stackoverflow.com/questions/360024/how-do-i-set-a-connection-string-config-programatically-in-net
	typeof(ConfigurationElementCollection).GetField("bReadOnly", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(ConfigurationManager.ConnectionStrings, false);

	ConfigurationManager.ConnectionStrings.Add(new ConnectionStringSettings("Eu",  string.Format("data source={0};User ID=ed_framework;Password=ed_framework", db)));
	ConfigurationManager.ConnectionStrings.Add(new ConnectionStringSettings("TMS", string.Format("data source={0};User ID=ed_tms;Password=ed_tms", db)));

}


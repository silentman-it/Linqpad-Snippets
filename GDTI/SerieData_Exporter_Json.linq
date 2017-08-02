<Query Kind="Program">
  <Reference>C:\Work\BidEvolution\Phase_Zero\packages\Newtonsoft.Json.10.0.2\lib\net40\Newtonsoft.Json.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.configuration.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Security.dll</Reference>
  <Reference>C:\Work\TMS\DEV\TMS\Common\TMS.Core\bin\x64\Debug\TMS.Core.dll</Reference>
  <Namespace>Newtonsoft.Json</Namespace>
  <Namespace>System.Configuration</Namespace>
  <Namespace>TMS.Core.Data.Simple</Namespace>
</Query>

void Main()
{
	SetConnString("gdti_tp");
	
	string src, sym, cls, chn, ctx;
	
	src = "DCS";
	sym = null;
	cls = "8CBA4AAD-4B1E-45A1-B0B4-E3DA29509B19";
	chn = null;
	ctx = null;
	
	DateTime d1 = DateTime.Parse("01/02/2017");
	DateTime d2 = DateTime.Parse("04/02/2017");

	SaveDump(src, sym, cls, chn, ctx, d1, d2);
}

void SaveDump(string src, string sym, string cls, string chn, string ctx, DateTime d1, DateTime d2)
{
	var sd = SerieData.Instance.GetSerieData(src, sym, cls, chn, ctx, d1, d2);
	sd.Where(x => x.ExtendedData.Any()).Dump();
	
	//var buffer = BinarySerialization.Serialize(sd);
	var buffer = JsonConvert.SerializeObject(sd, Newtonsoft.Json.Formatting.None);
	
	var fileName = string.Format("SerieData_Dump_{0}_{1}_{2}_{3}_{4}_{5}_{6}_{7}.json",
		src ?? "null",
		sym ?? "null",
		cls ?? "null",
		chn ?? "null",
		ctx ?? "null",
		d1.ToString("yyyyMMdd"),
		d2.ToString("yyyyMMdd"),
		DateTime.Now.ToString("yyyyMMddHHmmss"));
	
	File.WriteAllText(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), fileName), buffer);
}


void SetConnString(string db)
{
	// Trick per modificare la lista delle connstring
	// http://stackoverflow.com/questions/360024/how-do-i-set-a-connection-string-config-programatically-in-net
	typeof(ConfigurationElementCollection).GetField("bReadOnly", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(ConfigurationManager.ConnectionStrings, false);

	ConfigurationManager.ConnectionStrings.Add(new ConnectionStringSettings("Eu",  string.Format("data source={0};User ID=ed_framework;Password=ed_framework", db)));
	ConfigurationManager.ConnectionStrings.Add(new ConnectionStringSettings("TMS", string.Format("data source={0};User ID=ed_tms;Password=ed_tms", db)));

}

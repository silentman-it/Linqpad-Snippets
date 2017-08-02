<Query Kind="Program">
  <Reference>C:\Work\BidEvolution\Phase_Zero\packages\Newtonsoft.Json.10.0.2\lib\net40\Newtonsoft.Json.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.configuration.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Security.dll</Reference>
  <Reference>C:\Work\TMS\DEV\TMS\Common\TMS.Core\bin\x64\Debug\TMS.Core.dll</Reference>
  <Namespace>System.Configuration</Namespace>
  <Namespace>TMS.Core.Data.Simple</Namespace>
  <Namespace>Newtonsoft.Json</Namespace>
</Query>

void Main()
{
	SetConnString("win2008r2test4");
	
	string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "SerieData_Dump_DCS_null_8CBA4AAD-4B1E-45A1-B0B4-E3DA29509B19_null_null_20170201_20170204_20170731140206.json");
	
	var o = JsonConvert.DeserializeObject<IEnumerable<SerieDataItem>>(File.ReadAllText(fileName));
	
	o.Dump();

	SerieData.Instance.SetSerieData(o);
	
	

}


void SetConnString(string db)
{
	// Trick per modificare la lista delle connstring
	// http://stackoverflow.com/questions/360024/how-do-i-set-a-connection-string-config-programatically-in-net
	typeof(ConfigurationElementCollection).GetField("bReadOnly", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(ConfigurationManager.ConnectionStrings, false);

	ConfigurationManager.ConnectionStrings.Add(new ConnectionStringSettings("Eu",  string.Format("data source={0};User ID=ed_framework;Password=ed_framework", db)));
	ConfigurationManager.ConnectionStrings.Add(new ConnectionStringSettings("TMS", string.Format("data source={0};User ID=ed_tms;Password=ed_tms", db)));

}

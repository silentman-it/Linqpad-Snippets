<Query Kind="Program">
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
	
	var sd = SerieData.Instance.GetSerieData("Plant", null, "GasConsumptionMeasurement", null, null, DateTime.Parse("28/03/2017 06:00"), DateTime.Parse("28/03/2017 12:15"));

	var lsRows = sd
		//.Where(x => x.ExtendedData.Any())
		.SelectMany(s => s.ExtendedData.Select(d => new {
			Unit = s.Serie.Symbol,
			StartDate = d.StartDate,
			EndDate = d.EndDate,
			Value = d.DoubleValue			
		}))
		.Select(x => string.Format("{0};{1};{2};{3}", x.Unit, x.StartDate, x.EndDate, x.Value))
		.Dump();
		
	File.WriteAllLines(@"C:\Temp\Gas_consumption_from_TP.csv", lsRows);
}


void SetConnString(string db)
{
	// Trick per modificare la lista delle connstring
	// http://stackoverflow.com/questions/360024/how-do-i-set-a-connection-string-config-programatically-in-net
	typeof(ConfigurationElementCollection).GetField("bReadOnly", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(ConfigurationManager.ConnectionStrings, false);

	ConfigurationManager.ConnectionStrings.Add(new ConnectionStringSettings("Eu",  string.Format("data source={0};User ID=ed_framework;Password=ed_framework", db)));
	ConfigurationManager.ConnectionStrings.Add(new ConnectionStringSettings("TMS", string.Format("data source={0};User ID=ed_tms;Password=ed_tms", db)));

}
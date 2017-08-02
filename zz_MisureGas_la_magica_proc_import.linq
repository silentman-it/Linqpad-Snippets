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
		
	var serieDataItems = File
		.ReadAllLines(@"C:\Temp\Gas_consumption_from_TP.csv")
		.Select(x => x.Split(';'))
		.Select(x => new {
			Unit = x[0],
			StartDate = DateTimeOffset.Parse(x[1]),
			EndDate = DateTimeOffset.Parse(x[2]),
			Value = Double.Parse(x[3])
		})
		.GroupBy(x => x.Unit)
		.Select(x => new SerieDataItem()
		{
			Serie = new SerieDescription
			{
				Source = "Plant",
				Symbol = x.Key,
				Class = "GasConsumptionMeasurement",
				UoM = "SMC",
				Frequency = "MINUTE"
			},
			ExtendedData = x.Select(d => new DataItem
			{
				StartDate = d.StartDate,
				EndDate = d.EndDate,
				DoubleValue = d.Value
			}).ToList()
		})
		.Dump();
		
		SerieData.Instance.SetSerieData(serieDataItems);
}


void SetConnString(string db)
{
	// Trick per modificare la lista delle connstring
	// http://stackoverflow.com/questions/360024/how-do-i-set-a-connection-string-config-programatically-in-net
	typeof(ConfigurationElementCollection).GetField("bReadOnly", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(ConfigurationManager.ConnectionStrings, false);

	ConfigurationManager.ConnectionStrings.Add(new ConnectionStringSettings("Eu",  string.Format("data source={0};User ID=ed_framework;Password=ed_framework", db)));
	ConfigurationManager.ConnectionStrings.Add(new ConnectionStringSettings("TMS", string.Format("data source={0};User ID=ed_tms;Password=ed_tms", db)));

}
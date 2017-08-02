<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.configuration.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Security.dll</Reference>
  <Reference>C:\Work\TMS\DEV\TMS\Common\TMS.Core\bin\x64\Debug\TMS.Common.dll</Reference>
  <Reference>C:\Work\TMS\DEV\TMS\Common\TMS.Core\bin\x64\Debug\TMS.Core.dll</Reference>
  <Namespace>System.Configuration</Namespace>
  <Namespace>TMS.Common.Extensions</Namespace>
  <Namespace>TMS.Core.Data.Simple</Namespace>
</Query>

void Main()
{
	SetConnString("svil4");
	
	Random r = new Random(DateTime.Now.Millisecond);
	
	var sdi = new SerieDataItem()
	{
		Serie = new SerieDescription()
		{
//			Source = "Local",
//			Symbol = "UP_FONTANA_B_1",
//			Class = "PlanCorrection",
//			Channel = null,
//			Context = null,
//			Frequency = "FIFTEEN_MINUTES",
//			UoM = "MW",

			Source = "GME",
			Symbol = "UP_FONTANA_B_1",
			Class = "BindingProgram",
			Channel = "FMS",
			Context = "MSD1",
			Frequency = "HOUR",
			UoM = "MWH",

		}
	};

	var flowDate = DateTime.Today;
	
	var dataItems = flowDate.GetAllQuarterHours().Select(x => new DataItem()
	{
		StartDate = x,
		EndDate = x.AddMinutes(15),
		DoubleValue = -5 + (r.NextDouble() * 10)
	});
	
	sdi.ExtendedData = dataItems.ToList();

	SerieData.Instance.SetSerieData(sdi);
	
	//
	// DELETE (use with care!)
	//
//	SerieData.Instance.Debug = true;
	//SerieData.Instance.GetSerieData("GME", "UP_TURBIGO_4", "UnitSchedule", null, null, flowDate, flowDate.AddDays(1)).Dump();

	//SerieData.Instance.DeleteSerieData("GME", "UP_TURBIGO_4", "BindingProgram", "FMS", null, flowDate, flowDate.AddDays(1));
}


void SetConnString(string db)
{
	// Trick per modificare la lista delle connstring
	// http://stackoverflow.com/questions/360024/how-do-i-set-a-connection-string-config-programatically-in-net
	typeof(ConfigurationElementCollection).GetField("bReadOnly", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(ConfigurationManager.ConnectionStrings, false);

	ConfigurationManager.ConnectionStrings.Add(new ConnectionStringSettings("Eu",  string.Format("data source={0};User ID=ed_framework;Password=ed_framework", db)));
	ConfigurationManager.ConnectionStrings.Add(new ConnectionStringSettings("TMS", string.Format("data source={0};User ID=ed_tms;Password=ed_tms", db)));

}
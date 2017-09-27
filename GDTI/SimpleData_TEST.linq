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
	SetConnString("gdti_iren_test");
	
	//
	// DEBUG ?
	//

	//SerieData.Instance.Debug = true;

	//
	// WRITE
	//

	//var sdi = new SerieDataItem()
	//{
	//	Serie = new SerieDescription()
	//	{
	//		Source = "Calculation",
	//		Symbol = "UP_TEST_1",
	//		Class = "SetupChanges",
	//		Channel = "PVMC",
	//		Context = null,
	//		Frequency = "MINUTE",
	//		UoM = "NONE",
	//	}
	//};

	//sdi.Data[DateTime.Parse("23/02/2016 10:00")] = "/nomeassetto";
	//sdi.Data[DateTime.Parse("23/02/2016 17:48")] = "nomeassetto/";
	
	//SerieData.Instance.SetSerieData(sdi);
	
	//
	// DELETE (use with care!)
	//

	//SerieData.Instance.DeleteSerieData("External", "UP_S.PANCRAZ_1", "BindingProgram", null, "MSD4", DateTime.Parse("27/01/2017"), DateTime.Parse("27/01/2017").AddDays(1));
		
	//
	// READ
	//
	
	var sd = SerieData.Instance.GetSerieData("GME", null, "UnitSchedule", null, null, DateTime.Parse("22/09/2017"));
	sd
		.Where(x => x.ExtendedData.Any())
		.Dump();
	
	//
	// SEMAPHORES
	//
	
	//SerieData.Instance.SetSemaphore(null, "UP_TURBIGO_4", "InServiceMinimumTime", "Signal", null, DateTime.Parse("26/02/2016"), "RED", "FAKE");
	
	//
	// SERIES
	//
	
//	SerieData.Instance.GetSeries("GME", "UP_TURBIGO_4", "BindingProgram", null, null)
//		.Where(x => x.Channel != "Signal")
//		.Dump();

}


void SetConnString(string db)
{
	// Trick per modificare la lista delle connstring
	// http://stackoverflow.com/questions/360024/how-do-i-set-a-connection-string-config-programatically-in-net
	typeof(ConfigurationElementCollection).GetField("bReadOnly", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(ConfigurationManager.ConnectionStrings, false);

	ConfigurationManager.ConnectionStrings.Add(new ConnectionStringSettings("Eu",  string.Format("data source={0};User ID=ed_framework;Password=ed_framework", db)));
	ConfigurationManager.ConnectionStrings.Add(new ConnectionStringSettings("TMS", string.Format("data source={0};User ID=ed_tms;Password=ed_tms", db)));

}
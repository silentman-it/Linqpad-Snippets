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
	SetConnString("gdti_alperia_prod");
	
	var flowDate = DateTime.Parse("09/03/2017");
	var sd =
		       SerieData.Instance.GetSerieData("GME",      null, "UnitSchedule",   null,  null, flowDate)   // US
		.Union(SerieData.Instance.GetSerieData("GME",      null, "BindingProgram", "FMS", null, flowDate))  // FMS
		.Union(SerieData.Instance.GetSerieData("External", null, "BindingProgram", null,  null, flowDate))  // PVEnel
		.Union(SerieData.Instance.GetSerieData("Local",    null, "PlanCorrection", "FMS", null, flowDate))  // PCP
	;
	
	sd.ToPivotTable(x => x.Serie.Symbol,
					x => string.Format("{0}{1}{2}{3}", x.Serie.Source, x.Serie.Class, x.Serie.Channel, x.Serie.Context),
					x => x.SelectMany(s => s.ExtendedData).Count())
	.Dump();

}


void SetConnString(string db)
{
	// Trick per modificare la lista delle connstring
	// http://stackoverflow.com/questions/360024/how-do-i-set-a-connection-string-config-programatically-in-net
	typeof(ConfigurationElementCollection).GetField("bReadOnly", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(ConfigurationManager.ConnectionStrings, false);

	ConfigurationManager.ConnectionStrings.Add(new ConnectionStringSettings("Eu",  string.Format("data source={0};User ID=ed_framework;Password=ed_framework", db)));
	ConfigurationManager.ConnectionStrings.Add(new ConnectionStringSettings("TMS", string.Format("data source={0};User ID=ed_tms;Password=ed_tms", db)));

}
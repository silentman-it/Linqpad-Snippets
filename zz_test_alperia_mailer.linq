<Query Kind="Program">
  <Connection>
    <ID>66787ff1-b452-4d4c-ad74-cafaae6e0aaf</ID>
    <Persist>true</Persist>
    <Driver>EntityFrameworkDbContext</Driver>
    <CustomAssemblyPath>C:\Work\TMS\DEV\TMS\Common\TMS.Core\bin\x64\Debug\TMS.Core.dll</CustomAssemblyPath>
    <CustomTypeName>TMS.Core.Data.EntityFramework.ED_TMS_DbContext</CustomTypeName>
    <AppConfigPath>C:\Work\TMS\DEV\TMS\Web\TMS.WS\Web.config</AppConfigPath>
  </Connection>
  <Reference>C:\Work\TMS\DEV\TMS\Common\TMS.Core\bin\x64\Debug\TMS.Core.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.configuration.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Security.dll</Reference>
  <Namespace>TMS.Core.Data.Simple</Namespace>
  <Namespace>System.Configuration</Namespace>
</Query>

void Main()
{
	SetConnString("gdti_tp");
	
	string mkt = "MSD3";
	
	var flowDate = DateTime.Parse("16/11/2016");

//	var sdi_FMS  = SerieData.Instance.GetSerieData("GME", null, "BindingProgram", "FMS", mkt, flowDate);
//	var sdi_US   = SerieData.Instance.GetSerieData("GME", null, "UnitSchedule", null, mkt, flowDate);
	
	var up = GDTIData.Instance.GetUnitProperties(null, "IsHourly").Dump();
	up.ToDictionary(k => k.Unit.ETSOCode, v => v.BooleanValue ?? false).Dump();

	//GDTIData.Instance.GetUnit("UP_NAPOLIL_4").Properties.Dump();
	
//	sdi_FMS.Dump();
//	sdi_US.Dump();
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
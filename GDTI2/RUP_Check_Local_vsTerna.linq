<Query Kind="Program">
  <Connection>
    <ID>c0b901f2-09b0-4fb4-8578-e8c692ab9ed2</ID>
    <Persist>true</Persist>
    <Driver>EntityFrameworkDbContext</Driver>
    <CustomAssemblyPath>C:\Work\TMS\DEV\TMS\Common\GDTI.Data\bin\Debug\GDTI.Data.dll</CustomAssemblyPath>
    <CustomTypeName>GDTI.Data.EDMWarehouseEntities</CustomTypeName>
    <AppConfigPath>C:\Work\TMS\DEV\TMS\Web\TMS.WS\Web.config</AppConfigPath>
  </Connection>
  <Reference>C:\Work\TMS\DEV\TMS\Common\GDTI.Data\bin\Debug\GDTI.Data.dll</Reference>
  <Namespace>GDTI.Data</Namespace>
</Query>

void Main()
{
	GDTIData.Instance.Debug = false;
	var local = GDTIData.Instance.Get("RUPLocal", "UP_TURBIGO_4", null, null, null, DateTimeOffset.Parse("26/10/2014"), DateTimeOffset.Parse("27/10/2014"));
	var terna = GDTIData.Instance.Get("RUPTerna", "UP_TURBIGO_4", null, null, null, DateTimeOffset.Parse("26/10/2014"), DateTimeOffset.Parse("27/10/2014"));
	
	
	local.Join(
		terna,
		loc => string.Format("{0}/{1}/{2}/{3}", loc.Symbol, loc.Class, loc.Channel, loc.Context),
		ter => string.Format("{0}/{1}/{2}/{3}", ter.Symbol, ter.Class, ter.Channel, ter.Context),
		(loc, ter) => new KeyValuePair<string, bool>(
			string.Format("{0}/{1}/{2}/{3}", loc.Symbol, loc.Class, loc.Channel, loc.Context),
			CompareData(loc.Data, ter.Data)
			)
		)
		.Dump();
		
		
	
}

// Define other methods and classes here
public bool CompareData(Dictionary<DateTimeOffset, object> local, Dictionary<DateTimeOffset, object> terna)
{
	// true se le dimensioni degli array sono differenti o se ci sono elementi diversi
	var b = local.Count() != terna.Count() || local.Except(terna).Any();
	return b;
}
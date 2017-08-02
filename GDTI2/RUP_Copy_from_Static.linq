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
	
	string unitName = "UP_TURBIGO_4";
	var baseDate = DateTime.Today;
	//int numDays = 1;
	
	//List<GDTIDataItem> newRup = new List<GDTIDataItem>();
	
	var rup = GDTIData.Instance.Get("StaticRUP", unitName, null, null, null, DateTimeOffset.Now, DateTimeOffset.Now);
	
	if(rup == null || !rup.Any())
		throw new Exception("No Static RUP for...");
	
	var psmins = rup
		.Where(x => x.Channel == "PSMIN")
		.Select(x => new { x.Class, x.Data.First().Value })
		.OrderBy(x => x.Value)
		.Select(x => x.Class)
		.ToList()
		.Dump();
	
	var newRup = rup.Select(x => new GDTIDataItem()
	{
		Source = "RUPLocal",
		Symbol = x.Symbol,
		Class = string.Format("FASCIA_{0:00}", psmins.IndexOf(x.Class) + 1),
		Channel = x.Channel,
		Frequency = "FIFTEEN_MINUTES",
		UoM = x.UoM,
		ExtendedData = null,
		Data = FillData(x.Data, baseDate)
	});
	
	newRup.Dump();

}

Dictionary<DateTimeOffset, object> FillData(Dictionary<DateTimeOffset, object> dic, DateTime baseDate)
{
	Dictionary<DateTimeOffset, object> outdic = new Dictionary<DateTimeOffset, object>();
	object v = dic.First().Value;
	
	for(var d = baseDate.Date; d < baseDate.AddDays(1); d = d.AddMinutes(15))
	{
		outdic[d] = v;
	}
	
	return outdic;
}
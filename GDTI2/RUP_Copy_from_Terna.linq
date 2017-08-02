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
	int numDays = 1;
	
	List<GDTIDataItem> newRup = new List<GDTIDataItem>();
	
	var rup = GDTIData.Instance.Get("RUPTerna", unitName, null, null, null, DateTimeOffset.Parse("26/10/2014"), DateTimeOffset.Parse("27/10/2014"));

	rup = rup.Where(x => x.Channel != "VariationID").ToList();
	
	for (int i = 0; i < numDays; i++)
	{
		newRup.AddRange(rup.Select(x => new GDTIDataItem()
		{
			Source = "RUPLocal",
			Symbol = x.Symbol,
			Class = x.Class,
			Channel = x.Channel,
			Context = x.Context,
			Frequency = x.Frequency,
			UoM = x.UoM,
			ExtendedData = null,
			Data = TimeShift(x.Data, baseDate.AddDays(i))
		}));
	}
	
	newRup.Dump();
}

// Define other methods and classes here
Dictionary<DateTimeOffset, object> TimeShift(Dictionary<DateTimeOffset, object> dic, DateTime baseDate)
{
	var shifted = dic
		.Select(x => new KeyValuePair<DateTimeOffset, object>(
				new DateTimeOffset(baseDate.Add(x.Key.TimeOfDay)),
				x.Value))
		.GroupBy(x => x.Key)
		.Select(x => new KeyValuePair<DateTimeOffset, object>(x.Key, x.FirstOrDefault().Value))
		.ToDictionary(x => x.Key, x => x.Value);
	
	return shifted;
	
}
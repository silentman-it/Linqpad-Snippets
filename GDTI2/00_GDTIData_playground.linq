<Query Kind="Statements">
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
  <Namespace>GDTI.Data.Extensions</Namespace>
</Query>

GDTIData.Instance.Debug = false;
var rup = GDTIData.Instance.Get("RUPLocal", "UP_BUSSENTO_1", null, null, null, DateTimeOffset.Parse("06/02/2015"), DateTimeOffset.Parse("07/02/2015"))
//.Select(x => new
//{
//	x.Source,
//	x.Symbol,
//	x.Class,
//	x.Channel,
//	x.Context,
//	//Vals = x.Data.Select(d => d.Value).SingleOrDefault()
//})
.OrderBy(x => x.Symbol)
.ThenBy(x => x.Class)
.ThenBy(x => x.Channel)

.Where(x => x.Source == "RUPLocal" || x.Source == "RUPTerna")
.Where(x => x.Channel == "PSMIN" || x.Channel == "PSMAX")
.SelectMany(x => x.Data.Select(d => new {
	x.Source,
	x.Class,
	x.Channel,
	d.Key,
	d.Value
}))
//.GroupBy(x => new {x.Source, x.Class})
//.Select(x => new
//{
//	x.Key,
//	Table = x.ToPivotTable(p => p.Key.ToString(), p => p.Channel, p => p.First().Value)
//})
.ToPivotTable(p => p.Key.ToString(), p => p.Source + Environment.NewLine + p.Class + Environment.NewLine + p.Channel, p => p.First().Value)
.Dump()
;
//
//

// SET

//var ls = new List<GDTIDataItem>();
//var di = new GDTIDataItem()
//{
//	Source = "TestIrregular",
//	Frequency = "IRREGULAR",
//	UoM = "NONE",
//	ExtendedData = new Dictionary<GDTITimeRange, object>()	
//};
//
//di.ExtendedData.Add(new GDTITimeRange(DateTimeOffset.Parse("20/01/2015 3:47 +01:00"), DateTimeOffset.Parse("21/01/2015 14:30 +01:00")), 777);
//
//ls.Add(di);
//
//GDTIData.Instance.Set("Prova_serie_irregular.xml", ls);

// SEMAPHORES

//GDTIData.Instance.GetSemaphores(null, null, DateTimeOffset.Now.Date, DateTimeOffset.Now.Date.AddDays(1))
//.Dump();

//GDTIData.Instance.SetSemaphores(new List<GDTISemaphore>()
//{
//	new GDTISemaphore()
//	{
//		Type = "RUPTerna",
//		Key = "UP_NRGAMOLISE_1",
//		ValidityStartDate = DateTimeOffset.Parse("10/02/2015"),
//		ValidityEndDate = DateTimeOffset.Parse("11/02/2015"),
//		Value = "GREEN",
//		Notes = "Confirmed"
//	}
//});
<Query Kind="Statements">
  <Connection>
    <ID>1b664880-aaa6-49a8-86d0-43deea4970dd</ID>
    <Persist>true</Persist>
    <Driver>EntityFrameworkDbContext</Driver>
    <CustomAssemblyPath>C:\Work\EMODS\DEV\EMODS2\Data\EMODS.Data\bin\Debug\EMODS.Data.dll</CustomAssemblyPath>
    <CustomTypeName>EMODS.Data.EDMWarehouseContainer</CustomTypeName>
    <AppConfigPath>C:\Work\EMODS\DEV\EMODS2\Data\EMODS.Data\bin\Debug\EMODS.Data.dll.config</AppConfigPath>
  </Connection>
</Query>

var df = DateTime.Parse("05/04/2014");
var dt = df.AddDays(1);

var serieIds = SerieSet.Where (s =>
	s.Source == "Calculation" &&
	s.Class == "Costs" &&
	s.Channel == "Attiva"
	).Select(x => x.Id).ToArray();

// Filtra time range e serie
var filterSerieValidity = SerieDetailSet
.Where(x =>
	serieIds.Contains(x.Serie.Id) &&
	x.ValidityStartDate >= df &&
	x.ValidityEndDate < dt);
	
// Raggruppa per serie e versione
var groupSerie = filterSerieValidity
	.GroupBy(x => new { x.Serie, x.TimeStamp })
	.ToList();
	
//groupSerie.Select(x => new { S = x.Key.Serie.Id, TS = x.Key.TimeStamp }).Dump();

groupSerie
.GroupBy(x => x.Key.Serie)
.Select(x => new {
	Serie = x.Key,
	Data = x.OrderByDescending(d => d.Key.TimeStamp).FirstOrDefault().ToDictionary(k => k.ValidityStartDate, v => v.DoubleValue)
})
.Dump()
;
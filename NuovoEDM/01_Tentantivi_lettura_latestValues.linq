<Query Kind="Statements">
  <Connection>
    <ID>04246806-8f9e-4db3-91b7-da8187f0df18</ID>
    <Persist>true</Persist>
    <Driver>EntityFrameworkDbContext</Driver>
    <CustomAssemblyPath>C:\Work\EMODS\DEV\Web\EMODS.Data\bin\Debug\EMODS.Data.dll</CustomAssemblyPath>
    <CustomTypeName>EMODS.Data.EDMWarehouseContainer</CustomTypeName>
    <AppConfigPath>C:\Work\EMODS\DEV\Web\EMODS.Data\app.config</AppConfigPath>
    <DisplayName>EDMWarehouseContainer_MariaDB</DisplayName>
  </Connection>
</Query>

var sw = Stopwatch.StartNew();

var serieIds = SerieSet.Where(x =>
	x.Source.Name == "Calculation" &&
	x.Symbol.Name == "Puccini" &&
	x.Class.Name == "Costs" &&
	//x.Channel.Name == "Attiva" &&
	true
	)
	.Select(x => x.Id)
	.ToArray();

if(!serieIds.Any())
	throw new Exception("Serie not found");
	
sw.Restart();
var maxEndDate = SerieDetailSet.Where(x => serieIds.Contains(x.Serie.Id)).Max(x => x.ValidityEndDate).Dump();
sw.Elapsed.Dump("max()");
	
sw.Restart();
var ls = SerieDetailSet
.Where(x => serieIds.Contains(x.Serie.Id) && x.ValidityEndDate == maxEndDate)
.OrderByDescending(x => x.ValidityEndDate)
.ToList()
.GroupBy(x => new { x.ValidityStartDate, x.ValidityEndDate })
.First().OrderByDescending(x => x.TimeStamp).FirstOrDefault()
;
sw.Elapsed.Dump("Select");

ls.Dump();
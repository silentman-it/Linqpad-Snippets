<Query Kind="Statements">
  <Connection>
    <ID>cd1884ca-c1e5-4913-b91c-7eb3833a2156</ID>
    <Persist>true</Persist>
    <Driver>EntityFrameworkDbContext</Driver>
    <CustomAssemblyPath>C:\Work\EMODS\DEV\Web\EMODS.Data\bin\Debug\EMODS.Data.dll</CustomAssemblyPath>
    <CustomTypeName>EMODS.Data.EDMWarehouseContainer</CustomTypeName>
    <AppConfigPath>C:\Work\EMODS\DEV\Web\EMODS.Data\bin\Debug\EMODS.Data.dll.config</AppConfigPath>
  </Connection>
</Query>

var s = "Puccini";
var serieIds = SerieSet.Where(x =>
	x.Source.Name.Contains(s) ||
	x.Symbol.Name.Contains(s) ||
	x.Class.Name.Contains(s) ||
	x.Channel.Name.Contains(s) ||
	x.Context.Name.Contains(s))
	.Select(x => new {
		Source = x.Source.Name,
		Symbol = x.Symbol.Name,
		Class = x.Class.Name,
		Channel = x.Channel.Name,
		Context = x.Context.Name,
	})
	.Dump("Search matching: \"" + s + "\"");
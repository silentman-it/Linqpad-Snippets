<Query Kind="Statements">
  <Connection>
    <ID>2ae0a590-6106-4275-804d-699870600cc9</ID>
    <Persist>true</Persist>
    <Driver>EntityFrameworkDbContext</Driver>
    <CustomAssemblyPath>G:\Work\FipavManager\Backend\FipavManagerDal\bin\Debug\FipavManagerDal.dll</CustomAssemblyPath>
    <CustomTypeName>FipavManagerDal.FipavContextContainer</CustomTypeName>
    <AppConfigPath>G:\Work\FipavManager\Backend\FipavManagerDal\bin\Debug\FipavManagerDal.dll.config</AppConfigPath>
  </Connection>
  <Output>DataGrids</Output>
</Query>

var g = GaraSet.Where(x => x.Stagione == "2014" && x.Id == 10000).Single().Dump();

//foreach(var d in g.Designazione.ToList())
//	DesignazioneSet.Remove(d);
//
//var sq = SquadraSet.Where(x => x.Nome.StartsWith("TEST ")).Dump();
//
//foreach(var s in sq.ToList())
//{
//	SquadraSet.Remove(s);
//}
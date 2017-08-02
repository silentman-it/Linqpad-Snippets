<Query Kind="Statements">
  <Connection>
    <ID>ca2dc2b6-8c9f-4aac-a665-f228d6780d91</ID>
    <Persist>true</Persist>
    <Driver>EntityFrameworkDbContext</Driver>
    <CustomAssemblyPath>C:\Work\EMODS\DEV\EMODS2\Gatherer\Gatherer.MySQL.Data\bin\Debug\Gatherer.MySQL.Data.dll</CustomAssemblyPath>
    <CustomTypeName>Gatherer.MySQL.Data.GathererContainer</CustomTypeName>
    <AppConfigPath>C:\Work\EMODS\DEV\EMODS2\Gatherer\Gatherer.MySQL.Data\bin\Debug\Gatherer.MySQL.Data.dll.config</AppConfigPath>
  </Connection>
</Query>

//ActivitySet.Count(x => x.Status != "SUC" && x.Status != "ERR").Dump("ActivitySet");
//ActivitySet.Where(x => x.Status != "SUC" && x.Status != "ERR").Take(10).Dump();

MessageSet
.GroupBy(x => x.Status)
.Select(x => new {
	Status = x.Key,
	Count = x.Count()
})
.Dump("Gatherer Status (Messages)");


ActivitySet
.GroupBy(x => x.Status)
.Select(x => new {
	Status = x.Key,
	Count = x.Count()
})
.Dump("Gatherer Status (Activities)");

// Svuote Coda Messaggi
//var lsMsgs = MessageSet.Where(x => x.Status != "COM" && x.Status != "ERR");
//foreach (var msg in lsMsgs) msg.Status = "ERR";
//
//var lsAct = ActivitySet.Where(x => x.Status != "SUC" && x.Status != "ERR");
//foreach (var act in lsAct) act.Status = "ERR";
//
//SaveChanges();
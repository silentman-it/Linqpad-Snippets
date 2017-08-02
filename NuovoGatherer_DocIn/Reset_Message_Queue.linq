<Query Kind="Statements">
  <Connection>
    <ID>810b533b-80cb-4257-807c-6efb3bfd01fb</ID>
    <Persist>true</Persist>
    <Driver>EntityFrameworkDbContext</Driver>
    <CustomAssemblyPath>C:\Work\NRG_SVIL\DEV\SrcNet\Gatherer\Gatherer.Service\bin\Debug\Gatherer.Data.dll</CustomAssemblyPath>
    <CustomTypeName>Gatherer.Data.GathererEntities</CustomTypeName>
    <AppConfigPath>C:\Work\NRG_SVIL\DEV\SrcNet\Gatherer\Gatherer.Service\bin\Debug\Gatherer.Service.exe.config</AppConfigPath>
  </Connection>
</Query>

var m = MESSAGES.Where(x => x.STATUS != "COM");
m.ToList().ForEach(x => x.STATUS = "ERR");
SaveChanges();
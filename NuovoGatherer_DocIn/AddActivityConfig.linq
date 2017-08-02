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

ACTIVITYCONFIGURATIONS.Add(new ACTIVITYCONFIGURATIONS() {
	RULE     = "/*[local-name()='PIPEDocument']/*[local-name()='PIPTransaction']/*[local-name()='BidNotification']",
	SCHEMA   = "asm://GME.ActivityPack.Schemas.BidNotification.xsd,GME.ActivityPack",
	ASSEMBLY = "GME.ActivityPack.PIPEBidNotification,GME.ActivityPack"
});

SaveChanges();

ACTIVITYCONFIGURATIONS.Dump();
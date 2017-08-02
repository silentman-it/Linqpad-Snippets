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

string[] arr = {
//"gatherer                 |||  activityItem     |||  Gatherer.Provider.Provider.GathererActivityItem, Gatherer.Provider            ",
//"gatherer                 |||  blobItem         |||  Gatherer.Provider.Provider.GathererBlobItem, Gatherer.Provider                ",
//"gatherer                 |||  messageEnvelope  |||  Gatherer.Provider.Provider.GathererMessageEnvelope, Gatherer.Provider         ",
//"gatherer                 |||  messageItem      |||  Gatherer.Provider.Provider.GathererMessageItem, Gatherer.Provider             ",
//"gatherer                 |||  messageProvider  |||  Gatherer.Provider.Provider.GathererMessageProvider, Gatherer.Provider         ",
//"gatherer                 |||  runnerProvider   |||  EnergyCore.Gatherer.Core.Providers.Runners.SingleRunnerProvider, EnergyCore   ",
//"gatherer/core/modules    |||  0                |||  Gatherer.Provider.Modules.GathererModule, Gatherer.Provider                   ",
//"gatherer/server          |||  policyProvider   |||  EnergyCore.Gatherer.Core.Providers.Policies.SerialPolicyProvider, EnergyCore  ",
//"gatherer/log/listeners/0 |||  minimumSeverity  |||  255                                                                           ",
//"gatherer/log/listeners/0 |||  typeName         |||  Gatherer.Provider.Logs.NrgSuiteLogListener, Gatherer.Provider                 ",
//"gatherer/server/jobs/0   |||  Timeout          |||  00:01:00                                                                      ",
//"gatherer/server/jobs/0   |||  TypeName         |||  Gatherer.Jobs.AutoUploader, Gatherer.Jobs                                     ",
//"commonXmlSchemas         |||  0                |||  GME.ActivityPack.Schemas.SimpleTypesv1_0.xsd, GME.ActivityPack                ",
};

char[] seps = {'|'};

foreach(var row in arr)
{
	var tok = row.Split(seps, StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim()).ToArray();
	
	PARAMETERS.Add(new PARAMETERS() {
		CLASS = tok[0],
		PARAMETERKEY = tok[1],
		TEXTVALUE1 = tok[2]
	});
}

//SaveChanges();

PARAMETERS.Dump();
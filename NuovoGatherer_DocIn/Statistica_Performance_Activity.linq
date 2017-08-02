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

ACTIVITIES
.GroupBy(x => new { x.NAME, x.STATUS })
.ToList()
.Select(x => new {
	Act = x.Key.NAME,
	Status = x.Key.STATUS,
	Average = x.Any(a => a.EXECUTIONSTART.HasValue && a.EXECUTIONEND.HasValue) ? TimeSpan.FromMilliseconds(x.Where(a => a.EXECUTIONSTART.HasValue && a.EXECUTIONEND.HasValue).Average(a => (a.EXECUTIONEND.Value - a.EXECUTIONSTART.Value).TotalMilliseconds)) : TimeSpan.Zero,
	Count = x.Count()
})
.Dump();
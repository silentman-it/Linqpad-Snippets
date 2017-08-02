<Query Kind="Statements">
  <Connection>
    <ID>cd1884ca-c1e5-4913-b91c-7eb3833a2156</ID>
    <Persist>true</Persist>
    <Driver>EntityFrameworkDbContext</Driver>
    <CustomAssemblyPath>C:\Work\EMODS\DEV\Web\EMODS.Data\bin\Debug\EMODS.Data.dll</CustomAssemblyPath>
    <CustomTypeName>EMODS.Data.EMODS_DataModelContainer</CustomTypeName>
    <AppConfigPath>C:\Work\EMODS\DEV\Web\EMODS.Data\bin\Debug\EMODS.Data.dll.config</AppConfigPath>
  </Connection>
  <Output>DataGrids</Output>
</Query>

var fede = UserSet.Where(x => x.UserName == "Fede").Single();

var eqIds = fede.Permission.Select(x => x.Equipment.Id).Dump();

var ls1 = fede
	.Permission
	.ToList();

var ls2 = EquipmentSet
		.ToList()
		.Where(x => !eqIds.Contains(x.Id))
		.Select(x => new Permission()
		{
			CanRead = false,
			CanWrite = false,
			Equipment = x,
			User = fede
		});

ls1.Union(ls2).Dump();
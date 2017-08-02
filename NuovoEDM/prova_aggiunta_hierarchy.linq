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

var fede = UserSet.SingleOrDefault(x => x.Name == "federico");

fede.Permission.Add(new Permission { Type = "CanManagePage", Enabled = true });
//var rootNode = fede.Role.HierarchyNode.Single(x => x.Hierarchy.Id == "Pages").Dump();
//
//var n = new HierarchyNode() { Hierarchy = HierarchySet.Find("Pages"), Label = "" };
//rootNode.Children.Add(n);
//
//n.SystemPage.Add(new SystemPage() { Title = "Child Page #2" });

SaveChanges();
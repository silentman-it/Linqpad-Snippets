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

//RoleSet.Find("Selex ES", "Executive").HierarchyNode.Add(
//HierarchyNodeSet.SingleOrDefault(x => x.Label == "Executive Home Page"));
RoleSet.Find("Digitex", "Tenant").HierarchyNode.Add(
HierarchyNodeSet.SingleOrDefault(x => x.Label == "Tenant"));

//SaveChanges();
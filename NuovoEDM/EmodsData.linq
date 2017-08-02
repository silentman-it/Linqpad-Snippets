<Query Kind="Statements">
  <Connection>
    <ID>1b664880-aaa6-49a8-86d0-43deea4970dd</ID>
    <Persist>true</Persist>
    <Driver>EntityFrameworkDbContext</Driver>
    <CustomAssemblyPath>C:\Work\EMODS\DEV\EMODS2\Data\EMODS.Data\bin\Debug\EMODS.Data.dll</CustomAssemblyPath>
    <CustomTypeName>EMODS.Data.EDMWarehouseContainer</CustomTypeName>
    <AppConfigPath>C:\Work\EMODS\DEV\EMODS2\Data\EMODS.Data\bin\Debug\EMODS.Data.dll.config</AppConfigPath>
  </Connection>
  <Reference>C:\Work\EMODS\DEV\EMODS2\Data\EMODS.Data\bin\Debug\EMODS.Data.dll</Reference>
</Query>

EMODS.Data.EmodsData.Value.Debug = false;

//var df = DateTime.Parse("21/11/2013", System.Globalization.CultureInfo.GetCultureInfo("it-IT"));
//var dt = DateTime.Parse("22/11/2013", System.Globalization.CultureInfo.GetCultureInfo("it-IT"));
//var o = EMODS.Data.EmodsData.Value.Get("IESolutions", "Puccini", "Energia" , "Attiva", null, df, dt);
//o.Dump();

//string.Join(",", o.FirstOrDefault().Data.Select(x => x.Value).ToArray()).Dump();

//EmodsHierarchy.Value.Get("ROOT NODE", "Pages")
var tree = EmodsCMS.Value.GetNavigationTree("System");

foreach (var element in tree.BreadthFirstEnumerator())
{
	Console.WriteLine (element.Title);	
}
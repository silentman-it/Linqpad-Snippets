<Query Kind="Statements">
  <Connection>
    <ID>1b664880-aaa6-49a8-86d0-43deea4970dd</ID>
    <Persist>true</Persist>
    <Driver>EntityFrameworkDbContext</Driver>
    <CustomAssemblyPath>C:\Work\EMODS\DEV\EMODS2\Data\EMODS.Data\bin\Debug\EMODS.Data.dll</CustomAssemblyPath>
    <CustomTypeName>EMODS.Data.EDMWarehouseContainer</CustomTypeName>
    <AppConfigPath>C:\Work\EMODS\DEV\EMODS2\Data\EMODS.Data\bin\Debug\EMODS.Data.dll.config</AppConfigPath>
  </Connection>
  <Namespace>System.Globalization</Namespace>
</Query>

var s = 
@"Genova Via Puccini, 44.422679, 8.851608
Genova Via Hermada, 44.423092, 8.853051
Genova Via Pieragostini, 44.414958, 8.881128
Firenze Campi Bisenzio, 43.841733, 11.149192
Torino Caselle, 45.201124, 7.642491
Catania, 37.424960, 15.050041
UK Basildon, 51.590868, 0.494275
UK Edinburgh, 55.969313, -3.236427";


var du = s.Replace("\r\n", "\n").Split('\n').Select(x => x.Split(',').Select(t => t.Trim()).ToArray())
.Dump();


foreach (var element in du)
{
	GeoDataSet.Add(new GeoData() { Id = element[0], Longitude = Convert.ToDouble(element[2], CultureInfo.GetCultureInfo("en-US")), Latitude = Convert.ToDouble(element[1], CultureInfo.GetCultureInfo("en-US")), IsPublic = true });	
}

SaveChanges();
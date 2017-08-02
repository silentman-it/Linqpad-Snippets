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
  <Reference>C:\Work\SEM_PRJ\SEM\DEV\Common\SEM.Proxies.SIF.WSPC\bin\x64\Debug\SEM.Proxies.SIF.WSPC.dll</Reference>
  <Reference>C:\Work\SEM_PRJ\SEM\DEV\Common\TMS.Common\bin\x64\Debug\TMS.Common.dll</Reference>
  <Namespace>SEM.Proxies.SIF.WSPC</Namespace>
  <Namespace>TMS.Common.Utils.Serialization</Namespace>
</Query>

//// Fase 1: aggiungi utente
//
//var r = RoleSet.Add(new Role() { RoleName = "ADMIN" });
//
//var newUser = UserSet.Add(new User()
//{
//UserName = "Fede",
//Password = "DIgCi/OqamoUPthG8r4epA==",
//Email = "federico.coppola@selex-es.com",
//UserStatus = "VALID",
//Role = r,
//Phone = "+393478145523",
//LastPasswordChange = DateTime.MinValue,
//LastLockoutDate = DateTime.MinValue,
//FailedAttempts = 0,
//FullName = "Federico Coppola",
//LastAccess = DateTime.MinValue,
//OldPasswords = "",
//Notes = "",
//PasswordAnswer = "Avezzano",
//PasswordQuestion = "Cognome di tua mamma da nubile?"
//});
//
//SaveChanges();
//
//// Fase 2: popola Equips
//
//string filePlantConfig = @"C:\TEMP\PointDescriptors_PlantConfig.dat";
//
//IEnumerable<PointDescriptor> pdList = BinarySerialization.Deserialize<IEnumerable<PointDescriptor>>(File.ReadAllBytes(filePlantConfig));
//
//
//foreach(var pd in pdList)
//{
//	EquipmentSet.Add(new EMODS.Data.Equipment()
//	{
//		Name = string.Format("{0}{1}{2}{3}{4}{5} {6} {7}",
//				pd.Location,
//				pd.Product,
//				pd.Equipment_L1,
//				pd.Equipment_L2,
//				pd.Equipment_L3,
//				pd.Equipment_N,
//				pd.Code,
//				pd.Progr),
//		Description = pd.Description
//				
//	});
//}
//
//SaveChanges();

EquipmentSet.Count().Dump();


// Fase 2.1: get Fede
var newUser = UserSet.Where(x => x.UserName == "Fede").Single();

// Fase 3: crea permission
newUser.Permission.Add(new Permission()
{
	CanRead = true,
	CanWrite = false,
	Equipment = EquipmentSet.First()
});

newUser.Permission.Add(new Permission()
{
	CanRead = true,
	CanWrite = true,
	Equipment = EquipmentSet.OrderBy(x => x.Name).Skip(1).First()
});

SaveChanges();

UserSet.Where(x => x.UserName == "Fede").Dump();
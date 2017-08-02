<Query Kind="Statements">
  <Connection>
    <ID>cd1884ca-c1e5-4913-b91c-7eb3833a2156</ID>
    <Persist>true</Persist>
    <Driver>EntityFrameworkDbContext</Driver>
    <CustomAssemblyPath>C:\Work\EMODS\DEV\Web\EMODS.Data\bin\Debug\EMODS.Data.dll</CustomAssemblyPath>
    <CustomTypeName>EMODS.Data.EMODS_DataModelContainer</CustomTypeName>
    <AppConfigPath>C:\Work\EMODS\DEV\Web\EMODS.Data\bin\Debug\EMODS.Data.dll.config</AppConfigPath>
  </Connection>
</Query>

var r = RoleSet.Add(new Role() { RoleName = "ADMIN" });

UserSet.Add(new User()
{
UserName = "Fede",
Password = "DIgCi/OqamoUPthG8r4epA==",
Email = "federico.coppola@selex-es.com",
UserStatus = "VALID",
Role = r,
Phone = "+393478145523",
LastPasswordChange = DateTime.MinValue,
LastLockoutDate = DateTime.MinValue,
FailedAttempts = 0,
FullName = "Federico Coppola",
LastAccess = DateTime.MinValue,
OldPasswords = "",
Notes = "",
PasswordAnswer = "Avezzano",
PasswordQuestion = "Cognome di tua mamma da nubile?"
});

SaveChanges();

UserSet.Where(x => x.UserName == "Fede").Dump();
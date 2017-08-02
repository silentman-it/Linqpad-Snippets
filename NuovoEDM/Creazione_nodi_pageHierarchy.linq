<Query Kind="Program">
  <Connection>
    <ID>1b664880-aaa6-49a8-86d0-43deea4970dd</ID>
    <Persist>true</Persist>
    <Driver>EntityFrameworkDbContext</Driver>
    <CustomAssemblyPath>C:\Work\EMODS\DEV\EMODS2\Data\EMODS.Data\bin\Debug\EMODS.Data.dll</CustomAssemblyPath>
    <CustomTypeName>EMODS.Data.EDMWarehouseContainer</CustomTypeName>
    <AppConfigPath>C:\Work\EMODS\DEV\EMODS2\Data\EMODS.Data\bin\Debug\EMODS.Data.dll.config</AppConfigPath>
  </Connection>
  <Namespace>System.Security.Cryptography</Namespace>
</Query>

void Main()
{
	// roles
//	var sesEx = RoleSet.Add(new Role() { Organization = "Selex ES", Name = "Executive" });
//	var sesEm = RoleSet.Add(new Role() { Organization = "Selex ES", Name = "Energy Manager" });
//	var dgtxT = RoleSet.Add(new Role() { Organization = "Digitex", Name = "Tenant" });
//	
//	sesEx.HierarchyNode.Add(HierarchyNodeSet.Single(x => x.Label == "Executive Home Page"));
//	sesEm.HierarchyNode.Add(HierarchyNodeSet.Single(x => x.Label == "Energy Manager Home Page"));
//	dgtxT.HierarchyNode.Add(HierarchyNodeSet.Single(x => x.Label == "Tenant"));
//	
//	SaveChanges();
	
	
	// users
//	UserSet.Add(new User() {
//		Name = "claudio",
//		FullName = "Claudio Cabrini",
//		Email = "claudio.cabrini@selex-es.com",
//		Role = RoleSet.Find("Selex ES", "Energy Manager"),
//		Password = Encode("emods"),
//		Status = "VALID",
//		LastPasswordChange = DateTime.Now
//	});
//	
//	SaveChanges();
	
}

//// Define other methods and classes here
//HierarchyNode AddChildPageNode(HierarchyNode parent, string childNodeLabel)
//{
//	var cn = new HierarchyNode() { Label = childNodeLabel, Hierarchy = HierarchySet.Find("Pages") };
//	parent.Children.Add(cn);
//	return cn;
//}
//
//HierarchyNode CreatePageNode(string nodeLabel)
//{
//	var n = HierarchyNodeSet.Add(new HierarchyNode() { Label = nodeLabel, Hierarchy = HierarchySet.Find("Pages") });
//	return n;
//}
//



public static string Encode(string clearText)
{
	Byte[] originalBytes;
	Byte[] encodedBytes;
	MD5 md5;

	md5 = new MD5CryptoServiceProvider();
	originalBytes = ASCIIEncoding.Default.GetBytes(clearText);
	encodedBytes = md5.ComputeHash(originalBytes);

	return Convert.ToBase64String(encodedBytes);
}
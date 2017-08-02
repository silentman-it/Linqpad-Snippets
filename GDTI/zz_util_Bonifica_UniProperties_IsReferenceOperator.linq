<Query Kind="Statements">
  <Connection>
    <ID>66787ff1-b452-4d4c-ad74-cafaae6e0aaf</ID>
    <Persist>true</Persist>
    <Driver>EntityFrameworkDbContext</Driver>
    <CustomAssemblyPath>C:\Work\TMS\DEV\TMS\Common\TMS.Core\bin\x64\Debug\TMS.Core.dll</CustomAssemblyPath>
    <CustomTypeName>TMS.Core.Data.EntityFramework.ED_TMS_DbContext</CustomTypeName>
    <AppConfigPath>C:\Work\TMS\DEV\TMS\Web\TMS.WS\Web.config</AppConfigPath>
  </Connection>
</Query>

var lsUnitProperties = UnitProperties.Include("Unit");

foreach(var u in Units)
{
	var hasRefOp = lsUnitProperties.Any(x => x.Unit.ETSOCode == u.ETSOCode && x.Name == "IsReferenceOperator");
	if(!hasRefOp)
	{
		var setting = new UnitProperty() { Unit = u, Name = "IsReferenceOperator", BooleanValue = true };
		UnitProperties.Add(setting);
	}
}

SaveChanges();

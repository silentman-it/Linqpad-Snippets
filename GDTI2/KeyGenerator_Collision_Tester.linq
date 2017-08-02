<Query Kind="Statements">
  <Connection>
    <ID>c0b901f2-09b0-4fb4-8578-e8c692ab9ed2</ID>
    <Persist>true</Persist>
    <Driver>EntityFrameworkDbContext</Driver>
    <CustomAssemblyPath>C:\Work\TMS\DEV\TMS\Common\GDTI.Data\bin\Debug\GDTI.Data.dll</CustomAssemblyPath>
    <CustomTypeName>GDTI.Data.EDMWarehouseEntities</CustomTypeName>
    <AppConfigPath>C:\Work\TMS\DEV\TMS\Web\TMS.WS\Web.config</AppConfigPath>
  </Connection>
</Query>

// Last run: Exception: COLLISION AFTER 43316918 LOOPS!

var hist = new HashSet<string>();

int n = 0;

while(++n < 100000000)
{
	var k = KeyGenerator.Instance.Create();
	if(hist.Contains(k)) throw new Exception("COLLISION AFTER " + n + " LOOPS!");
	else hist.Add(k);
	
	if(n % 1000000 == 0) Console.WriteLine (n);
}
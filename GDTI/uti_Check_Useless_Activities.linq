<Query Kind="Program">
  <Connection>
    <ID>4350ef31-afed-4427-8f50-10d748e6778f</ID>
    <Persist>true</Persist>
    <Driver Assembly="IQDriver" PublicKeyToken="5b59726538a49684">IQDriver.IQDriver</Driver>
    <Provider>Devart.Data.Oracle</Provider>
    <CustomCxString>AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAAC5XQXlS+t0aO3JgY8nUl0AAAAAACAAAAAAADZgAAwAAAABAAAACfXkpTF0ppC5hqDHdIiGu6AAAAAASAAACgAAAAEAAAAODLRGTs6zVVCgMVBfTMdFhAAAAAM93E9PqDR0jKAdg72t1D3iJx5YxtghB0gjzPsRcAP+cjOvnvVcnwjHYC+6BDeBUwZu/6Z93YiqwDjP2INxaznxQAAACcw2+lc5V+B4IHpY1c/rBugCNqzg==</CustomCxString>
    <Server>bessie3</Server>
    <UserName>ed_framework</UserName>
    <Password>AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAA3q0pbjoE2kGb/5UBq8YBrAAAAAACAAAAAAADZgAAwAAAABAAAABVrrxKg6/4vhQd1lIiur2IAAAAAASAAACgAAAAEAAAAHKuZ7nzMQ/sTdM915NZeucQAAAAJLcFL5O8dnK2NAYHpdxQ1RQAAADiMOYc0A6fiuPTcBdzTK7eRv64bQ==</Password>
    <EncryptCustomCxString>true</EncryptCustomCxString>
    <DriverData>
      <StripUnderscores>true</StripUnderscores>
      <QuietenAllCaps>true</QuietenAllCaps>
      <ConnectAs>Default</ConnectAs>
      <UseOciMode>true</UseOciMode>
    </DriverData>
  </Connection>
</Query>

void Main()
{
	string[] installedActivities = {
		"TMS.ActivityPack.Flows"         ,
		"GDTI.ActivityPack.Integration"  ,
		"TMS.ActivityPack.RealTime"      ,
		"Energy.RealTime.Core"           ,
		"Energy.RealTime.Utils"          ,
		"GDTI.ActivityPack.GenericSerie" ,
		"GDTI.ActivityPack.RUP"          ,
		"GDTI.ActivityPack.TernaLink"    ,
		"GDTI.Jobs.TernaGateway"         ,
		"GDTI.ActivityPack.Strategy"     ,
		"Energy.RealTime.Strategy"
	};
	
	var parameters = Parameters
	.Where(x => x.ParameterClass.StartsWith("tms/gatherer/activities/"))
	.GroupBy(x => x.ParameterClass)
	.Select(x => new
	{
		Name = x.Where(y => y.ParameterCd == "activityName").Single().TextValue1,
		Schema = x.Where(y => y.ParameterCd == "schemaName").Single().TextValue1,
		Assembly = x.Where(y => y.ParameterCd == "handlerTypeName").Single().TextValue1
	})
	.ToList()
	.Where(x => !x.Assembly.ContainsAnyOf(installedActivities))
	.Dump()
	;
	
}

// Define other methods and classes here
public static class myExt
{
	public static bool ContainsAnyOf(this string s, IEnumerable<string> coll)
	{
		foreach (var element in coll)
			if(s.Contains(element)) return true;
	
		return false;
	}
}
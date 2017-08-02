<Query Kind="Program">
  <Connection>
    <ID>1b664880-aaa6-49a8-86d0-43deea4970dd</ID>
    <Persist>true</Persist>
    <Driver>EntityFrameworkDbContext</Driver>
    <CustomAssemblyPath>C:\Work\EMODS\DEV\EMODS2\Data\EMODS.Data\bin\Debug\EMODS.Data.dll</CustomAssemblyPath>
    <CustomTypeName>EMODS.Data.EDMWarehouseContainer</CustomTypeName>
    <AppConfigPath>C:\Work\EMODS\DEV\EMODS2\Data\EMODS.Data\bin\Debug\EMODS.Data.dll.config</AppConfigPath>
  </Connection>
</Query>

void Main()
{
	DateTimeOffset myDateTime    = new DateTime(2016, 05, 09, 1, 41, 0);
	DateTimeOffset myEndDateTime = new DateTime(2016, 05, 17, 5, 12, 00);

//	DateTimeOffset myDateTime    = new DateTime(2016, 3, 27, 1, 41, 0);
//	DateTimeOffset myEndDateTime = new DateTime(2016, 3, 27, 5, 12, 0);
	
	var result = SplitRangeIntoChunks(myDateTime, myEndDateTime, TimeSpan.FromDays(1));

	result.Dump();
}

// Define other methods and classes here
public static IEnumerable<Tuple<DateTimeOffset, DateTimeOffset>> SplitRangeIntoChunks(DateTimeOffset start, DateTimeOffset end, TimeSpan chunkSize)
{
	DateTimeOffset chunkEnd;
	
	while ((chunkEnd = RoundDown(start, chunkSize).Add(chunkSize)) < end)
	{
		yield return Tuple.Create(start, chunkEnd);
		start = chunkEnd;
	}
	yield return Tuple.Create(start, end);
}

private static DateTime RoundDown(DateTimeOffset dt, TimeSpan d)
{
	var res_ticks = dt.Ticks - (dt.Ticks % d.Ticks);
	return new DateTime(res_ticks);
}

private static DateTime RoundUp(DateTimeOffset dt, TimeSpan d)
{
	return new DateTime(((dt.Ticks + d.Ticks - 1) / d.Ticks) * d.Ticks);
}
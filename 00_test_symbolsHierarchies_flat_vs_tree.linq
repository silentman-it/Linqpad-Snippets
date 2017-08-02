<Query Kind="Program">
  <Connection>
    <ID>49afb245-820a-4c50-83f6-74657afb1098</ID>
    <Persist>true</Persist>
    <Driver Assembly="IQDriver" PublicKeyToken="5b59726538a49684">IQDriver.IQDriver</Driver>
    <Provider>Devart.Data.Oracle</Provider>
    <CustomCxString>AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAAC5XQXlS+t0aO3JgY8nUl0AAAAAACAAAAAAADZgAAwAAAABAAAADWrX1/vS0dK6HJYC0kXf3LAAAAAASAAACgAAAAEAAAAHuxyd1ceuMjyRA/DQjX7FZAAAAAhM57v2oGo2F9667QXCJ+4GcDybZ6QDNkjceCj1gBwXooJkRrJpfat1/bcN8bLh6/bJYZ7ZXmCi3vDA8i/nch4xQAAABZEl8j2wtPdx1vvLYrZZGrt8a9sw==</CustomCxString>
    <Server>bessie3</Server>
    <UserName>ed_warehouse</UserName>
    <Password>AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAAC5XQXlS+t0aO3JgY8nUl0AAAAAACAAAAAAADZgAAwAAAABAAAAATH9z3zLaP8yzeZ0mi8On5AAAAAASAAACgAAAAEAAAADXtHu4F/htU/kCwFPrlaeUQAAAAiNho9lLO5xEL1dIZEbzDfxQAAABm4CyvofB/K/NyxVqMpG4Ei8CDSA==</Password>
    <EncryptCustomCxString>true</EncryptCustomCxString>
    <DriverData>
      <StripUnderscores>true</StripUnderscores>
      <QuietenAllCaps>true</QuietenAllCaps>
      <ConnectAs>Default</ConnectAs>
      <UseOciMode>true</UseOciMode>
    </DriverData>
  </Connection>
  <Reference>C:\Work\EMODS\DEV\Prototype\EMODS.Data\bin\x64\Debug\TMS.Common.dll</Reference>
  <Namespace>TMS.Common.Extensions</Namespace>
  <Namespace>System.Data.Objects</Namespace>
  <Namespace>TMS.Common.Types</Namespace>
</Query>

void Main()
{
	string root = "UP_TURBIGO_4";
	//string root = null;
	DTreeBuilder<string> build = new DTreeBuilder<string>(root ?? "ROOT");
	var tree = FillRecursive(SymbolsHierarchies, root, build).ToTree();
	
	//tree.DepthFirstNodeEnumerator.Select(x => x.GetNodePathAsString('/')).Dump();
	var aSymbols = tree.DepthFirstEnumerator.ToArray();

	// Dump Series
//	Series
//	.Where(x =>
//	aSymbols.Contains(x.SymbolCd) &&
//	x.SourceCd == "Terna" &&
//	x.ClassCd == "VDT_RUP" &&
//	x.ContextCd == "Dynamic" &&
//	true)
//	.Dump();

	// Dump Details
	SeriesDetails
		.Where(x =>
		aSymbols.Contains(x.Serie.SymbolCd) &&
		x.Serie.SourceCd == "Terna" &&
		x.Serie.ClassCd == "VDT_RUP" &&
		x.Serie.ContextCd == "Dynamic" &&
		x.ValidityStartDate >= DateTime.Parse("21/01/2014") &&
		x.ValidityEndDate <= DateTime.Parse("22/01/2014") &&
		true)
	.Select(x => new 
	{
		Source = x.Serie.SourceCd,
		Symbol = x.Serie.SymbolCd,
		Class = x.Serie.ClassCd,
		Channel = x.Serie.ChannelCd,
		Context = x.Serie.ContextCd,
		StartDate = x.ValidityStartDate,
		EndDate = x.ValidityEndDate,
		Value = x.Value,
		Label = x.Label
	})
	.OrderBy(x => x.Source)
	.ThenBy(x => x.Symbol)
	.ThenBy(x => x.Class)
	.ThenBy(x => x.Channel)
	.ThenBy(x => x.Context)
	.ThenBy(x => x.StartDate)
	
	//.ToPivotTable(x => x.StartDate, x => string.Format("{0}/{1}/{2}/{3}", x.Source, x.Symbol, x.Class, x.Channel), x => x.FirstOrDefault().Value.Value)
	.Dump();	
}

// Define other methods and classes here

DTreeBuilder<string> FillRecursive(IEnumerable<SymbolsHierarchies> flatObjects, string parentId, DTreeBuilder<string> builder)
{
	var ls = flatObjects.Where(x => x.ParentSymbolCd == parentId).ToList();
	foreach (var item in ls)
	{
		builder = builder.AddWithChild(item.SymbolCd);
		builder = FillRecursive(flatObjects, item.SymbolCd, builder);
		builder = builder.Up();
	}
	return builder;
}
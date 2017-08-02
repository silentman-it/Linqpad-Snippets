<Query Kind="Statements">
  <Connection>
    <ID>13ea5274-7303-4ca0-adfa-ff4b1e31ef01</ID>
    <Persist>true</Persist>
    <Driver Assembly="IQDriver" PublicKeyToken="5b59726538a49684">IQDriver.IQDriver</Driver>
    <Provider>Devart.Data.Oracle</Provider>
    <CustomCxString>AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAAXw9k02xJM0uVTAPS/QGCJwAAAAACAAAAAAAQZgAAAAEAACAAAACSqb2p/I5Aqrgbx2Fz7IC79yTUYgOfKEwbSE11dPNWSAAAAAAOgAAAAAIAACAAAAAmCMzYXsuguBTxlBhZ9NVYmSah89iaDA4oBeSner7KIEAAAACBtLrTxL6aKWT4O0UZMD51sTw2p+p/MB2F2EzF3kHl7y/fqDafp9bUHC33qvxLPg2vv1/h9ID8ZyI7QR/X8xXeQAAAAFFV4G4zbV6Xdicyru5UZ1m6Rm2SPqCgpzjnqKAlFnjKceUrRocQdaY33PYl9eJbUR3qQ0pGaay7adTEUOv/kJE=</CustomCxString>
    <Server>svil5</Server>
    <EncryptCustomCxString>true</EncryptCustomCxString>
    <UserName>ed_warehouse</UserName>
    <Password>AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAAXw9k02xJM0uVTAPS/QGCJwAAAAACAAAAAAAQZgAAAAEAACAAAABmlxfbKt4ZoQnbKozLAz/6zUZPqrjrjpYt6oAh2C28KAAAAAAOgAAAAAIAACAAAAA8kZlWVpdPGsC07mdTDMNplEZJLFO8GFBgb9YIyaoj7BAAAACfh9wHdMnLZLQvLGQEwIL+QAAAALOusXWPr7mOG8HtzMzSLIR1z+E8qWVVTrPDoxENFqND4/te+D+rdGqIdR6uhETbxyKUhOZHjXGMG/WwpL8xxq4=</Password>
    <DriverData>
      <StripUnderscores>true</StripUnderscores>
      <QuietenAllCaps>true</QuietenAllCaps>
      <ConnectAs>Default</ConnectAs>
      <UseOciMode>true</UseOciMode>
    </DriverData>
  </Connection>
  <Output>DataGrids</Output>
</Query>

string[] regularFrequencies = { "MINUTE", "FIFTEEN_MINUTES", "HOUR", "DAY" };

string sDate1 = "01/01/2016";
string sDate2 = "31/12/2016";

SeriesDetails
.Where(x =>
	//x.Serie.SourceCd == "Terna" &&
	//x.Serie.SymbolCd.StartsWith("UP_TRRVLDLIGA_5") &&
	//x.Serie.ClassCd == "BindingProgram" &&
	//x.Serie.ChannelCd == "PV" &&
	
	regularFrequencies.Contains(x.Serie.FrequencyCd) &&
	x.Serie.Historicized == "N" &&
	true)
//.ToList()
.Where(x =>
	x.ValidityStartDate >= DateTime.Parse(sDate1) &&
	x.ValidityStartDate < DateTime.Parse(sDate2).AddDays(1) &&
	true)
.Select(x => new 
{
	StreamID = x.StreamID,
	SerieID = x.SerieID,
	GenerationCd = x.GenerationCd,
	Source = x.Serie.SourceCd,
	Symbol = x.Serie.SymbolCd,
	Class = x.Serie.ClassCd,
	Channel = x.Serie.ChannelCd,
	Context = x.Serie.ContextCd,
	Freq = x.Serie.FrequencyCd,
	Historicized = x.Serie.Historicized,
	StartDate = x.ValidityStartDate,
	StartDateOffset = x.ValidityStartDateOffset,
	EndDate = x.ValidityEndDate,	
	EndDateOffset = x.ValidityEndDateOffset,
	Value = x.Value,
	Label = x.Label,
	TransactionStartTS = x.TransactionStartTs,
	TransactionEndTS = x.TransactionEndTs
})

.OrderBy(x => x.TransactionStartTS)
.ThenBy(x => x.TransactionEndTS)
.ThenBy(x => x.Symbol)
.ThenBy(x => x.Class)
.ThenBy(x => x.Channel)
.ThenBy(x => x.Context)
.ThenBy(x => x.StartDate)

.ToList()
.GroupBy(x => new {
	x.SerieID, x.StartDate, x.StartDateOffset, x.EndDate, x.EndDateOffset
})
.Where(x => x.Count() > 1)
.Select(x => new {
	SerieID = x.Key.SerieID,
	Source = x.First().Source,
	Symbol = x.First().Symbol,
	Class = x.First().Class,
	Channel = x.First().Channel,
	Context = x.First().Context,
	N = x.Count(),
	Lista = x.ToList()
})


// Per vedere quali date/unita ci sono
//.ToList()
//.Select(x => new {
//	Symbol = x.Symbol,
//	StartDate = x.StartDate.Date.ToString("dd MMM yyyy")
//})
//.Distinct()

//.ToPivotTable(x => x.StartDate, x => string.Format("{0}/{1}/{2}/{3}", x.Source, x.Symbol, x.Class, x.Channel), x => x.FirstOrDefault().Value.Value)

// ------------------ CHECK DUPS ------------------
//.ToList()
//.GroupBy(x => new {
//	x.Source,
//	x.Symbol,
//	x.Class,
//	x.Channel,
//	x.Context,
//	x.StartDate,
//	x.StartDateOffset,
//	x.EndDate,
//	x.EndDateOffset,
////	x.TransactionStartTS,
//	x.TransactionEndTS
//})
//.Where(x => x.Count() > 1)
// ------------------ CHECK DUPS ------------------

.Dump();
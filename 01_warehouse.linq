<Query Kind="Statements">
  <Connection>
    <ID>c0c91c78-8047-4492-bbdd-d510641d316e</ID>
    <Persist>true</Persist>
    <Driver Assembly="IQDriver" PublicKeyToken="5b59726538a49684">IQDriver.IQDriver</Driver>
    <Provider>Devart.Data.Oracle</Provider>
    <CustomCxString>AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAAyhhI3zxfB0Gx1rxqxf78uQAAAAACAAAAAAAQZgAAAAEAACAAAAAaREcW/v07jNgTwazIqDznnJxPFLRGJR0taz9KLXm+nwAAAAAOgAAAAAIAACAAAAB+QKlhLQQe3NcQp0OnanPGLi57sDQ5x2NmalF9TMXOc0AAAACkkXYPZyvXieasxRY54ndFXTFqGORUCYImf2Smdmg6IQgLFlAGVkKT9Mh/1EnWY4FKyNrOcfhTJxDmfbyA/AxfQAAAABRqqFXfPfCpjdEbuWofrC+TvHtC3Rp61q7OejpVnqxxtElBE4OmXJcvg0GBRqHTLAPUiq8kOTlRQSILQ/tLvxE=</CustomCxString>
    <Server>gdti_tp</Server>
    <UserName>ed_warehouse</UserName>
    <EncryptCustomCxString>true</EncryptCustomCxString>
    <NoPluralization>true</NoPluralization>
    <Password>AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAAac45e5C3qUmtB10kiS8YJQAAAAACAAAAAAAQZgAAAAEAACAAAACSMu+0e56ZhvPoemC+3moswsn3s9MFcLPb4m085H0AuAAAAAAOgAAAAAIAACAAAAAfODuzoXOOenpBDCeJtgv4AJrq1iV1tFPom9fPbnVhjRAAAAAu9WuQqEH5vIp34aercMRyQAAAAD0uydppG2tU7KRVPVaETESRMmv3iv9RWLbLZ4NSZC/bF1DCA0GvEUddMnKeyGDnIjIt3YZS/xpfeGImUjAqRTc=</Password>
    <DisplayName>gdti_tp.ed_warehouse</DisplayName>
    <DriverData>
      <StripUnderscores>true</StripUnderscores>
      <QuietenAllCaps>true</QuietenAllCaps>
      <ConnectAs>Default</ConnectAs>
      <UseOciMode>true</UseOciMode>
    </DriverData>
  </Connection>
  <Output>DataGrids</Output>
</Query>

string sDate = "19/05/2016";

SeriesDetails
.Where(x =>
	x.Serie.SourceCd == "Plant" &&
	x.Serie.SymbolCd == "UP_NAPOLIL_4" &&
	x.Serie.ClassCd == "InstantMeasure" &&
	//x.Serie.ChannelCd == "PVMC" &&
	true)
//.ToList()
.Where(x =>
	x.ValidityStartDate >= DateTime.Parse(sDate) &&
	x.ValidityStartDate < DateTime.Parse(sDate).AddDays(1) &&
	true)
.Select(x => new 
{
	SerieID = x.SerieID,
	StreamID = x.StreamID,
	GenerationCd = x.GenerationCd,
	Source = x.Serie.SourceCd,
	Symbol = x.Serie.SymbolCd,
	Class = x.Serie.ClassCd,
	Channel = x.Serie.ChannelCd,
	Context = x.Serie.ContextCd,
	Hist = x.Serie.Historicized,
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

//.ToList()
//.GroupBy(x => new {
//	x.SerieID,
//	x.StartDate
//})


.Dump();
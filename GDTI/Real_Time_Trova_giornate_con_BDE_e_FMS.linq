<Query Kind="Statements">
  <Connection>
    <ID>0ed57ccc-0ed8-4619-a75c-b012ef2d7699</ID>
    <Persist>true</Persist>
    <Driver Assembly="IQDriver" PublicKeyToken="5b59726538a49684">IQDriver.IQDriver</Driver>
    <Provider>Devart.Data.Oracle</Provider>
    <CustomCxString>AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAAkOYp3WDye0ORXtly5hEZsAAAAAACAAAAAAAQZgAAAAEAACAAAADxNN+JVm6755IypkVtlT4rgI255UfxVq8lSyvTotaisAAAAAAOgAAAAAIAACAAAAA+Zyb1t3XUZ4fCKj7Q3nJxjUTbmMkVF4ccLCXmPv1HA1AAAACR4sP//i25pmgXVfgs1tbo6e2mxy1gvAJA7N32XosuHC11LP8m3+vdU9uAIriTxv+fvMFHdmqC9P+DMlOFLoCnMR1Tld5HqyTLskOf2FFL3kAAAAD0Vjk56MiikCbFCB8cc1pqbWFd+3c+xmXGe0kGU1NAcAfkFezLIF4FCXswqfm5c2E6GkHpSHbFG7nVp3zWtAJj</CustomCxString>
    <Server>win2008r2test2</Server>
    <EncryptCustomCxString>true</EncryptCustomCxString>
    <DisplayName>win2008r2test2.ed_warehouse</DisplayName>
    <UserName>ed_warehouse</UserName>
    <Password>AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAAg4VPWvAGA0K8d2W/MBVMggAAAAACAAAAAAAQZgAAAAEAACAAAAC7h7/cZm/4xnKUUbteeWDXgzflTLeUAzNSBQtjf8V/uQAAAAAOgAAAAAIAACAAAADF9uIiwH1UV8MNRpaGxUPdRHZA6UsGW5DNmlDavj1uMBAAAADiD4sb59R+MLfU7VwLb0LCQAAAAIoRGCSOOeTtpYxXn1zNUoaUTuhYm6qr3+F7qX/pPvPFJlTTm9AfWYY4WsiCBOSLdti+xPbsgrVifrKI3CzwpjA=</Password>
    <DriverData>
      <StripUnderscores>true</StripUnderscores>
      <QuietenAllCaps>true</QuietenAllCaps>
      <ConnectAs>Default</ConnectAs>
      <UseOciMode>true</UseOciMode>
    </DriverData>
  </Connection>
  <Output>DataGrids</Output>
  <Namespace>System.Data.Objects</Namespace>
</Query>

var lsFMS = SeriesDetails
.Where(x =>
	x.Serie.SourceCd == "GME" &&
	x.Serie.ClassCd == "BindingProgram" &&
	x.Serie.ChannelCd == "FMS" &&
	true)
.Select(x => new 
{
	StreamID = x.StreamID,
	GenerationCd = x.GenerationCd,
	Source = x.Serie.SourceCd,
	Symbol = x.Serie.SymbolCd,
	Class = x.Serie.ClassCd,
	Channel = x.Serie.ChannelCd,
	Context = x.Serie.ContextCd,
	StartDate = x.ValidityStartDate,
	EndDate = x.ValidityEndDate,
	Value = x.Value,
	Label = x.Label,
	TransactionTS = x.TransactionStartTs
})
.OrderBy(x => x.TransactionTS)
.ThenBy(x => x.Symbol)
.ThenBy(x => x.Class)
.ThenBy(x => x.Channel)
.ThenBy(x => x.Context)
.ThenBy(x => x.StartDate)
.ToList()
.Select(x => new {
	Type = "FMS",
	Symbol = x.Symbol,
	StartDate = x.StartDate.Date.ToString("yyyyMMdd")
})
.Distinct()

.Dump();

var lsBDE = SeriesDetails
.Where(x =>
	//x.Serie.SourceCd == "GME" &&
	//x.Serie.SymbolCd.StartsWith("UP_TURBIGO_4") &&
	x.Serie.ClassCd == "DispatchOrderMessage" &&
	true)
.Select(x => new 
{
	StreamID = x.StreamID,
	GenerationCd = x.GenerationCd,
	Source = x.Serie.SourceCd,
	Symbol = x.Serie.SymbolCd,
	Class = x.Serie.ClassCd,
	Channel = x.Serie.ChannelCd,
	Context = x.Serie.ContextCd,
	StartDate = x.ValidityStartDate,
	EndDate = x.ValidityEndDate,
	Value = x.Value,
	Label = x.Label,
	TransactionTS = x.TransactionStartTs
})
.OrderBy(x => x.TransactionTS)
.ThenBy(x => x.Symbol)
.ThenBy(x => x.Class)
.ThenBy(x => x.Channel)
.ThenBy(x => x.Context)
.ThenBy(x => x.StartDate)
.ToList()
.Select(x => new {
	Type = "BDE",
	Symbol = x.Symbol,
	StartDate = x.StartDate.Date.ToString("yyyyMMdd")
})
.Distinct()
.Dump()
;


var lsJ = lsBDE.Join(lsFMS, x => new { x.StartDate, x.Symbol }, x => new { x.StartDate, x.Symbol }, (x,y) => new { x.StartDate, x.Symbol }).Dump();
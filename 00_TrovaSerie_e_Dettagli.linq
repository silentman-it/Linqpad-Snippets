<Query Kind="Statements">
  <Connection>
    <ID>19bf2bdc-4e48-4a27-8303-e8bba9a3a113</ID>
    <Persist>true</Persist>
    <Driver Assembly="IQDriver" PublicKeyToken="5b59726538a49684">IQDriver.IQDriver</Driver>
    <Provider>Devart.Data.Oracle</Provider>
    <CustomCxString>AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAAg4VPWvAGA0K8d2W/MBVMggAAAAACAAAAAAAQZgAAAAEAACAAAAB5+dg7ce7N2OIKjlzuCxIsiB1ZOSCk+f162xYyPGi/ZQAAAAAOgAAAAAIAACAAAAATVeksXKZ2T7nLZGEMcpDEQ2FHHJUiLk4DZStm+NVyX0AAAACn/0arCv5bzP1sx9wMecLmK7Uvij2GCfzcgrrljCWD2NCUBLtLcgtrTl76nPkyvAKNPCZlBkhglrCIj5UvIbPSQAAAAPKuKL8JhSU93kGq1GdzT/8aGb5JFPmNqphjKLUun56xu5ZL18soi2X5fqsMrntRXJ3mRdsSTDXDad5f221TOPM=</CustomCxString>
    <Server>svil4</Server>
    <EncryptCustomCxString>true</EncryptCustomCxString>
    <DisplayName>svil4.ed_warehouse</DisplayName>
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
</Query>

string Source = "Plant";
string Symbol = "UP_TURBIGO_4";
string Class = "InstantMeasure";
string Channel = null;
string Context = null;
decimal? PrimaryKey = null;
decimal? SecondaryKey = null;
DateTime? theDate = null;

//theDate = DateTime.Parse("18/07/2014");

var lsSeries =
Series
.Where( x =>
	(Source != null ? x.SourceCd == Source : true)    &&
	(Symbol != null ? x.SymbolCd == Symbol : true)    &&
	(Class != null ? x.ClassCd == Class : true)       &&
	(Channel != null ? x.ChannelCd == Channel : true) &&
	(Context != null ? x.ContextCd == Context : true) &&
	(PrimaryKey != null ? x.PrimaryKey == PrimaryKey : true) &&
	(SecondaryKey != null ? x.SecondaryKey == SecondaryKey : true)
	)
//.Dump()
;

var lsSid = lsSeries.Select(x => x.SerieID);

SeriesDetails
.Where(x => lsSid.Contains(x.SerieID))
.ToList()
.Where(x => theDate.HasValue ? (x.ValidityStartDate.Date == theDate.Value) : true)
.Join(lsSeries, o => o.SerieID, i => i.SerieID, (i, o) => new { SerieId = o.SerieID, Serie = o, SerieDetails = i })
.ToList()
.GroupBy(x => x.SerieId)
.Select(x => new
{
	SerieId = x.Key,
	Serie = x.First().Serie,
	SerieDetails = x.Select(sd => sd.SerieDetails)
})
.Dump();
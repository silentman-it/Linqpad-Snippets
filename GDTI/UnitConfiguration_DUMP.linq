<Query Kind="Statements">
  <Connection>
    <ID>375ac341-be79-4385-a67a-9e65e9c57c4a</ID>
    <Persist>true</Persist>
    <Driver Assembly="IQDriver" PublicKeyToken="5b59726538a49684">IQDriver.IQDriver</Driver>
    <Provider>Devart.Data.Oracle</Provider>
    <CustomCxString>AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAA1t9zCdZ1H0aBMXh1HHDywAAAAAACAAAAAAADZgAAwAAAABAAAAB4E5bUbcnoeu05ag+6KGEhAAAAAASAAACgAAAAEAAAAPPYa/hmLyF6jDD7KIOFmqxAAAAApzIqEkexqx+50iEpS3oJFBwK03HGDys1k71vPJW+PX9v/+VqvGkJWoKOlaRYT4nkDrYX543hEZbQQAu5V9PjYhQAAACG+myFRojWeY+enhMHyRVbXzbstg==</CustomCxString>
    <Server>gdtisvil</Server>
    <UserName>ed_warehouse</UserName>
    <Password>AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAA1t9zCdZ1H0aBMXh1HHDywAAAAAACAAAAAAADZgAAwAAAABAAAAAzY0GnnrUv+pqbPVptPVA8AAAAAASAAACgAAAAEAAAAFYwGPZA+7vniB1KtxgLKecQAAAAfYm3ZFTICeARdk3nGvJ8mRQAAAB0/EHg2lBLyFxLVZq6BLF28fhZgw==</Password>
    <EncryptCustomCxString>true</EncryptCustomCxString>
    <DriverData>
      <StripUnderscores>true</StripUnderscores>
      <QuietenAllCaps>true</QuietenAllCaps>
      <ConnectAs>Default</ConnectAs>
      <UseOciMode>true</UseOciMode>
    </DriverData>
  </Connection>
  <Output>DataGrids</Output>
  <Reference>C:\Work\EMODS\DEV\Prototype\EMODS.Data\bin\x64\Debug\TMS.Common.dll</Reference>
  <Namespace>TMS.Common.Extensions</Namespace>
  <Namespace>System.Data.Objects</Namespace>
</Query>

SeriesDetails
.Where(x =>
	x.Serie.SourceCd == "Configuration" &&
	//x.Serie.SymbolCd == "UP_BUSSENTO_1" &&
	//x.Serie.ClassCd == "UnitType" &&
	x.ValidityStartDate <= DateTime.Now && x.ValidityEndDate >= DateTime.Now &&
	true)
.ToList()
.Select(x => new 
{
	StreamID = x.StreamID,
	GenerationCd = x.GenerationCd,
	Source = x.Serie.SourceCd,
	Unit = x.Serie.SymbolCd,
	Key = x.Serie.ClassCd,
	StartDate = x.ValidityStartDate,
	EndDate = x.ValidityEndDate,
	Value = x.Value.HasValue ? x.Value.ToString() : x.Label,
	TransactionTS = x.TransactionStartTs
})
.GroupBy(x => new { x.Unit, x.Key })
.Select(x => new
{
	Unit = x.Key.Unit,
	Key = x.Key.Key,
	LastValue = x.OrderByDescending(y => y.TransactionTS).FirstOrDefault().Value,
	TS = x.OrderByDescending(y => y.TransactionTS).FirstOrDefault().TransactionTS
})
.OrderBy(x => x.Unit)
.ThenBy(x => x.Key)
.Dump();
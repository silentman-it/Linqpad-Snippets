<Query Kind="Statements">
  <Connection>
    <ID>f4fade70-c966-476d-beae-c8551408b0fe</ID>
    <Persist>true</Persist>
    <Driver Assembly="IQDriver" PublicKeyToken="5b59726538a49684">IQDriver.IQDriver</Driver>
    <Provider>Devart.Data.Oracle</Provider>
    <CustomCxString>AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAAExu2soIgZEOm6KkOCtfZlwAAAAACAAAAAAAQZgAAAAEAACAAAACkvMSloUwPI+I5V2D1oWxtbmyLJTjOeZB+yt+mLfGUxgAAAAAOgAAAAAIAACAAAAD8nFmrW1z0zgjJRsM/6opBie3YP3+UjicsuCTvCdb4K0AAAAAKpLjLORMlKE0S68NravrMZ5FVXv9DWR/mvvGJbayfYjI4vzXeUYGQOq5VI02aJgdhc0Po2Dd2nw8pBS+42tNyQAAAAHNWreGOzpzuJ44KoLwIN5yb+B7k/eyAmDs6miismkw8ScBFWuKfH/bbC0J1jC/h2EZCTFA00rFbCCGVUeuh/W4=</CustomCxString>
    <Server>bat_erg_dev</Server>
    <EncryptCustomCxString>true</EncryptCustomCxString>
    <NoPluralization>true</NoPluralization>
    <DisplayName>bat_erg_dev.bat_dev</DisplayName>
    <UserName>bat_dev</UserName>
    <Password>AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAAExu2soIgZEOm6KkOCtfZlwAAAAACAAAAAAAQZgAAAAEAACAAAABFkzQRTT5nZaam83dNqGpcIoZF2hrWYJV8tQJkuajZmAAAAAAOgAAAAAIAACAAAADUA7+ci6o95muhSCkZXaz1+ctERh0CTXSwiAx9GsVAdhAAAADjQxKv7HFlmoXv1O8lJSHmQAAAALMkLQcRAH4eKZoN1Bz3pp9oa/k85p56d9hvmCAF+wyiP5DmvHwAA+TiApIdXBfpT39zBz0oNsFbcVy/c3T6IZk=</Password>
    <DriverData>
      <StripUnderscores>true</StripUnderscores>
      <QuietenAllCaps>true</QuietenAllCaps>
      <ConnectAs>Default</ConnectAs>
      <UseOciMode>true</UseOciMode>
    </DriverData>
  </Connection>
</Query>

var ls = BatInvoiceMappings
	.Where(x => x.SourceType == "ETRM")
	.Select(x => new {
		Purpose = x.TransactionPurpose,
		Account = x.Account,		
		TradeType = x.TradeType
	})
	.Distinct()
	.Dump();
	
foreach (var i in ls)
{
	if(i.Purpose == "Buy")
	{
		BatCostCenters.InsertOnSubmit(new BatCostCenters {
			CogeAccount = i.Account,
			TradeType = i.TradeType,
			CostCenter = "FILL_ME"
		});
	}
	else
	{
		BatProfitCenters.InsertOnSubmit(new BatProfitCenters {
			SellAccount = i.Account,
			TradeType = i.TradeType,
			ProfitCenter = "FILL_ME"
		});
	}
	
}

SubmitChanges();
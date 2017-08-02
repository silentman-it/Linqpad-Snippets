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
  <NuGetReference>ClosedXML</NuGetReference>
  <Namespace>ClosedXML.Excel</Namespace>
</Query>

var itemsToRemove = BatInvoiceMappings.Where(x => x.SourceType == "EnBO");
foreach (var item in itemsToRemove)
{
	BatInvoiceMappings.DeleteOnSubmit(item);	
}

List<BatInvoiceMappings> ls = new List<BatInvoiceMappings>();
var xlsFileName = @"C:\Users\Federico\Desktop\Mappatura GME e Terna_con_mapping_EnBO_4.0_per BAT 2.0.xlsx";

var workbook = new XLWorkbook(xlsFileName);
var ws1 = workbook.Worksheet(2); 



IXLRow r = ws1.Row(8);
while(!r.IsEmpty())
{
	var commodity = r.Cell("J").Value.ToString();
	var prodotto =  r.Cell("J").Value.ToString();
	var tradeType =  r.Cell("K").Value.ToString();
	var codiceFlussoEconomico =  r.Cell("L").Value.ToString();
	var buyOrSell =  r.Cell("H").Value.ToString().Trim().ToLower() == "attiva" ? "SELL" : "BUY";
	var conto = buyOrSell == "BUY"   ? r.Cell("M").Value.ToString() : r.Cell("N").Value.ToString();
	var codiceMateriale = r.Cell("O").Value.ToString();
	var movimentoMagazzino = !r.Cell("P").IsEmpty();
	
	var controparte = r.Cell("C").Value.ToString();
	var corrispettivo =  Convert.ToDecimal(r.Cell("F").Value);
	var codiceCliente = r.Cell("Q").Value.ToString();
	
	var accountingCenter = buyOrSell == "BUY" ? r.Cell("T").Value.ToString() : r.Cell("U").Value.ToString();
	
	r = r.RowBelow();
	
	BatInvoiceMappings bim = new BatInvoiceMappings() {
		SourceType = "EnBO",
		IntercompanyFlag = 0,
		ItemType = corrispettivo.ToString(),
		Commodity = commodity,
		Market = "ITALY",
		Product = prodotto,		
		CounterpartCode = codiceCliente,
		TradeType = tradeType,		
		Account = conto,
		TransactionPurpose = buyOrSell,
		MaterialCode = codiceMateriale,
		TransactionCode = codiceFlussoEconomico,
		GoodsMovement = movimentoMagazzino ? 1 : 0,
		AccountingCenter = null
	};

	ls.Add(bim);
	
	BatInvoiceMappings.InsertOrUpdateOnSubmit(bim);
	
}

ls.Dump();

SubmitChanges();
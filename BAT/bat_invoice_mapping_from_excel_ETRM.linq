<Query Kind="Statements">
  <Connection>
    <ID>ee4a399f-2c42-4996-a7dc-d4d4e7237141</ID>
    <Persist>true</Persist>
    <Driver Assembly="IQDriver" PublicKeyToken="5b59726538a49684">IQDriver.IQDriver</Driver>
    <Provider>Devart.Data.Oracle</Provider>
    <CustomCxString>AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAAg4VPWvAGA0K8d2W/MBVMggAAAAACAAAAAAAQZgAAAAEAACAAAADglTTx3C11jhPYnlj4yIn6F12XUCZG0jmhAgSxvy/GwAAAAAAOgAAAAAIAACAAAAAkpGpkXXL/W/KeyPCYOXNDtzoDfHW1+MJF9HRtISTTTzAAAABE7dxKTiZ2F5FPsgCw2UdeJFhZd6h0jm1G6uaN7GxpocLSPaMM8x0TNmcKnyedBNNAAAAAjEojooi7KINAFNFqDlqlA0kcGPNaovF/EROD2VWCi3/qgTuH/J9pCzupmAUfrrruAY27Lc6OXvlQ5S+J3P29yg==</CustomCxString>
    <Server>svil11</Server>
    <EncryptCustomCxString>true</EncryptCustomCxString>
    <NoPluralization>true</NoPluralization>
    <Password>AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAAg4VPWvAGA0K8d2W/MBVMggAAAAACAAAAAAAQZgAAAAEAACAAAAAsKI7PIeRb6nHL+Gvkk3laid/R6+o/PbO4YTeARPNJPQAAAAAOgAAAAAIAACAAAAABtp4lAH0bq0jHK0mHbRIAzMHx65wZrHVW6S9GVu6YaBAAAAC/G6+IFqdxCIM2menwE9AMQAAAACjN95MAN7Pa/6aRuTFlUvreV8mavBrvEJy9GDbt1qRmNMUry7OMWRtaSDA8zFyKKReQTq6MfMMaLBA3mLtpm3M=</Password>
    <DisplayName>svil11.bat</DisplayName>
    <UserName>bat</UserName>
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

var itemsToRemove = BatInvoiceMappings.Where(x => x.SourceType == "ETRM");
foreach (var item in itemsToRemove)
{
	BatInvoiceMappings.DeleteOnSubmit(item);	
}

List<BatInvoiceMappings> ls = new List<BatInvoiceMappings>();
//var xlsFileName = @"C:\Users\Federico\Desktop\Mappatura ETRM-BAT 2.1_rev3 _ Bozza.xlsx";
var xlsFileName = @"\\edggepwsv004.edg.grptop.net\Repository ENY\AA DOSSIER ASSISTENZA\_Clienti\ERG\Attivita'\Integrazione_ETRM_REMIT_SAP\SAP\Mappatura ETRM-BAT 2.1\Definitiva predisposta da Tina Torchio-ERG\Mappatura ETRM-SAP marzo .xlsx";

var workbook = new XLWorkbook(xlsFileName);

IXLWorksheet ws1;
var found = workbook.TryGetWorksheet("Mappatura ETRM", out ws1); 

if(!found) throw new Exception("Worksheet not found!");

IXLRow r = ws1.Row(2);
while(!r.IsEmpty())
{
	var commodity = r.Cell("H").Value.ToString();
	var market = r.Cell("I").Value.ToString();
	var buyOrSell = r.Cell("J").Value.ToString().Trim().ToUpper();
	var priceName = r.Cell("K").Value.ToString().Trim();
	var delivery = r.Cell("Q").Value.ToString().Trim();
	var proposerName = r.Cell("S").Value.ToString().Trim();
	var intercompanyFlag = !r.Cell("R").IsEmpty() && r.Cell("R").Value.ToString().ToUpper().Trim() != "NO";
	var account = r.Cell("T").Value.ToString();
	var codiceMateriale = r.Cell("U").Value.ToString();
	var movimentoMagazzino = !r.Cell("V").IsEmpty();
	var centroDiCosto = r.Cell("W").Value.ToString().Trim();
	var centroDiProfitto = r.Cell("X").Value.ToString().Trim();
	
	//var isOK = !r.Cell("Y").IsEmpty() && r.Cell("Y").Value.ToString().ToUpper().Trim() != "NO";	
	var isOK = true;
	
	r = r.RowBelow();
	
	if(!isOK)
	{
		continue;
	}
	
	BatInvoiceMappings bim = new BatInvoiceMappings() {
		SourceType = "ETRM",
		IntercompanyFlag = intercompanyFlag ? 1 : 0,
		Commodity = commodity,
		Market = market,
		DeliveryType = delivery,
		ProposerName = proposerName,
		CounterpartCode = null,
		Account = account,
		TransactionPurpose = buyOrSell,
		MaterialCode = codiceMateriale,
		TransactionCode = priceName,
		GoodsMovement = movimentoMagazzino ? 1 : 0,
		AccountingCenter = buyOrSell == "BUY" ? centroDiCosto : centroDiProfitto
	};

	ls.Add(bim);
	
	BatInvoiceMappings.InsertOrUpdateOnSubmit(bim);
	
}

ls.Dump();

SubmitChanges();
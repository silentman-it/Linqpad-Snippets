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
</Query>

var dtFrom = DateTime.Parse("01/01/2016");
var dtTo   = DateTime.Parse("01/02/2016");

var etrmOrderItems = EtrmOrderItems
	.Where(x => x.CompetenceDate >= dtFrom && x.CompetenceDate <= dtTo)
	.ToList();
var mappings = BatInvoiceMappings.ToList();

etrmOrderItems.LeftOuterJoin(mappings,
	o => new
	{
		Source = "ETRM",
		Commodity = o.Commodity,
		Market = o.Market,
		Delivery = o.Delivery,
		Proposer = o.Proposer,
		Intercompany = (o.IntercompanyFlag.Trim().ToUpper() == "Y"),
		Purpose = o.Purpose.ToUpper().Trim(),
		Price = o.PriceName
	},
	i => new
	{
		Source = i.SourceType,
		Commodity = i.Commodity,
		Market = i.Market,
		Delivery = i.DeliveryType,
		Proposer = i.ProposerName,
		Intercompany = (i.IntercompanyFlag != 0),
		Purpose = i.TransactionPurpose.ToUpper().Trim(),
		Price = i.TransactionCode
	},
	(o,i) => new
	{
		OrderItem = o,
		Mapping = i,
		OK = (i != null)
	})
	.Dump();
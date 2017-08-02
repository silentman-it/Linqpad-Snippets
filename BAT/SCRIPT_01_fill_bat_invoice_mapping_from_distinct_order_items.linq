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

var ls = EtrmOrderItems
.Select(x => new {
	x.DealID,
	x.Commodity,
	x.ProductName,
	x.DealType,
	x.PriceName,
	x.Purpose
})
.Distinct()
.Dump();


foreach (var i in ls)
{
	BatInvoiceMappings bim = new BatInvoiceMappings()
	{
		SourceType = "ETRM",
		Commodity = i.Commodity,
		Product = i.ProductName,
		TradeType = i.DealType,
		TransactionCode = i.PriceName,
		TransactionPurpose = i.Purpose,
		Account = "FILL_ME",
		MaterialCode = "FILL_ME",
		GoodsMovement = 1
	};
	
	BatInvoiceMappings.InsertOnSubmit(bim);
	
	
}

SubmitChanges();
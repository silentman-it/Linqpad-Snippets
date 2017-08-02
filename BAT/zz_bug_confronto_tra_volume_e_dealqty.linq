<Query Kind="Statements">
  <Connection>
    <ID>c113dd3e-82bf-428b-b875-fa97865b9c8c</ID>
    <Persist>true</Persist>
    <Driver Assembly="IQDriver" PublicKeyToken="5b59726538a49684">IQDriver.IQDriver</Driver>
    <Provider>Devart.Data.Oracle</Provider>
    <CustomCxString>AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAAExu2soIgZEOm6KkOCtfZlwAAAAACAAAAAAAQZgAAAAEAACAAAADbHhbgKdiPXuOGO5QQI9hO9IUjEaw3eBY/Rst7OhVK5AAAAAAOgAAAAAIAACAAAAC6d11NBa2rVWMWpmmrnX1uHNte6jQ7HzbQjF3hKj60FzAAAABWlUjcMx6ufqndNdlZ1ot+vZPCic4K6ns+L5RdAAVo4vMVr/DpFxjPFVL5I1zl4u9AAAAAQ7ee+BDYykrRqpE2lQwXhPprJq4O9na+YyIAZPL3qM2l7SxkcCV5+3xfrago4HonT7Dz6bXmnvd5gomKgA8OcA==</CustomCxString>
    <Server>bat_erg_dev</Server>
    <EncryptCustomCxString>true</EncryptCustomCxString>
    <NoPluralization>true</NoPluralization>
    <DisplayName>bat_erg_test.bat</DisplayName>
    <UserName>bat</UserName>
    <Password>AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAAExu2soIgZEOm6KkOCtfZlwAAAAACAAAAAAAQZgAAAAEAACAAAACzOEHMq7BtLwMM4DpkupXcHVJtmm1JGkzgvd1UN05hewAAAAAOgAAAAAIAACAAAAB/Gp9Gz33Yc6/LAk1H1sJJa35WANq58NS8qxGkt8w3VxAAAADO2R+jHA3pyqMKX1zUuaD+QAAAAOENaMi5rsozuvp5b8tQQmUVr9uqN7pWxOE0eTgr9z2uHJqhRKiDqBt+j06cuQMbCj7a0uH2YoQ4bserAWWz4M4=</Password>
    <DriverData>
      <StripUnderscores>true</StripUnderscores>
      <QuietenAllCaps>true</QuietenAllCaps>
      <ConnectAs>Default</ConnectAs>
      <UseOciMode>true</UseOciMode>
    </DriverData>
  </Connection>
</Query>

BatInvoices
.Where(x => x.Source == "ETRM" && x.SendState == "Sent")
.Join(EtrmOrderItems, bi => bi.TransactionID, eoi => eoi.OrderItemID, (bi, eoi) => new {  BatInvoice = bi, EtrmOrderItem = eoi })
.Join(ErgTransactions, o => o.BatInvoice.TransactionID, et => et.IdTransactionXdm, (o, et) => new { BatInvoice = o.BatInvoice, EtrmOrderItem = o.EtrmOrderItem, ErgTransaction = et })
//.Where(x => x.EtrmOrderItem.Volume.ToString() == x.ErgTransaction.Quantita && x.ErgTransaction.Quantita != "1")
.Select(x => new {
	OrderItemId = x.EtrmOrderItem.OrderItemID,
	DealId = x.EtrmOrderItem.DealID,
	EtrmOrderItem_DealQty = x.EtrmOrderItem.DealQuantity,
	EtrmOrderItem_Volume = x.EtrmOrderItem.Volume,
	ErgTransaction_Qty = x.ErgTransaction.Quantita })
.Dump();
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

BatInvoices
     .Where(x => x.SendState == "Sent" && x.SapGoodsmovementID == null)
     .Join(ErgTransactions, bi => bi.TransactionID, et => et.IdTransactionXdm, (bi, et) => new {
         BatInvoice = bi,
         ErgTransaction = et
     })
     .ToList()
     .Select(x => new {
         IsAvailableOnSAP = false,
         BatInvoice = x.BatInvoice,
         ErgTransaction = x.ErgTransaction,
         NextCheck = DateTime.Now,
         LastChecked = (DateTime?)null,
         LoopDelay = TimeSpan.FromMinutes(1),
         CheckCount = 0
     })
     .Where(x => x.ErgTransaction.MovimentoMagazzinoServizio != 0 ?
             // Se movMagazz = true  -> record con orderId e deliveryId valorizzato
             (!string.IsNullOrEmpty(x.BatInvoice.SapOrderID) && !string.IsNullOrEmpty(x.BatInvoice.SapDeliverydocID))
             :
             // Se movMagazz = false -> record con orderId valorizzato
             (!string.IsNullOrEmpty(x.BatInvoice.SapOrderID))
     )
     .Dump();
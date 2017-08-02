<Query Kind="Statements">
  <Connection>
    <ID>e39fe6aa-6707-422d-8a8e-28977d2f8d9c</ID>
    <Persist>true</Persist>
    <Driver Assembly="IQDriver" PublicKeyToken="5b59726538a49684">IQDriver.IQDriver</Driver>
    <Provider>Devart.Data.Oracle</Provider>
    <CustomCxString>AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAAac45e5C3qUmtB10kiS8YJQAAAAACAAAAAAAQZgAAAAEAACAAAACyT7JGVwRfVOG5t4no7Bu0uUP7C7miwO0QkXvKjNY/pgAAAAAOgAAAAAIAACAAAABwbEJb0Ozc+DTD2W1e/k12EC2dneZj7qrxS7A47Idw4rAAAACSkFha38/EATfBuXMgGLBNusgrvHwW+54/iydsEC0d4bb56zfx9nhxwLBl0yeM0aeRwY9BMAdZWG/aKi5m0xRBYPYQzklkq8TrYFBDS2tiTK0pc2Y9zVF6j3rS0mW5xovai7hgDzUrR24R+8SMIwB/KnJHUNRfRiuTGYnFj3sgxKWoZvjq1fGwE/FeFVFa/ZH754hKNyQ/c/j3dbJ2ZQlSFWgKXDyQN/3A6FBug99YmkAAAAC1b+821CARWGSwFYquvwWVlyGXrwchxhGqzJMAEKwZzRpfEg7ESw0rEilFHQlst6Rljqhj/SEub1BaFu10rls3</CustomCxString>
    <Server>(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP) (HOST=bessie) (PORT=1521)) (CONNECT_DATA=(SERVER=dedicated) (SERVICE_NAME=gdtisvil)))</Server>
    <UserName>ed_tms</UserName>
    <EncryptCustomCxString>true</EncryptCustomCxString>
    <NoPluralization>true</NoPluralization>
    <Password>AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAAac45e5C3qUmtB10kiS8YJQAAAAACAAAAAAAQZgAAAAEAACAAAABPT7CzCGIlyiPrp3F6jg8Xsenm9eBz6rCYEOqlFZU3yQAAAAAOgAAAAAIAACAAAACUbuLGGtfNOibUXZ2cZ0Pe6nN8sZNoUaXgkaEfn7l1CRAAAAAKVBA4ie8fWABWI2w1Np+7QAAAAO4kUyQQ6hswR5CP6HOyLDKLcevKRDfKGP+Jx6NnK7BqzULpAn1kLQ0YuF8XF+ZyS+q4gt02bKc9/BNnxkb3rUY=</Password>
    <DisplayName>gdtisvil.ed_tms</DisplayName>
    <DriverData>
      <StripUnderscores>true</StripUnderscores>
      <QuietenAllCaps>true</QuietenAllCaps>
      <ConnectAs>Default</ConnectAs>
      <UseOciMode>true</UseOciMode>
    </DriverData>
  </Connection>
</Query>

string unit = "UP_VILLA_1";
UnitsAssets.Where(x => x.Units.ReferenceNoTx == unit).Dump();
UnitsAssetsBands.Where(x => x.UnitsAssets.Units.ReferenceNoTx == unit).Dump();
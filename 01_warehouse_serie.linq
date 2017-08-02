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
  <Reference>C:\Work\TMS\DEV\TMS\Common\TMS.Common\bin\x64\Debug\TMS.Common.dll</Reference>
  <Namespace>TMS.Common.Extensions</Namespace>
</Query>

Series
.ToList()
.Where(x =>
	x.SourceCd == "GME" &&
	//x.SymbolCd == "UP_TURBIGO_4" &&
	x.ClassCd == "UnitSchedule" &&
	//x.ClassCd == "PVM" &&
	true)
.OrderBy(x => x.SourceCd)
.ThenBy(x => x.SymbolCd)
.ThenBy(x => x.ClassCd)
.ThenBy(x => x.ChannelCd)
.ThenBy(x => x.ContextCd)
.GroupBy(x => x.SymbolCd)
.Dump();
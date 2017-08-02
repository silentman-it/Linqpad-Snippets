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
</Query>

int formulaId = 2459;
Formulas theFormula = Formulas.Where(f => f.FormulaID == Convert.ToDecimal(formulaId)).SingleOrDefault();

theFormula.Dump();

string formulaDesc = theFormula.FormulaName;

decimal[] serieIds = theFormula.FormulasSeries.Select(f => f.SerieID).ToArray();

Series.
Where(x => serieIds.Contains(x.SerieID))
.OrderBy(x => x.ClassCd)
.ThenBy(x => x.ChannelCd)
.ThenBy(x => x.ContextCd)
.Select(x => new
{
	x.SerieID,
	x.SourceCd,
	x.SymbolCd,
	x.ClassCd,
	//ClassDescription = x.Classes.Description,
	x.ChannelCd,
	x.ContextCd,
	x.PrimaryKey,
	x.SecondaryKey
})
//.Select(x => new
//{
//	x.SerieID,
//	x.ClassDescription,
//	XmlString = string.Format("{0} {1} {2} {3} {4}",
//		x.SourceCd != null ? string.Format("Source=\"{0}\" ", x.SourceCd) : "",
//		x.SymbolCd != null ? string.Format("Symbol=\"{0}\" ", x.SymbolCd) : "",
//		x.ClassCd != null ? string.Format("Class=\"{0}\" ", x.ClassCd) : "",
//		x.ChannelCd != null ? string.Format("Channel=\"{0}\" ", x.ChannelCd) : "",
//		x.ContextCd != null ? string.Format("Context=\"{0}\" ", x.ContextCd) : "")
//		.Trim()
//})
.Dump(formulaDesc);
<Query Kind="Statements">
  <Connection>
    <ID>1202f3d3-b7d5-4258-ac52-f1ee0160d302</ID>
    <Persist>true</Persist>
    <Driver Assembly="IQDriver" PublicKeyToken="5b59726538a49684">IQDriver.IQDriver</Driver>
    <Provider>Devart.Data.Oracle</Provider>
    <CustomCxString>AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAAg4VPWvAGA0K8d2W/MBVMggAAAAACAAAAAAAQZgAAAAEAACAAAAAKrfwu2OSOqwolUiPWik9UN40wR5Dzp4939gu7/wVSuwAAAAAOgAAAAAIAACAAAACEHPLUc7dObeuKE6aYQstf+XRl/tjcDVDoCpnw4ihNuUAAAAC0VT8gG05av7bVR2DERhfHx3gtwL8WhST0zmfkwV8ip/oDMVYiXXx0hgdbNoDB0vyx7Rg4JLZL1BbrRfvx2KfFQAAAAGcFgzHUIB3znMMgGvIP1f3+0+6mNB1vIQEny/V/UbBz0mNNCGdQJ3ueiwZ2jOBed3lJ/4gj4yWxCf6BYbSCAGk=</CustomCxString>
    <Server>svil4</Server>
    <UserName>ed_framework</UserName>
    <EncryptCustomCxString>true</EncryptCustomCxString>
    <Password>AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAA8yqkEwwIyEy9PPj7RuTVJgAAAAACAAAAAAAQZgAAAAEAACAAAACvZ09a4O49pXr13xw2SRRmG6aSJ3FKsaCyYw3Xl3RMzQAAAAAOgAAAAAIAACAAAAB8la2vpFtF4sC3guzRW/tgam52bSl04YCE2RfWfn6v1hAAAABwfqVOxamEcNGTSCO9QM5VQAAAABP4/dXbp0cm+gGrwa6hMG2W26osBYKrZYAZDS11X+dBr7CPCq/mM7m83JuzL+GIVuNg0bXIIlzQMpME/OmtGBQ=</Password>
    <DisplayName>svil4.ed_framework</DisplayName>
    <DriverData>
      <StripUnderscores>true</StripUnderscores>
      <QuietenAllCaps>true</QuietenAllCaps>
      <ConnectAs>Default</ConnectAs>
      <UseOciMode>true</UseOciMode>
    </DriverData>
  </Connection>
  <Output>DataGrids</Output>
</Query>

///////////////////////////////////
// ADD JOB
///////////////////////////////////

//var pclass = Parameters.Where(x => x.ParameterClass.StartsWith("gatherer/server/jobs")).Dump().Select(x => x.ParameterClass).OrderBy(x => x).Distinct().LastOrDefault();
//var lastKey = Int32.Parse(pclass.Split('/').Last()).Dump("Last Key");
//lastKey++;
//
//var newKey = string.Format("gatherer/server/jobs/{0}", lastKey);
//
//Parameters.InsertOnSubmit(new Parameters() {
//	ParameterClass = newKey,
//	ParameterCd = "Timeout",
//	TextValue1 = "00:01:00"
//});
//
//Parameters.InsertOnSubmit(new Parameters() {
//	ParameterClass = newKey,
//	ParameterCd = "TypeName",
//	TextValue1 = "GDTI.Jobs.Misc.ImportInstantMeasures, GDTI.Jobs.Misc"
//});

//SubmitChanges();


///////////////////////////////////
// ADD ACTIVITY
///////////////////////////////////
var parameterClass = "tms/gatherer/activities/GetVEGFromTerna";
var activityName = "GetVEGFromTerna";
var handlerTypeName = "GDTI.ActivityPack.TernaLink.GetVEGFromTerna,GDTI.ActivityPack.TernaLink";
var schemaName = "asm://TMS.Common.Types.Schemas.TMS_Commands.xsd,TMS.Common";

Parameters.InsertOnSubmit(new Parameters() {
	ParameterClass = parameterClass,
	ParameterCd = "activityName",
	TextValue1 = activityName
});

Parameters.InsertOnSubmit(new Parameters() {
	ParameterClass = parameterClass,
	ParameterCd = "handlerTypeName",
	TextValue1 = handlerTypeName
});

Parameters.InsertOnSubmit(new Parameters() {
	ParameterClass = parameterClass,
	ParameterCd = "schemaName",
	TextValue1 = schemaName
});

//SubmitChanges();


var addedRows = Parameters
//.Where(x => x.ParameterClass.Contains("jobs") || x.ParameterClass.Contains("tms/gatherer/activities/"))
.Where(x => x.ParameterClass.Contains(parameterClass))
.Dump();

// DELETE!!!
//foreach(var r in addedRows)
//	Parameters.DeleteOnSubmit(r);
//SubmitChanges();
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

//Activities.Join(Messages, o => o.Messageid, i => i.Messageid, (o, i) => new { Act = o, Mes = i})
//.Where(x => x.Act.Status == "CRE" && (x.Mes.Status == "COM" || x.Mes.Status == "ERR"))
//.Dump();

var ls = Activities.Join(Messages, o => o.Messageid, i => i.Messageid, (o, i) => new { Act = o, Mes = i})
.Where(x =>
	x.Act.Status == "CRE" &&
	//x.Act.Name == "CalculateAndExportSetPoints" &&
	//(x.Mes.Status == "DEL" || x.Mes.Status == "RUN")
	true
	)
.Select(x => new
{
	ActivityObject = x.Act,
	MessageObject  = x.Mes,
	ActivityType   = x.Act.Name,
	Activity_ID    = x.Act.Activityid,
	Message_ID     = x.Mes.Messageid,
	Activity_ST    = x.Act.Status,
	Message_ST     = x.Mes.Status,
	FileName       = x.Mes.Filename,
	Created        = x.Mes.Creationdate
})
.OrderBy(x => x.Created)
.GroupBy(x => x.ActivityType)
.Select(x => new { Key = x.Key, N = x.Count() })
.Dump();

// Cancel Activities
//foreach (var i in ls)
//{
//	i.ActivityObject.Status = "ERR";
//	Activities.UpdateOnSubmit(i.ActivityObject);
//}
//
//SubmitChanges();
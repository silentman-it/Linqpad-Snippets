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
</Query>

Activities
.Where(x => x.Status == "CRE")
.Dump()
.ToList()
.ForEach(x => {
	x.Status = "ERR";
	Activities.UpdateOnSubmit(x);
});

SubmitChanges();
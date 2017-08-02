<Query Kind="Statements">
  <Connection>
    <ID>233e2ecb-eb1d-4f13-99ea-e13b89f590f5</ID>
    <Persist>true</Persist>
    <Driver Assembly="IQDriver" PublicKeyToken="5b59726538a49684">IQDriver.IQDriver</Driver>
    <Provider>Devart.Data.Oracle</Provider>
    <CustomCxString>AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAANdCQGyQX5UakzxG+SLuRZQAAAAACAAAAAAADZgAAwAAAABAAAABjiFXIEI7sivHQR75do9MBAAAAAASAAACgAAAAEAAAAESXT3jDWaynyRZmTYNhIVowAAAAck8Y8gUN24H+bmZtjWEFvydArIo9wKxoyVpPX6qJrroS/ExNQptnP3T09eqLSTVEFAAAAMsBBfZUqeA6RSfRsc3VebOOHepp</CustomCxString>
    <Server>svilhomer</Server>
    <UserName>ed_tms</UserName>
    <Password>AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAANdCQGyQX5UakzxG+SLuRZQAAAAACAAAAAAADZgAAwAAAABAAAADzcpAOreWR+UMFh4CUx4BKAAAAAASAAACgAAAAEAAAAPoP3TO+Qi6IftXCa3UatAMIAAAAzIDrHbiR61gUAAAAOJ1PC5NH8+yZ/FF6l7kldeS8azs=</Password>
    <EncryptCustomCxString>true</EncryptCustomCxString>
    <DriverData>
      <StripUnderscores>true</StripUnderscores>
      <QuietenAllCaps>true</QuietenAllCaps>
      <ConnectAs>Default</ConnectAs>
      <UseOciMode>true</UseOciMode>
    </DriverData>
  </Connection>
</Query>

string unit = "UP_SRGNPGLCNT_1";
string act = "REAL_TIME";

var formulaId = ActivitiesFormulas
.Where(af => af.ActivityCd == act && af.ReferenceNoTx == unit).Select(af => af.FormulaID).SingleOrDefault().Dump();

ActivitiesSeriesGroups
.Where(asg => asg.ActivityCd == act && asg.ReferenceNoTx == unit)
.Join(ActivitiesGroupsDetails, asg => asg.ActivitySerieGroupID, agd => agd.ActivitySerieGroupID, (a,b) => new
{
	Act = a.ActivityCd,
	Unit = a.ReferenceNoTx,
	Tab = a.Description,
	TabPos = a.Position,
	SerieID = b.SerieID
})
.Dump();

//ActivitiesGroupsDetails
//.Where(agd => agd.
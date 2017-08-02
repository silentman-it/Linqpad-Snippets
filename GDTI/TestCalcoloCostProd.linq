<Query Kind="Statements">
  <Connection>
    <ID>805f4710-8163-418e-bb73-a7f4e26e44f2</ID>
    <Persist>true</Persist>
    <Driver Assembly="IQDriver" PublicKeyToken="5b59726538a49684">IQDriver.IQDriver</Driver>
    <Provider>Devart.Data.Oracle</Provider>
    <CustomCxString>AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAA3q0pbjoE2kGb/5UBq8YBrAAAAAACAAAAAAADZgAAwAAAABAAAAAeHJsXBLNwms/xadm/jx8hAAAAAASAAACgAAAAEAAAAJ69WKH+yS+z5LyOQ4W0q4VAAAAAphKJWkOOKkLVKJ2eNx+24HM1CtXbo421tt1sEb6LNi4G6tJYnCUQ/rdHYcsl1QdvjjJW2gWRUW7HzJcoIe3UcBQAAAAZSEBI0ZTTtegWFi8g74FYI8lGdQ==</CustomCxString>
    <Server>svilhomer</Server>
    <UserName>ed_warehouse</UserName>
    <Password>AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAA3q0pbjoE2kGb/5UBq8YBrAAAAAACAAAAAAADZgAAwAAAABAAAACtgZXWThM72sU57pDRLwUBAAAAAASAAACgAAAAEAAAADX7ybfpIWv3/qK3nq/fkS0QAAAAOmx1fzo6mQ8RGxiiDu5xvhQAAABmgdhb+/TT2zhbMS6M/p6yl5qnLA==</Password>
    <DisplayName>svilhomer.ed_warehouse</DisplayName>
    <EncryptCustomCxString>true</EncryptCustomCxString>
    <DriverData>
      <StripUnderscores>true</StripUnderscores>
      <QuietenAllCaps>true</QuietenAllCaps>
      <ConnectAs>Default</ConnectAs>
      <UseOciMode>true</UseOciMode>
    </DriverData>
  </Connection>
  <Output>DataGrids</Output>
  <Reference>C:\Work\TMS_PRJ\DEV\TMS\Common\Energy.RealTime.Strategy\bin\x64\Debug\Energy.RealTime.Core.dll</Reference>
  <Reference>C:\Work\TMS_PRJ\DEV\TMS\Common\Energy.RealTime.Strategy\bin\x64\Debug\Energy.RealTime.Strategy.dll</Reference>
  <Namespace>Energy.RealTime.Core.Utils</Namespace>
  <Namespace>Energy.RealTime.Strategy</Namespace>
</Query>

string fileName = @"C:\Users\CoppolaF\Desktop\DEBUG_RealTimeDeltaCosts_Calculation_20140718.bin";

RealTimeDeltaCosts o = BinarySerialization.Deserialize<RealTimeDeltaCosts>(File.ReadAllBytes(fileName));
//o.Efficiency.Dump("ORIGINAL");

//o.Efficiency.Values.OrderBy(k => k.Key).Dump("SORTED");
//
//o.Efficiency["UP_TORINONORD_1_ASSETTO_1_F1", 1].Dump("Efficiency[UP_TORINONORD_1_ASSETTO_1_F1, 1]");
//o.Efficiency["UP_TORINONORD_1_ASSETTO_1_F1", 339].Dump("Efficiency[UP_TORINONORD_1_ASSETTO_1_F1, 339]");
//o.Efficiency["UP_TORINONORD_1_ASSETTO_1_F1", 340].Dump("Efficiency[UP_TORINONORD_1_ASSETTO_1_F1, 340]");

o.Debug = true;

o.Compute();

o.FMS.Dump("FMS");
o.PVM.Dump("PVM");
o.Efficiency.Dump("Eff Curves");
o.FuelCost.Dump("Fuel Cost");
o.VariableCosts.Dump("Variable Costs");
o.ResultValues.Dump("Results");
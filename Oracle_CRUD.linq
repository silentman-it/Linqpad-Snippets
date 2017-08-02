<Query Kind="Statements">
  <Connection>
    <ID>eebaf413-8ad0-47b7-86c1-c7960a97b4d7</ID>
    <Persist>true</Persist>
    <Driver Assembly="IQDriver" PublicKeyToken="5b59726538a49684">IQDriver.IQDriver</Driver>
    <Provider>Devart.Data.Oracle</Provider>
    <CustomCxString>AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAAX7wiAzS2sEWFQZ7M1vdRgAAAAAACAAAAAAADZgAAwAAAABAAAACIHspvMFBP/eWm82Q5DHhWAAAAAASAAACgAAAAEAAAAJpXSVTwJB+knAw3tzEhzg9AAAAAf2NphGscufDiaMe88DAxSGjf6SCx/ecXQAxTqSWgVxo+Of7Wwu8d7W1mtwrnQPzV1LRveQ1xM28GhibZw+fK9hQAAABmRihufed2Hi38m13ZKERP8+3dag==</CustomCxString>
    <Server>sinergreen</Server>
    <UserName>ed_framework</UserName>
    <Password>AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAAX7wiAzS2sEWFQZ7M1vdRgAAAAAACAAAAAAADZgAAwAAAABAAAABTomlfmymhXZeUYxiRCfoVAAAAAASAAACgAAAAEAAAAJ/Cp9XyKWnwGxRn25f5xVsQAAAAf4CHkJTqqN/s+ORwslOp7hQAAACt5x2byNu17wPpRkVXPEfh0HY5pA==</Password>
    <EncryptCustomCxString>true</EncryptCustomCxString>
    <DriverData>
      <StripUnderscores>true</StripUnderscores>
      <QuietenAllCaps>true</QuietenAllCaps>
      <ConnectAs>Default</ConnectAs>
      <UseOciMode>true</UseOciMode>
    </DriverData>
  </Connection>
</Query>

//----------------- INSERT ---------------------

//Parameters row = new Parameters()
//{
//	ParameterClass = "gatherer/server/jobs/1",
//	ParameterCd = "Timeout",
//	TextValue1 = "00:15:00"
//};
//Parameters.InsertOnSubmit(row);
//
//row = new Parameters()
//{
//	ParameterClass = "gatherer/server/jobs/1",
//	ParameterCd = "TypeName",
//	TextValue1 = "EMODS.Jobs.SubmitCostsCalculation,EMODS.Jobs"
//};
//Parameters.InsertOnSubmit(row);


//----------------- UPDATE ---------------------
//var row = Parameters
//.Where(x => x.ParameterClass.Contains("jobs/1") && x.ParameterCd == "Timeout")
//.Single()
//.Dump();
//row.TextValue1 = "00:01:00";

//----------------- DELETE ---------------------
//var rows = Parameters
//.Where(x => x.ParameterClass.Contains("jobs/1"))
//.Dump();
//
//foreach (var r in rows)
//{
//	Parameters.DeleteOnSubmit(r);	
//}
//
//SubmitChanges();
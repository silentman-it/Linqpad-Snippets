<Query Kind="Statements">
  <Connection>
    <ID>f4e92e34-c4b4-4ff0-b8d3-9d5b847aab47</ID>
    <Persist>true</Persist>
    <Driver Assembly="IQDriver" PublicKeyToken="5b59726538a49684">IQDriver.IQDriver</Driver>
    <Provider>Devart.Data.Oracle</Provider>
    <CustomCxString>AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAAXw9k02xJM0uVTAPS/QGCJwAAAAACAAAAAAAQZgAAAAEAACAAAAAdBF4Of4+OUrSbOQYSCEt4gvjLNH7Ewz1B4iWYtPav+wAAAAAOgAAAAAIAACAAAADN7ov5xAfpiY021oz0qaxfLyNGtcaVrt9asHXYjW1tBzAAAACzPNMJBBpNAvOdCrjAXPrjuyW3vjStRjf2G7Hc3qVujzIulRMus9OT0lXVpXq+MbVAAAAA2e4kMN/0l/c4qvJiepQPJ8mJxc54uhHK2BZOwfjiYQ3uXKW3ki9Lf0cRdfhrGmm+OAMROTsPTlIVif/B1TOtqQ==</CustomCxString>
    <Server>svil4</Server>
    <EncryptCustomCxString>true</EncryptCustomCxString>
    <DisplayName>svil4.ed_tms</DisplayName>
    <UserName>ed_tms</UserName>
    <Password>AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAAXw9k02xJM0uVTAPS/QGCJwAAAAACAAAAAAAQZgAAAAEAACAAAACe4YWygoqaWlbRNHnNvUQ3XVr51Qn6HnGNE3fdXLAYMwAAAAAOgAAAAAIAACAAAAAYsyvVah0qDenyPdC1okQt3PabIEfI8ViVYb6MYTMw+hAAAAA51YDTrFdVNTnCRwlTg9IkQAAAAD/K4CqVbS+7Z1JUOjLsnWftsxY1iKJ939pZxe9Rf+tfeFsDn13uggPUulCmATHRZTq/0qQ93/vxovGdeVw77ww=</Password>
    <DriverData>
      <StripUnderscores>true</StripUnderscores>
      <QuietenAllCaps>true</QuietenAllCaps>
      <ConnectAs>Default</ConnectAs>
      <UseOciMode>true</UseOciMode>
    </DriverData>
  </Connection>
</Query>

ActivitiesAssociations
.OrderBy(x => x.ReferenceNoTx)
.ToList()
.ToPivotTable(x => x.ReferenceNoTx, x => x.ActivityCd, x => x.Any() ? "x" : "")
.Dump();
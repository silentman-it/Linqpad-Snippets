<Query Kind="Statements">
  <Connection>
    <ID>5e7e6496-0017-451d-bc97-f509ff34b585</ID>
    <Persist>true</Persist>
    <Driver Assembly="IQDriver" PublicKeyToken="5b59726538a49684">IQDriver.IQDriver</Driver>
    <Provider>Devart.Data.Oracle</Provider>
    <CustomCxString>AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAA2gyzB2XF+E2Ar6/EAdc9fwAAAAACAAAAAAAQZgAAAAEAACAAAAA6jhzKPumX8w20cFDbqERoN/7fSRGrSBi2j6JfQC1YQQAAAAAOgAAAAAIAACAAAACqkTnGmH4A5Agu5mS/90RgeuGWPUlm30gfZI8fHNExPkAAAABIdu7uCQFu02frRBg5tZbt7/XoRJ76i/yYOD2BHlu5RNuoxgQb9Nv3ATQboAcgQd4SKpGdeO+o7D3y9V60JvgZQAAAAEiONrbfqF8mNlCx1WFGAblk/3T7XyBa3Mxl300jeLb65Usr7ncBm08C3R6Pv1qqby4F9SolBJkuoIeLpYBfvRc=</CustomCxString>
    <Server>gdti_tp</Server>
    <EncryptCustomCxString>true</EncryptCustomCxString>
    <NoPluralization>true</NoPluralization>
    <DisplayName>gdti_tp.ed_framework</DisplayName>
    <UserName>ed_framework</UserName>
    <Password>AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAA2gyzB2XF+E2Ar6/EAdc9fwAAAAACAAAAAAAQZgAAAAEAACAAAADq2zC7LHPUpWrbAsap1NH7zTgfBbiaU1CU+wd0cQo3/QAAAAAOgAAAAAIAACAAAADhjUBCvCJZXoFiCDeTBlceuNtsahjnF0GH/5NFznVAixAAAAD9xs4O+z0IqouMTvRwZTWAQAAAAJjlRb4Ryzd/1k+SWDHG5lmzs3AXp57vWgS213/FcaPM4ZKnOK62vFX1ToaOkTsconyzSet3yYMjrrTU4zP74oQ=</Password>
    <DriverData>
      <StripUnderscores>true</StripUnderscores>
      <QuietenAllCaps>true</QuietenAllCaps>
      <ConnectAs>Default</ConnectAs>
      <UseOciMode>true</UseOciMode>
    </DriverData>
  </Connection>
</Query>

Messages
	.Join(Activities, o => o.Messageid, i => i.Messageid, (o, i) => new {
		MessageId = o.Messageid,
		ActivityId = i.Activityid,
		FileName = o.Filename,
		MessageStatus = o.Status,
		ActivityStatus = i.Status,
		MessageCreationDate = o.Creationdate,
		Activity = i.Name,
		ActivityStart = i.ExecutionStartTs,
		ActivityEnd = i.ExecutionEndTs,
		MessagePriority = o.Priority,
		Runner = o.Runnerid,
		Queue = o.Runnerqueueid
	})
	//.ToList()
	//.Where(x => x.ActivityStatus == "CRE")
	.Where(x =>
		x.ActivityEnd != null &&
		//x.Activity == "ImportEnBOMeasures" &&
		true)	
	.OrderByDescending(x => x.ActivityEnd)
	.Take(10000)
	.Dump()
	.ToList()
	.GroupBy(x => x.ActivityEnd.Value.Date)
	.Dump()
	;
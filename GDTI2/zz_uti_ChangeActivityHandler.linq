<Query Kind="Statements">
  <Connection>
    <ID>e47250bf-8df8-49fe-b998-cbbcb5ac27fe</ID>
    <Persist>true</Persist>
    <Driver Assembly="IQDriver" PublicKeyToken="5b59726538a49684">IQDriver.IQDriver</Driver>
    <Provider>Devart.Data.Oracle</Provider>
    <CustomCxString>AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAA8yqkEwwIyEy9PPj7RuTVJgAAAAACAAAAAAAQZgAAAAEAACAAAACW93HrZ2Xxk46iZp5T0osTVEAXjwWVTUWjh7ZCHra7RwAAAAAOgAAAAAIAACAAAAAKRmuIfddAG+XLkxtZqrIveZh5dtaN+6xlrHI4WFnoVEAAAABKWuT1jkG4aZq5wOZ5TnCqh2Bpb6p/ZOZnyr1l4R+mtrSmQglWLxNVh/ZHm82TxMagPPpQlhCxkveU3iLtaKxdQAAAAPFftcv4c4unBXXstKLJiWX2Ug6qTMweB1h+G71Ph9Oe30BJ4FvO76zf/B0Ad+EnJpIc17n4CSTpI6/6zjHY3zU=</CustomCxString>
    <Server>svilhomer</Server>
    <UserName>ed_framework</UserName>
    <EncryptCustomCxString>true</EncryptCustomCxString>
    <NoPluralization>true</NoPluralization>
    <Password>AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAA8yqkEwwIyEy9PPj7RuTVJgAAAAACAAAAAAAQZgAAAAEAACAAAAAnrJzYgRsNEfcB8coj0LYbR1dVgHHPsGIPQktMl11AFwAAAAAOgAAAAAIAACAAAADz/QEhT6JRymYx/elk3UK+4amiA4yDeeIfZI5yEyjuvxAAAADls8ueKrMQa41ziEdei7CcQAAAAB85ZF2sHpw8VeMpdFh/YVLKdZSmd5EqIZLkGravIKi5X6AsTmQMxh9GjjUjaX76BhHep+41nV+NaDSbZzPocVo=</Password>
    <DriverData>
      <StripUnderscores>true</StripUnderscores>
      <QuietenAllCaps>true</QuietenAllCaps>
      <ConnectAs>Default</ConnectAs>
      <UseOciMode>true</UseOciMode>
    </DriverData>
  </Connection>
</Query>

// EDIT
//

//var parClass = Parameters.SingleOrDefault(x => x.ParameterCd == "activityName" && x.TextValue1 == "VDTClear").Dump().ParameterClass;
//
//var p1 = Parameters.SingleOrDefault(x => x.ParameterClass == parClass && x.ParameterCd == "schemaName");
//p1.TextValue1 = "asm://TMS.Common.Types.Schemas.TMS_Commands.xsd,TMS.Common";
//var p2 = Parameters.SingleOrDefault(x => x.ParameterClass == parClass && x.ParameterCd == "handlerTypeName");
//p2.TextValue1 = "GDTI.Activities.RUP.VDTClear,GDTI.Activities.RUP";
//
//Parameters.UpdateOnSubmit(p1);
//Parameters.UpdateOnSubmit(p2);

// ADD
//

//var parClass = "VDTClear";
//var activityName = "VDTClear";
//var handlerTypeName = "GDTI.Activities.RUP.VDTClear,GDTI.Activities.RUP";
//var schemaName = "asm://TMS.Common.Types.Schemas.TMS_Commands.xsd,TMS.Common";
//
//parClass = "tms/gatherer/activities/" + parClass;
//
//Parameters.InsertOnSubmit(new Parameters()
//{
//	ParameterClass = parClass,
//	ParameterCd = "activityName",
//	TextValue1 = activityName
//});
//Parameters.InsertOnSubmit(new Parameters()
//{
//	ParameterClass = parClass,
//	ParameterCd = "handlerTypeName",
//	TextValue1 = handlerTypeName
//});
//Parameters.InsertOnSubmit(new Parameters()
//{
//	ParameterClass = parClass,
//	ParameterCd = "schemaName",
//	TextValue1 = schemaName
//});


// SAVE!
//SubmitChanges();

Parameters.Where(x => x.ParameterClass == parClass).Dump();
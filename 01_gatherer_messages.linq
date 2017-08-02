<Query Kind="Statements">
  <Connection>
    <ID>271761f6-fcc6-4ef0-b573-f7a634bf4e80</ID>
    <Persist>true</Persist>
    <Driver Assembly="IQDriver" PublicKeyToken="5b59726538a49684">IQDriver.IQDriver</Driver>
    <Provider>Devart.Data.Oracle</Provider>
    <CustomCxString>AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAAg4VPWvAGA0K8d2W/MBVMggAAAAACAAAAAAAQZgAAAAEAACAAAACbdVcMSuF0M/c9kqPua0azGmYwt9h3b5/il688Qi5VNwAAAAAOgAAAAAIAACAAAADKoQTUrWpLLfpaIXv0WyYoDXRXB5d7M79rAhzpeAyLwMAAAADPPTBwRPB4l+xx0XyeFmQUJOLMHTv3aeozP4vwrwFSHQs7Qy/fz94UedRrvviGgXSv/uVozI9JW8rzpmEOnFitoCOeFj8idjAgJs4Klh0EhO262JB2Rh7JA2+GUgqJAGIojHfz3f3WZJeomsf2OkvnBWQvEu1CvDUQAUVnI+rRXbLO+DKrNUVkXCYnpX1RKlB+fILc71l8o9n+c28DZ7kWCBnmvjFTf/xAfdB9DsDv271GG6DGs+W91pxWcGAgEAFAAAAAoeGjdFE8527hqtdpjy+t3aNkyxel0kPwZ4+AFEcLMUOh96Js8RppJkJs+XA7yPYznP1Y3qSUgg6Budai6MRu/g==</CustomCxString>
    <Server>(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=172.19.123.83)(PORT=1521))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=gdti01.edg.grptop.net)))</Server>
    <UserName>ed_framework</UserName>
    <EncryptCustomCxString>true</EncryptCustomCxString>
    <NoPluralization>true</NoPluralization>
    <Password>AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAA8yqkEwwIyEy9PPj7RuTVJgAAAAACAAAAAAAQZgAAAAEAACAAAAAnrJzYgRsNEfcB8coj0LYbR1dVgHHPsGIPQktMl11AFwAAAAAOgAAAAAIAACAAAADz/QEhT6JRymYx/elk3UK+4amiA4yDeeIfZI5yEyjuvxAAAADls8ueKrMQa41ziEdei7CcQAAAAB85ZF2sHpw8VeMpdFh/YVLKdZSmd5EqIZLkGravIKi5X6AsTmQMxh9GjjUjaX76BhHep+41nV+NaDSbZzPocVo=</Password>
    <DisplayName>gdti01.ed_framework</DisplayName>
    <DriverData>
      <StripUnderscores>true</StripUnderscores>
      <QuietenAllCaps>true</QuietenAllCaps>
      <ConnectAs>Default</ConnectAs>
      <UseOciMode>true</UseOciMode>
    </DriverData>
  </Connection>
</Query>

Blobs
.Where(x =>
	x.MessageidMessages.Creationdate >= DateTime.Today &&
//	x.MessageidMessages.Status != "COM" &&
//	x.MessageidMessages.Status != "ERR" &&
//	x.MessageidMessages.Createdbyuserid == "API" &&
	true)
.Select(x => new
{
	MsgId = x.MessageidMessages.Messageid,
	UserId = x.MessageidMessages.Createdbyuserid,
	Created = x.MessageidMessages.Creationdate,
	FileName = x.MessageidMessages.Filename,
	Type = x.MessageidMessages.Messagetype,
	status = x.MessageidMessages.Status,
	//ContentFirsts = UTF8Encoding.UTF8.GetString(x.Content).Substring(0,20) + "(...)",
	NV = ""
})
.Dump();

// dump Blob
//int id = 3263;
//UTF8Encoding.UTF8.GetString(Blobs.Where(x => x.Objectid == id).First().Content).Dump();
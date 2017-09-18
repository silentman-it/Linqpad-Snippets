<Query Kind="Statements">
  <Connection>
    <ID>28f7de50-894b-413e-af92-a6ddac64cedf</ID>
    <Persist>true</Persist>
    <Driver Assembly="IQDriver" PublicKeyToken="5b59726538a49684">IQDriver.IQDriver</Driver>
    <Provider>Devart.Data.Oracle</Provider>
    <CustomCxString>AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAAkOYp3WDye0ORXtly5hEZsAAAAAACAAAAAAAQZgAAAAEAACAAAABs3SM43cU1cqZFEYvyWCBr2f3d443aAzIZdytRKVcvzwAAAAAOgAAAAAIAACAAAADqn1JNbU2X8oNRbH8e5SdKL5P4li8MGJBsoFrmv4qVgsAAAAC8C60jZxe6zObN5bQBNXPRPfWfNEzCDtcYd2b9AfL8Zk3o8919bAlpylOt48ZVQwZmCsjiHSFOg28e2akFU/v15Ccpy7lSov2bOdPN/yjUARGhTnHH/4V1F1hozwImz+P9VUTL7Tl2+ccdGKhHd1qmD7rvZbeIBe5db/FGfN5XFSunDi3vFgZz0A+lLi75yi4dEx86hFk5Qk9W2w2K2u/G9kf3CzGLRWSTvZOHF28njAFRvvDRIkWXSMECcMaScOdAAAAAx0fMn4TMHqPohg/42vvRaGd2mLdog3ArudhNUCq4+jgr2flDQEFYy/RW4VdRXdMkIvPBz1daSJn5P1DxBbP8aQ==</CustomCxString>
    <EncryptCustomCxString>true</EncryptCustomCxString>
    <DisplayName>win2008r2test1.ed_framework</DisplayName>
    <Server>(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=Win2008R2Test1.edg.grptop.net)(PORT=1521))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=ETR11DM1)))</Server>
    <UserName>ed_framework</UserName>
    <Password>AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAAvm6QbRxlJUuLCV01lsuo5AAAAAACAAAAAAAQZgAAAAEAACAAAABh4tFJTvgr+Vy2uDBP6l9Amx3z06r61pm7iI8Xbrf/TwAAAAAOgAAAAAIAACAAAABw2Ih0heWuoDAiSPadoE5N01uM+IgFo6e/n1odEpaKJxAAAACTs8x2QHPzt2WPga1pQyhSQAAAALsm2+UIJ0mLwsk2gWaHSPkWaK/MRIL8ZNYRraJj7KJdvddZNT3nWvgHLYl2v52SFYdKKemjWUy3STVBkjWviUk=</Password>
    <DriverData>
      <StripUnderscores>true</StripUnderscores>
      <QuietenAllCaps>true</QuietenAllCaps>
      <ConnectAs>Default</ConnectAs>
      <UseOciMode>true</UseOciMode>
    </DriverData>
  </Connection>
</Query>

var ls = Messages
.Select(x => new 
{
	x.Messageid,
	Blob = x.MessageidBlobs.Content,
	x.Status,
	x.Filename,
	x.Creationdate,
	x.Priority,
	x.Runnerid,
	x.Runnerqueueid
})
.Where(x => x.Filename.Contains("SendInstant"))
.OrderByDescending(x => x.Creationdate)
;

ls.Select(x => new {
	x.Messageid,
	x.Status,
	Blob = System.Text.Encoding.UTF8.GetString(x.Blob),
	x.Filename,
	x.Creationdate,
	Priority = x.Priority,
	Runner = x.Runnerid,
	Queue = x.Runnerqueueid
})
.ToList()
.Where(x => x.Blob.Contains("SendInstantMeasure"))
.Dump();
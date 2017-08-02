<Query Kind="Statements">
  <Connection>
    <ID>bc0ccaa6-5431-46a2-bbb6-09ac51d730b4</ID>
    <Persist>true</Persist>
    <Driver Assembly="IQDriver" PublicKeyToken="5b59726538a49684">IQDriver.IQDriver</Driver>
    <Provider>Devart.Data.Oracle</Provider>
    <CustomCxString>AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAAg4VPWvAGA0K8d2W/MBVMggAAAAACAAAAAAAQZgAAAAEAACAAAAB7tOdWDxmwcvnYFlyiPa6DGGS2xTZSukbOKd40r8KPQAAAAAAOgAAAAAIAACAAAACcvlH0JuqdN7yFuRZyo7Lvp3G1hxE/STzMocpV/zCg08AAAAADkTs7iGOiEkMjg9q+UsxX+g4RwuXYS0rueqp16s7yS87XkSkVkTuJyGp0r5ozcbAf07wWHVO/drh7bhL+GgYkn4S7wEw7niK5gKb0Pm6ue2+Ar3p7Wl2Gb6tRzmXg3qJfvx8RgFUWzQ88WKq0Dd17KyYp2svEx227vh4S7P9MEKeLiLbf3E0ruO5izefTEBYRHAWnVcSr6rDfK+7KoW2M6bekA9+9r4qnUSZ2cXl/VLCl03pG+9xg7pazXLYg5dNAAAAA+97UY2k/l0gYVoI6ORKl+QgxyIF/I1FbG8PyNdWmwg/3kSZSja2OJ9Y4LJ64ZD51cxLKRrkU67RBTRL28E9iLg==</CustomCxString>
    <Server>(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=172.19.123.83)(PORT=1521))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=gdti01.edg.grptop.net)))</Server>
    <EncryptCustomCxString>true</EncryptCustomCxString>
    <NoPluralization>true</NoPluralization>
    <Password>AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAAg4VPWvAGA0K8d2W/MBVMggAAAAACAAAAAAAQZgAAAAEAACAAAACQli97J7dR8RH/OHtNbTXsBJBXnoHfFRNm0aNSn3ovmAAAAAAOgAAAAAIAACAAAABkb3pHzVH+crZG6TQTlhS/+o2U0q+SaJGSA8OiObKY+hAAAABzYzgsLfGPlj4t3VpeqXXmQAAAAHP+9z7hrkZRFBVYrWmciGDfQnafJZkpTFs4KwPV9v49ZQT3K065RAiUkZja5MJM1sRCUJRZZJt4PESFpZMVM8E=</Password>
    <DisplayName>gdti01.ed_warehouse</DisplayName>
    <UserName>ed_warehouse</UserName>
    <DriverData>
      <StripUnderscores>true</StripUnderscores>
      <QuietenAllCaps>true</QuietenAllCaps>
      <ConnectAs>Default</ConnectAs>
      <UseOciMode>true</UseOciMode>
    </DriverData>
  </Connection>
  <Output>DataGrids</Output>
</Query>

int streamId = 28577;

var ls1 = SeriesDetails.Where(x => x.StreamID == streamId).Dump();
var ls2 = SeriesDetailsGroups.Where(x => x.StreamID == streamId).Dump();
var ls3 = Streams.Where(x => x.StreamID == streamId).Dump();


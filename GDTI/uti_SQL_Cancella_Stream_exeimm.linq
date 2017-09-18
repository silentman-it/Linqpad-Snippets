<Query Kind="SQL">
  <Connection>
    <ID>b318cdfc-42de-4061-9e9c-f72e11ab4749</ID>
    <Persist>true</Persist>
    <Driver Assembly="IQDriver" PublicKeyToken="5b59726538a49684">IQDriver.IQDriver</Driver>
    <Provider>Devart.Data.Oracle</Provider>
    <CustomCxString>AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAAhYrLhpjbjUKVEUXKx5QXqwAAAAACAAAAAAAQZgAAAAEAACAAAACNY7QniVA5iCr7nNeCi1Rfa0BH2RvdxvE1rrSPl+a48AAAAAAOgAAAAAIAACAAAABSPC80vVX3+R99L5Wou35OBm0p6rWhUhBjUHd4KTHDwsAAAABPgF0K40EexJgVv82s+EmxAaWeEuvh0F4XXJfuKkeQU+QaUzRWrytXVRXo5zOqggoTxvdVbvjLynw54K0rkwKoIf3eaInd/3NMHJCJx+spiaTZSnxvhA1zy2/x8+Orv4DPqMcjRc6vLNchzeiVGXDOWixIkVruuZnZW3XAjrhPmYi0mRIaIY63G4+xH29sMlCP4H4RCefC+tNye/baTFY4z+oDgl7gCzNZIFdmuhbLLgzDCJKOZUkJZxHFZawurRVAAAAA7uVF/zN0hNlGz9IQkZbNTPxrYBDkHd92BPtknR4DPkS2x9mdDKFZbmo2pQqBKKqOP13m9o4AcN01uLp8BWrplg==</CustomCxString>
    <EncryptCustomCxString>true</EncryptCustomCxString>
    <DisplayName>win2008r2test1.ed_warehouse</DisplayName>
    <Server>(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=Win2008R2Test1.edg.grptop.net)(PORT=1521))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=ETR11DM1)))</Server>
    <Password>AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAAhYrLhpjbjUKVEUXKx5QXqwAAAAACAAAAAAAQZgAAAAEAACAAAACsu7A3V7ZNkIdBvvvsBOHioixuOuuCEN/hL3c2M4pmtQAAAAAOgAAAAAIAACAAAADq5uXdajbbs3dCAeNbf9deFLC/AJ4CdJhB45G7YOUdghAAAACuwd7Q3DzsOOq7U3X0z6ANQAAAABJxGhDmdIUUphBdnaPGqwcU7XtKC5uu5dOel2LDs6WvmVnDujNsCxxNfYisDM58CKcQmfb8JFbZQcD1gGgSosU=</Password>
    <UserName>ed_warehouse</UserName>
    <DriverData>
      <StripUnderscores>true</StripUnderscores>
      <QuietenAllCaps>true</QuietenAllCaps>
      <ConnectAs>Default</ConnectAs>
      <UseOciMode>true</UseOciMode>
    </DriverData>
  </Connection>
</Query>

DECLARE
	ls	VARCHAR2(4000);
BEGIN

	ls := '(89653,89649,68464,36635,36632,89652,36533,36531,36529,36527,36525,89654,36548,36540,36538,36536)';

	EXECUTE IMMEDIATE 'DELETE FROM SERIES_DETAILS WHERE STREAM_ID IN ' || ls;
	EXECUTE IMMEDIATE 'DELETE FROM SERIES_DETAILS_GROUPS WHERE STREAM_ID IN ' || ls;
	EXECUTE IMMEDIATE 'DELETE FROM STREAMS WHERE STREAM_ID IN ' || ls;

END

COMMIT;



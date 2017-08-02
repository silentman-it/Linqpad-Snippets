<Query Kind="SQL">
  <Connection>
    <ID>13ea5274-7303-4ca0-adfa-ff4b1e31ef01</ID>
    <Persist>true</Persist>
    <Driver Assembly="IQDriver" PublicKeyToken="5b59726538a49684">IQDriver.IQDriver</Driver>
    <Provider>Devart.Data.Oracle</Provider>
    <CustomCxString>AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAAXw9k02xJM0uVTAPS/QGCJwAAAAACAAAAAAAQZgAAAAEAACAAAACSqb2p/I5Aqrgbx2Fz7IC79yTUYgOfKEwbSE11dPNWSAAAAAAOgAAAAAIAACAAAAAmCMzYXsuguBTxlBhZ9NVYmSah89iaDA4oBeSner7KIEAAAACBtLrTxL6aKWT4O0UZMD51sTw2p+p/MB2F2EzF3kHl7y/fqDafp9bUHC33qvxLPg2vv1/h9ID8ZyI7QR/X8xXeQAAAAFFV4G4zbV6Xdicyru5UZ1m6Rm2SPqCgpzjnqKAlFnjKceUrRocQdaY33PYl9eJbUR3qQ0pGaay7adTEUOv/kJE=</CustomCxString>
    <Server>svil5</Server>
    <EncryptCustomCxString>true</EncryptCustomCxString>
    <UserName>ed_warehouse</UserName>
    <Password>AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAAXw9k02xJM0uVTAPS/QGCJwAAAAACAAAAAAAQZgAAAAEAACAAAABmlxfbKt4ZoQnbKozLAz/6zUZPqrjrjpYt6oAh2C28KAAAAAAOgAAAAAIAACAAAAA8kZlWVpdPGsC07mdTDMNplEZJLFO8GFBgb9YIyaoj7BAAAACfh9wHdMnLZLQvLGQEwIL+QAAAALOusXWPr7mOG8HtzMzSLIR1z+E8qWVVTrPDoxENFqND4/te+D+rdGqIdR6uhETbxyKUhOZHjXGMG/WwpL8xxq4=</Password>
    <DriverData>
      <StripUnderscores>true</StripUnderscores>
      <QuietenAllCaps>true</QuietenAllCaps>
      <ConnectAs>Default</ConnectAs>
      <UseOciMode>true</UseOciMode>
    </DriverData>
  </Connection>
</Query>

select * from series_details
where serie_id in (select serie_id from series
	where
	source_cd = 'ExcelSender' and
	symbol_cd = 'UP_TORINONORD_1' and
	class_cd = 'PrimaryReserveUnavailable')
and validity_start_date >= to_date('27/04/2016', 'DD/MM/YYYY')
and validity_start_date <= to_date('28/04/2016', 'DD/MM/YYYY')
<Query Kind="SQL">
  <Connection>
    <ID>287891b1-f69e-43eb-a073-06c9d5f3ed9e</ID>
    <Persist>true</Persist>
    <Driver Assembly="IQDriver" PublicKeyToken="5b59726538a49684">IQDriver.IQDriver</Driver>
    <Provider>Devart.Data.Oracle</Provider>
    <CustomCxString>AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAABqjAv27bfEW6HYG6LADkfQAAAAACAAAAAAAQZgAAAAEAACAAAAChGevJ3JmDwOLfQBCIYlLOGeokKOAZXGmQ7r/0l67C5AAAAAAOgAAAAAIAACAAAACTAGtTtnmqRqDDtHEpShVG6jb55jK3ncv3DSSbuiaweDAAAACotnBxUiuZNIQcWhPmE/quoJV2v+wcsfkfFqAvVGaPz6nlMu0jHsPdYGQDxssk5dNAAAAAe69btScyG2ra0g3MG264XyUISIUEoycJLtgtttOgsuqeA0QldsaIqqrUlaWE66/Lx3FGb2mZdpdkWN1rMOCaSA==</CustomCxString>
    <Server>bat</Server>
    <EncryptCustomCxString>true</EncryptCustomCxString>
    <NoPluralization>true</NoPluralization>
    <Password>AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAAg4VPWvAGA0K8d2W/MBVMggAAAAACAAAAAAAQZgAAAAEAACAAAAAsKI7PIeRb6nHL+Gvkk3laid/R6+o/PbO4YTeARPNJPQAAAAAOgAAAAAIAACAAAAABtp4lAH0bq0jHK0mHbRIAzMHx65wZrHVW6S9GVu6YaBAAAAC/G6+IFqdxCIM2menwE9AMQAAAACjN95MAN7Pa/6aRuTFlUvreV8mavBrvEJy9GDbt1qRmNMUry7OMWRtaSDA8zFyKKReQTq6MfMMaLBA3mLtpm3M=</Password>
    <DisplayName>bat.bat</DisplayName>
    <UserName>bat</UserName>
    <DriverData>
      <StripUnderscores>true</StripUnderscores>
      <QuietenAllCaps>true</QuietenAllCaps>
      <ConnectAs>Default</ConnectAs>
      <UseOciMode>true</UseOciMode>
    </DriverData>
  </Connection>
</Query>

delete from enbo_invoices where enbo_progress_id in (select transaction_id from BAT_INVOICES where to_char(INVOICE_DATE, 'YYYYMMDD') >= '20150201' and to_char(INVOICE_DATE, 'YYYYMMDD') < '20150301')
delete from erg_transactions where id_transaction_xdm in (select transaction_id from BAT_INVOICES where to_char(INVOICE_DATE, 'YYYYMMDD') >= '20150201' and to_char(INVOICE_DATE, 'YYYYMMDD') < '20150301')
delete from BAT_INVOICES where to_char(INVOICE_DATE, 'YYYYMMDD') >= '20150201' and to_char(INVOICE_DATE, 'YYYYMMDD') < '20150301'

select transaction_id from BAT_INVOICES where to_char(INVOICE_DATE, 'YYYYMMDD') >= '20150201' and to_char(INVOICE_DATE, 'YYYYMMDD') < '20150301'
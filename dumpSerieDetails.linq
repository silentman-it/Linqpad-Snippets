<Query Kind="Statements">
  <Connection>
    <ID>038b8f63-f730-4248-ba62-3e8029e097da</ID>
    <Persist>true</Persist>
    <Driver Assembly="IQDriver" PublicKeyToken="5b59726538a49684">IQDriver.IQDriver</Driver>
    <Provider>Devart.Data.Oracle</Provider>
    <CustomCxString>AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAAB9Ts6+RM60qFxFE9i5wwCAAAAAACAAAAAAADZgAAqAAAABAAAAAsryPMmtT8B0p+mcUGEduWAAAAAASAAACgAAAAEAAAACjoMzV7Rp2gSZXs9Sd3juNAAAAAzw8QwBOyEwheTnXuYqXZzosw30tqZIJsNpqmOSzNzusRk4yPrWsOsEvxrHzvJsXHjdbgLcVbh19Khwh8O+ZhLhQAAAD0wyby1KnVBlAQ2vx65lK4t+Psyg==</CustomCxString>
    <Server>tmstest</Server>
    <UserName>ed_warehouse</UserName>
    <Password>AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAAB9Ts6+RM60qFxFE9i5wwCAAAAAACAAAAAAADZgAAqAAAABAAAAC9JVvuvHEq2rhaRVOCRcHrAAAAAASAAACgAAAAEAAAAN0HGJDiJQ/uTX8jgg9DNVEQAAAAT6vf33G+QQpF1oE/8yjpZBQAAACRTTdDcAliCBUljk1wtNozl+UOdQ==</Password>
    <EncryptCustomCxString>true</EncryptCustomCxString>
    <DisplayName>tmstest.ed_warehouse</DisplayName>
    <DriverData>
      <StripUnderscores>true</StripUnderscores>
      <QuietenAllCaps>true</QuietenAllCaps>
      <ConnectAs>Default</ConnectAs>
      <UseOciMode>true</UseOciMode>
    </DriverData>
  </Connection>
</Query>

decimal SerieId = 108;

SeriesDetails
.Where(s => s.SerieID == SerieId)
.Dump();
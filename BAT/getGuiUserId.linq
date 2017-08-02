<Query Kind="Statements">
  <Connection>
    <ID>a093b9aa-b4b5-47ac-b26d-6948cf4e98dd</ID>
    <Persist>true</Persist>
    <Driver Assembly="IQDriver" PublicKeyToken="5b59726538a49684">IQDriver.IQDriver</Driver>
    <Provider>Devart.Data.Oracle</Provider>
    <CustomCxString>AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAABqjAv27bfEW6HYG6LADkfQAAAAACAAAAAAAQZgAAAAEAACAAAACUEHghYevS48rdtddP485Gc+3XN2MdeI+rigEnLluQwwAAAAAOgAAAAAIAACAAAACa4CVun92uNmFhf3s93ucML5HwDOqjoGkzU3dHpJhwgTAAAAAJoDAardKh0PE8RWdr3mfEsb6pVX3bLlxWCiQZ2IOdonh5Ft3ta2LknWKcVa0g/EpAAAAAS2taHs09/rMWUGQLVlK67RGcIo92G/qr4s0agkyJb0L9cbtSLokBxzoSoDJFw9X5Gd1dLezQbgai+LH9NW3GHA==</CustomCxString>
    <Server>bat</Server>
    <EncryptCustomCxString>true</EncryptCustomCxString>
    <NoPluralization>true</NoPluralization>
    <DisplayName>bat.gmebid</DisplayName>
    <UserName>gmebid</UserName>
    <Password>AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAABqjAv27bfEW6HYG6LADkfQAAAAACAAAAAAAQZgAAAAEAACAAAAB6lPCJBQ5EBGk6Lp/S/z4unPq8IDAn59WjEp+PhHXl+gAAAAAOgAAAAAIAACAAAAAIblgGnqnN9FV/C4bwM0dLFVbkA0RVapy5/L7X4bkcDhAAAABvtFxVdmuNuWch4NDqAf7XQAAAAOvMppBSXcEevrbyZFeENPTN7GqkA/VmYQDnRveRY1hopP5wTMAmD2C0OMyMYcnS7oCMBAg3sG0mhhxurH2bbGc=</Password>
    <DriverData>
      <StripUnderscores>true</StripUnderscores>
      <QuietenAllCaps>true</QuietenAllCaps>
      <ConnectAs>Default</ConnectAs>
      <UseOciMode>true</UseOciMode>
    </DriverData>
  </Connection>
  <Output>DataGrids</Output>
</Query>

string user = Console.ReadLine();

if(!string.IsNullOrEmpty(user))
{
	Guiuser
	.Where(x => x.LoginID == user)
	.Dump();
}
else
{
	Guiuser
	.Dump();
}
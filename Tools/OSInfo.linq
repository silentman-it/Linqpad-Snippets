<Query Kind="Statements">
  <Connection>
    <ID>a4da0b26-e220-492d-a3ae-4b29358701ef</ID>
    <Persist>true</Persist>
    <Server>172.19.122.63\,3433</Server>
    <SqlSecurity>true</SqlSecurity>
    <UserName>sa</UserName>
    <Password>AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAAvm6QbRxlJUuLCV01lsuo5AAAAAACAAAAAAAQZgAAAAEAACAAAABCZX2NEJwBBxkMXKe3E6O8nnZCKWTREELMs7QORETUmQAAAAAOgAAAAAIAACAAAACnWDa1IoHM8yW+QOJTbPTIJ6KBEZGaGMuiRljo/BHB3BAAAACjSL7Cv48d/LckZxgcQjtJQAAAAK7cjXK/2lUepF0NlbJVxfFOPO52z69ENMR99XvafjoOrKt5x2io/dvDOEHjzmB/Qe1BHgF5Ux/lGJ26GwyzvKQ=</Password>
    <Database>EDG_PCE_DEV_COPPOLA_FIRSTREVIEW</Database>
    <ShowServer>true</ShowServer>
  </Connection>
  <Reference>&lt;RuntimeDirectory&gt;\System.Management.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Configuration.Install.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\Microsoft.JScript.dll</Reference>
  <Namespace>System.Management</Namespace>
</Query>

//Environment.OSVersion.Dump();

var mos = new ManagementObjectSearcher("SELECT * FROM Win32_OperatingSystem").Get().Cast<ManagementObject>();
mos.Dump();
var name = mos.Select(x => x.GetPropertyValue("Caption")).FirstOrDefault();
name.Dump();
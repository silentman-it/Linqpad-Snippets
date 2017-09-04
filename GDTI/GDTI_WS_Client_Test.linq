<Query Kind="Program">
  <Reference>C:\Work\TMS\DEV\GDTI\Common\GDTI.Common\bin\Debug\GDTI.Common.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.ServiceModel.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Runtime.Serialization.dll</Reference>
  <Namespace>System.ServiceModel</Namespace>
  <Namespace>GDTI.Common.Utils</Namespace>
</Query>

void Main(string[] args)
{

	VMUtils.ServerURI = "http://win2008r2test1:83/TmsMainService.svc";
	VMUtils.ServerDNS = "localhost";
	VMUtils.SetCredentials("System", "pippo");
	VMUtils.SvcProxy.AuthenticateUser("System", "pippo", null).Dump();
	VMUtils.SvcProxy.GetUnits(null).Dump();
	
	
}
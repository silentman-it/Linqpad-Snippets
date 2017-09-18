<Query Kind="Program">
  <Reference>C:\Work\TMS\DEV\GDTI\Common\GDTI.Common\bin\Debug\GDTI.Common.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.ServiceModel.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Runtime.Serialization.dll</Reference>
  <Namespace>System.ServiceModel</Namespace>
  <Namespace>GDTI.Common.Utils</Namespace>
</Query>

void Main(string[] args)
{
	// IREN_TEST
	VMUtils.ServerURI = "http://172.16.1.83/TmsMainService.svc";
	VMUtils.ServerDNS = "srvegt01";
	
	
	VMUtils.SetCredentials("System", "pippo");
	//VMUtils.SvcProxy.AuthenticateUser("System", "pippo", null).Dump();
	var units = VMUtils.SvcProxy.GetUnits(null).Select(x => "\"" + x.ETSOCode + "\"").ToArray().Dump();
	
	string.Join(", ", units).Dump();
	
	
	
	
}
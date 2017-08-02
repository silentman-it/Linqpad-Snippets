<Query Kind="Program">
  <Reference>C:\Work\BAT\DEV\SRC\BatService\bin\x64\Debug\BATLibrary.dll</Reference>
  <Reference>C:\Work\BAT\DEV\SRC\BatService\bin\x64\Debug\EES.Framework.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\Microsoft.Transactions.Bridge.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\SMDiagnostics.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Configuration.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.DirectoryServices.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.EnterpriseServices.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.IdentityModel.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.IdentityModel.Selectors.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Messaging.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Net.Http.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Runtime.DurableInstancing.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Runtime.Serialization.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Security.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.ServiceModel.Activation.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.ServiceModel.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.ServiceModel.Internals.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.ServiceProcess.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Web.ApplicationServices.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Web.Services.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Xaml.dll</Reference>
  <Namespace>System.ServiceModel</Namespace>
  <Namespace>BATLibrary.Entities</Namespace>
</Query>

void Main()
{
	// 
	
	var myBinding = EES.Framework.ServiceModel.CustomBindings.GetTcpBinding();
	var myEndpoint = new EndpointAddress("net.tcp://localhost:9000/Bat/Test");
	var myChannelFactory = new ChannelFactory<ITestService>(myBinding, myEndpoint);
	
	ITestService client = myChannelFactory.CreateChannel();
	
    client.GetClosingDates(2015, null).Dump();
	client.GetCostCenters("BUY103", null).Dump();
	client.GetProfitCenters("SELL147", "TT").Dump();
	
    ((ICommunicationObject)client).Close();
}

// Define other methods and classes here
[ServiceContract(Namespace = "http://energy.selexelsag.it/BAT")]
public interface ITestService
{
	[OperationContract]
	DateTime[] GetClosingDates(int? year, int? month);
	
	[OperationContract]
	BatCostCenter[] GetCostCenters(string account, string tradeType);
	
	[OperationContract]
	BatProfitCenter[] GetProfitCenters(string account, string tradeType);
}

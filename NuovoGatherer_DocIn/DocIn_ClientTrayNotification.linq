<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.ServiceModel.dll</Reference>
  <Namespace>System.ServiceModel</Namespace>
</Query>

void Main(string[] args)
{
	ChannelFactory<ITrayOperationManager> pipeFactory = new ChannelFactory<ITrayOperationManager>(new NetNamedPipeBinding(), new EndpointAddress("net.pipe://localhost/TrayOperationManager"));
	ITrayOperationManager pipeProxy = pipeFactory.CreateChannel();
	//Console.WriteLine("received from pipe: " + pipeProxy.DoTrayOperation("CLOSE"));
	
	//pipeProxy.DoTrayOperation("asdasd");
	
	pipeProxy.Close();
}

[ServiceContract]
public interface ITrayOperationManager
{
   [OperationContract]
   string DoTrayOperation(string value);

   [OperationContract(IsOneWay = true)]
   void Close();
}
<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.ServiceModel.dll</Reference>
  <Namespace>System.ServiceModel</Namespace>
</Query>

void Main(string[] args)
{
	var binding = new NetTcpBinding()
	{
		ReliableSession = new OptionalReliableSession() { Enabled = true }
	};

	ChannelFactory<ITrayOperationManager> channelFactory = new ChannelFactory<ITrayOperationManager>(binding, new EndpointAddress("net.tcp://localhost:9001/TrayOperationManager"));
	ITrayOperationManager proxy = channelFactory.CreateChannel();
	//Console.WriteLine("received from pipe: " + pipeProxy.DoTrayOperation("CLOSE"));
	
	proxy.DoTrayOperation("asdasd").Dump();
	
	//proxy.Ping();
	
	proxy.Close();
}

[ServiceContract]
public interface ITrayOperationManager
{
   [OperationContract]
   string DoTrayOperation(string value);

   [OperationContract(IsOneWay = true)]
   void Close();

   [OperationContract(IsOneWay = true)]
   void Ping();

}
<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.ServiceModel.dll</Reference>
  <Namespace>System.ServiceModel</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
  <Namespace>System.ServiceModel.Description</Namespace>
</Query>

void Main(string[] args)
{
	var host = new ServiceHost(typeof(TrayOperationManager), new Uri[] { new Uri("net.tcp://localhost:9001/") });
	var binding = new NetTcpBinding()
	{
		ReliableSession = new OptionalReliableSession() { Enabled = true, InactivityTimeout = TimeSpan.FromMinutes(3) }
	};
	

	host.AddServiceEndpoint(typeof(ITrayOperationManager), binding, "TrayOperationManager");

	
	host.Open();
	
	Console.WriteLine ("Service is active. Press enter to stop");
	Console.ReadLine();
	host.Close();
	Console.WriteLine ("Service closed!");
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

public class TrayOperationManager : ITrayOperationManager
{
	public string DoTrayOperation(string value)
	{
		Console.WriteLine ("[{0}] Invocato metodo DoTrayOperation()", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.fff"));
		return value.ToUpper();
	}
	
	public void Close()
	{
	}
	
	public void Ping()
	{
	}
}

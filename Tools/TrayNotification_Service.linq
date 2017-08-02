<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.ServiceModel.dll</Reference>
  <Namespace>System.ServiceModel</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

void Main(string[] args)
{
	var host = new ServiceHost(typeof(TrayOperationManager), new Uri[] { new Uri("net.pipe://localhost") });
	host.AddServiceEndpoint(typeof(ITrayOperationManager), new NetNamedPipeBinding(), "TrayOperationManager");
	host.Open();
	
	Console.WriteLine ("Press enter to stop");
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
		Console.WriteLine ("Invocato metodo DoTrayOperation()");
		return value.ToUpper();
	}
	
	public void Close()
	{
	}
	
	public void Ping()
	{
	}
}
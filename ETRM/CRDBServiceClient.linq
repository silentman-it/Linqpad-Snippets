<Query Kind="Program">
  <Reference>&lt;MyDocuments&gt;\Visual Studio 2013\Projects\CRDBServiceProxy\CRDBServiceProxy\bin\Debug\CRDBServiceProxy.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Runtime.Serialization.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.ServiceModel.dll</Reference>
  <Namespace>System.ServiceModel</Namespace>
  <Namespace>EES.ETRM.Framework.Enums</Namespace>
</Query>

void Main(string[] args)
{
	ChannelFactory<IOrders> channelFactory = new ChannelFactory<IOrders>(GetTcpBinding(), new EndpointAddress("net.tcp://172.19.122.183:800/EES.ETRM.PFM.CMWcfServiceLib/CRDBService/Orders"));
	IOrders px = channelFactory.CreateChannel();

	px.GetByCompetenceDate(null , null, null, DateTime.Parse("01/05/2016"), DateTime.Parse("31/05/2016"), true, null, null, 0, Int32.MaxValue).Dump();
}


public static NetTcpBinding GetTcpBinding()
{
	NetTcpBinding binding = new NetTcpBinding(SecurityMode.None) { TransactionFlow = true };
	binding.ReaderQuotas.MaxStringContentLength = int.MaxValue;
	binding.ReaderQuotas.MaxArrayLength = int.MaxValue;
	binding.MaxReceivedMessageSize = int.MaxValue;
	binding.SendTimeout = binding.OpenTimeout = binding.CloseTimeout = new TimeSpan(0, 14, 00); //
	binding.ReceiveTimeout = new TimeSpan(0, 14, 00);
	binding.MaxBufferSize = int.MaxValue;
	binding.ReliableSession.Enabled = true;
	return binding;
}
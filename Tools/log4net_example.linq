<Query Kind="Program">
  <Connection>
    <ID>ee4a399f-2c42-4996-a7dc-d4d4e7237141</ID>
    <Persist>true</Persist>
    <Driver Assembly="IQDriver" PublicKeyToken="5b59726538a49684">IQDriver.IQDriver</Driver>
    <Provider>Devart.Data.Oracle</Provider>
    <CustomCxString>AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAAg4VPWvAGA0K8d2W/MBVMggAAAAACAAAAAAAQZgAAAAEAACAAAADglTTx3C11jhPYnlj4yIn6F12XUCZG0jmhAgSxvy/GwAAAAAAOgAAAAAIAACAAAAAkpGpkXXL/W/KeyPCYOXNDtzoDfHW1+MJF9HRtISTTTzAAAABE7dxKTiZ2F5FPsgCw2UdeJFhZd6h0jm1G6uaN7GxpocLSPaMM8x0TNmcKnyedBNNAAAAAjEojooi7KINAFNFqDlqlA0kcGPNaovF/EROD2VWCi3/qgTuH/J9pCzupmAUfrrruAY27Lc6OXvlQ5S+J3P29yg==</CustomCxString>
    <Server>svil11</Server>
    <EncryptCustomCxString>true</EncryptCustomCxString>
    <NoPluralization>true</NoPluralization>
    <Password>AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAAg4VPWvAGA0K8d2W/MBVMggAAAAACAAAAAAAQZgAAAAEAACAAAAAsKI7PIeRb6nHL+Gvkk3laid/R6+o/PbO4YTeARPNJPQAAAAAOgAAAAAIAACAAAAABtp4lAH0bq0jHK0mHbRIAzMHx65wZrHVW6S9GVu6YaBAAAAC/G6+IFqdxCIM2menwE9AMQAAAACjN95MAN7Pa/6aRuTFlUvreV8mavBrvEJy9GDbt1qRmNMUry7OMWRtaSDA8zFyKKReQTq6MfMMaLBA3mLtpm3M=</Password>
    <DisplayName>svil11.bat</DisplayName>
    <UserName>bat</UserName>
    <DriverData>
      <StripUnderscores>true</StripUnderscores>
      <QuietenAllCaps>true</QuietenAllCaps>
      <ConnectAs>Default</ConnectAs>
      <UseOciMode>true</UseOciMode>
    </DriverData>
  </Connection>
  <NuGetReference>Eaardal.OracleDataAccess</NuGetReference>
  <NuGetReference>log4net</NuGetReference>
  <Namespace>log4net</Namespace>
  <Namespace>log4net.Appender</Namespace>
  <Namespace>log4net.Config</Namespace>
  <Namespace>log4net.Layout</Namespace>
  <Namespace>log4net.Repository.Hierarchy</Namespace>
</Query>

void Main()
{
	//Hierarchy hierarchy = (Hierarchy)LogManager.GetRepository();
	//hierarchy.Root.RemoveAllAppenders();
	//
	//FileAppender fileAppender = new FileAppender();
	//fileAppender.AppendToFile = true;
	//fileAppender.LockingModel = new FileAppender.MinimalLock();
	//fileAppender.File = @"C:\temp\log4net_log.txt";
	//PatternLayout pl = new PatternLayout();
	//pl.ConversionPattern = "%d [%2%t] %-5p [%-10c] %m%n";
	//pl.ActivateOptions();
	//fileAppender.Layout = pl;
	//fileAppender.ActivateOptions();
	
	//BasicConfigurator.Configure(fileAppender);
	//AdoNetAppender dba = new AdoNetAppender()
	//{
	//}
	
	
	
	
	XmlConfigurator.Configure(new FileInfo(@"C:\temp\log4net_config.xml"));
	
	ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

	var h = LogManager.GetRepository();
	var appenders = h.GetAppenders();
	var adoAppender = (AdoNetAppender)appenders.Where(x => x.Name == "AdoNetAppender").SingleOrDefault();
	//adoAppender.ConnectionString = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=172.19.97.204)(PORT=1522))(CONNECT_DATA=(SERVER=dedicated)(SERVICE_NAME=svil11)));User Id=BAT;Password=BAT";
	adoAppender.ActivateOptions();
		
	//Test logger
	while(true)
	{
		log.DebugFormat("Testing now {0}", Guid.NewGuid().ToString() );
		Thread.Sleep(2000);
	}
	
}


<Query Kind="Program">
  <NuGetReference>SAPNCo.x64</NuGetReference>
  <Namespace>SAP.Middleware.Connector</Namespace>
</Query>

void Main()
{
	ECCDestinationConfig cfg = new ECCDestinationConfig();
	
	if(!RfcDestinationManager.IsDestinationConfigurationRegistered())
	{
		RfcDestinationManager.RegisterDestinationConfiguration(cfg);
	}
	
	RfcDestination dest = RfcDestinationManager.GetDestination("mySAPdestination");
	RfcRepository repo = dest.Repository;
	IRfcFunction testfn = repo.CreateFunction("ZMARA_READ");
	
	testfn.Invoke(dest);
	
	var companyCodeList = testfn.GetTable("E_MARA");
	
	// companyCodeList now contains a table with companies and codes
}


public class ECCDestinationConfig : IDestinationConfiguration
{

   public bool ChangeEventsSupported()
   {
       return false;
   }

   public event RfcDestinationManager.ConfigurationChangeHandler ConfigurationChanged;

   public RfcConfigParameters GetParameters(string destinationName)
   {

       RfcConfigParameters parms = new RfcConfigParameters();

       if (destinationName.Equals("mySAPdestination"))
       {
           parms.Add(RfcConfigParameters.AppServerHost, "10.1.100.213");
           parms.Add(RfcConfigParameters.SystemNumber, "01");
           parms.Add(RfcConfigParameters.SystemID, "L2E");
           parms.Add(RfcConfigParameters.User, "5SELEX_SAP");
           parms.Add(RfcConfigParameters.Password, "Init_2015");
           parms.Add(RfcConfigParameters.Client, "900");
           parms.Add(RfcConfigParameters.Language, "EN"); 
           parms.Add(RfcConfigParameters.PoolSize, "5");
       }
       return parms;

   }
}
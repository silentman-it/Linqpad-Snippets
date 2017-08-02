<Query Kind="Program">
  <Reference>C:\Work\TMS\DEV\Tools\Energy.Licensing\Energy.Licensing\bin\Debug\Energy.Licensing.dll</Reference>
  <Namespace>Energy.Licensing</Namespace>
</Query>

void Main()
{
	var baseDir = @"C:\Work\TMS\DEV\Tools\Licenses";
	var expirationDate = DateTime.MaxValue;
	
	Scenario[] Scenarios = {
//		new Scenario("GDTI_Base", "ISAB",
//			LicenseNamesConst.GDTIBase),
//		new Scenario("GDTI_Base_ProdOverview", "IREN",
//			LicenseNamesConst.GDTIBase,
//			LicenseNamesConst.ProductionOverview),
//		new Scenario("GDTI_Base_UoM_RTS", "Sorgenia",
//			LicenseNamesConst.GDTIBase,
//			LicenseNamesConst.UnitOpsManager,
//			LicenseNamesConst.RealTimeStrategy),
//		new Scenario("GDTI_Base_Gas_ProdOverview", "TirrenoPower",
//			LicenseNamesConst.GDTIBase,
//			LicenseNamesConst.GasConsumption,
//			LicenseNamesConst.ProductionOverview),
		new Scenario("GDTI_DEBUG_NotForDeployment", "DEBUG",
			LicenseNamesConst.GDTIBase,
			LicenseNamesConst.GasConsumption,
			LicenseNamesConst.ProductionOverview,
			LicenseNamesConst.RealTimeStrategy,
			LicenseNamesConst.UnitOpsManager),

	};
	
	foreach (var scenario in Scenarios)
	{
		var scenarioDir = Path.Combine(baseDir, scenario.Name);
		if(!Directory.Exists(scenarioDir)) { Directory.CreateDirectory(scenarioDir); }
		var licenseFile = Path.Combine(scenarioDir, "License.xml");
		LicenseFile lf = new LicenseFile();
		
		foreach (var feat in scenario.Features)
		{
			lf.Features.Add(new LicenseFileFeature() { Key = feat, Expiration = expirationDate, Parameters = null });			
		}
		
		// SERIALIZZAZIONE E FIRMA
		XmlSerializationHelper.Serialize(licenseFile, lf);
		SignedXmlHelper.SignXml(licenseFile);
		
		lf.Dump(licenseFile);
		
		// VERIFICA FIRMA e DESERIALIZZAZIONE
		//	SignedXmlHelper.VerifyXml(fileName).Dump("Verifica XML firmato");
		//	XmlSerializationHelper.Deserialize<LicenseFile>(fileName).Dump("Deserializzazione XML");
		
		
	}
	
}

// Define other methods and classes here
public static class LicenseNamesConst
{
        public static readonly string GDTIBase = "GDTIBase";
        public static readonly string RealTimeStrategy = "RealTimeStrategy";
        public static readonly string UnitOpsManager = "UnitOpsManager";
        public static readonly string GasConsumption = "GasConsumption";
        public static readonly string ProductionOverview = "ProductionOverview";
}

public class Scenario
{
	public string Name { get; set; }
	public string[] Features { get; set; }
	public string Customers { get; set; }
	
	
	public Scenario(string name, string customers, params string[] feats)
	{
		Name = name;
		Customers = customers;
		Features = feats;		
	}
}
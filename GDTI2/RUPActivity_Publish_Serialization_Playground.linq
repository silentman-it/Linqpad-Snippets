<Query Kind="Program">
  <Connection>
    <ID>c0b901f2-09b0-4fb4-8578-e8c692ab9ed2</ID>
    <Persist>true</Persist>
    <Driver>EntityFrameworkDbContext</Driver>
    <CustomAssemblyPath>C:\Work\TMS\DEV\TMS\Common\GDTI.Data\bin\Debug\GDTI.Data.dll</CustomAssemblyPath>
    <CustomTypeName>GDTI.Data.EDMWarehouseEntities</CustomTypeName>
    <AppConfigPath>C:\Work\TMS\DEV\TMS\Web\TMS.WS\Web.config</AppConfigPath>
  </Connection>
  <Reference>C:\Work\TMS\DEV\TMS\Activities\GDTI.Activities.RUP\bin\Debug\GDTI.Activities.RUP.dll</Reference>
  <Reference>C:\Work\TMS\DEV\TMS\Activities\GDTI.Activities.RUP\bin\Debug\GDTI.Data.dll</Reference>
  <Namespace>GDTI.Activities.RUP.Schemas.DynamicRUP.Outbound</Namespace>
  <Namespace>GDTI.Activities.RUP.Schemas.StaticRUP.Inbound</Namespace>
  <Namespace>GDTI.Data.Extensions</Namespace>
  <Namespace>System.Globalization</Namespace>
</Query>

void Main()
{
	DateTime flowDate = DateTime.Parse("26/10/2014");
	string unitName = "UP_TURBIGO_4";
	
	var rupLocal = GDTIData.Instance.Get("RUPLocal", unitName, null, null, null, flowDate, flowDate.AddDays(1));
	var rupTernaVariationID = GDTIData.Instance.Get("RUPTerna", unitName, null, "VariationID", null, flowDate, flowDate.AddDays(1));
	
	rupLocal = rupLocal.Concat(rupTernaVariationID);
	
	FLUSSO flusso = new FLUSSO();
	
	var data = rupLocal;
	
	var table = data
	.SelectMany(x => x.Data.Select(d => new
	{
		x.Source,
		x.Symbol,
		x.Class,
		x.Channel,
		d.Key,
		d.Value
	}))
	.ToPivotTable(x => x.Key.ToString(), x => x.Channel, x => x.First().Value)
	.AsEnumerable()
	.Cast<DataRow>()
	//.Dump()
	;
	
	
	flusso.INSERISCI.Items.AddRange(
	table.Select(x => new VDT()
	{
		CODICEETSO = unitName,
		DATAORAINIZIO = DateTime.Parse((string)x["Key"]),
		DATAORAFINE = DateTime.Parse((string)x["Key"]).AddMinutes(15),
		StartDateWithTimeZone = DateTime.Parse((string)x["Key"]),
		EndDateWithTimeZone = DateTime.Parse((string)x["Key"]).AddMinutes(15),
		IDMOTIVAZIONE = (string)x["MotivationID"],
		NOTE = (string)x["Note"],
		FASCIA = new List<FASCIAUPAType>
		{
			new FASCIAUPAType()
			{
				PSMIN = (float)Convert.ToDouble(x["PSMIN"]),
				PSMAX = (float)Convert.ToDouble(x["PSMAX"]),
				ASSETTO = new FASCIAUPATypeASSETTO()
				{
					IDASSETTO = StringToByteArray((string)x["Assetto"]),
					PTMIN = (float)Convert.ToDouble(x["PTMIN"]),
					PTMAX = (float)Convert.ToDouble(x["PTMAX"]),
					BRS = (float)Convert.ToDouble(x["BRS"]),
					GPA = (float)Convert.ToDouble(x["GPA"]),
					GPD = (float)Convert.ToDouble(x["GPD"]),
					TARA = (float)Convert.ToDouble(x["TARA"]),
					TAVA = (float)Convert.ToDouble(x["TAVA"]),
					TDERAMPA = (float)Convert.ToDouble(x["TDERAMPA"]),
					TDERAMPASpecified = x["TDERAMPA"] != null,
					TRISP = (float)Convert.ToDouble(x["TRISP"]),
				}
			}
		}
	})
	)
	;
	
	flusso.MODIFICA = null;
	flusso.ELIMINA = null;
	

	flusso.Serialize().Dump();
}

// Define other methods and classes here
public static byte[] StringToByteArray(String hex)
{
  int NumberChars = hex.Length;
  byte[] bytes = new byte[NumberChars / 2];
  for (int i = 0; i < NumberChars; i += 2)
    bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
  return bytes;
}
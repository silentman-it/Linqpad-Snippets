<Query Kind="Program">
  <Connection>
    <ID>c0b901f2-09b0-4fb4-8578-e8c692ab9ed2</ID>
    <Persist>true</Persist>
    <Driver>EntityFrameworkDbContext</Driver>
    <CustomAssemblyPath>C:\Work\TMS\DEV\TMS\Common\GDTI.Data\bin\Debug\GDTI.Data.dll</CustomAssemblyPath>
    <CustomTypeName>GDTI.Data.EDMWarehouseEntities</CustomTypeName>
    <AppConfigPath>C:\Work\TMS\DEV\TMS\Web\TMS.WS\Web.config</AppConfigPath>
  </Connection>
  <Reference>C:\Work\TMS\DEV\TMS\Activities\GDTI.Activities.RealTime\bin\Debug\GDTI.Activities.RealTime.dll</Reference>
  <Reference>C:\Work\TMS\DEV\TMS\Activities\GDTI.Activities.RUP\bin\Debug\GDTI.Activities.RUP.dll</Reference>
  <Namespace>GDTI.Activities.RUP.Schemas.MIB.Outbound</Namespace>
  <Namespace>System.Globalization</Namespace>
</Query>

void Main()
{
	var fileName = @"\\edggepwsv004.edg.grptop.net\Repository ENY\AA DOSSIER TECNICO\GestoreDatiTecniciImpianto (G-DTI)\Test\FileTernaReali - NON MODIFICARE\Inserisci - INVIO A TERNA\MIB_20120517_20120515111239.xml";
	
	var path = @"C:\Temp\Archive\";
	var searchPattern = @"*_INS.xml";
	var fileNames = Directory.GetFiles(path, searchPattern, SearchOption.AllDirectories);
	
	
//	foreach(var fileName in fileNames)
//	{
		DoStuff(fileName);
//	}
	
	
}

private void DoStuff(string fileName)
{
	var o = FLUSSO.LoadFromFile(fileName);
	o.Dump();
	
	string src = "MIBTerna";
	string frq = "IRREGULAR";
	
	List<GDTIDataItem> lsDataItems = new List<GDTIDataItem>();
	
	foreach(var mib in o.INSERISCI)
	{
		lsDataItems.Add(new GDTIDataItem()
		{
			Source = src,
			Symbol = mib.CODICEETSO,
			Class = "MotivationID",
			Frequency = frq,
			UoM = "NONE",
			ExtendedData = GetExtendedData(mib, mib.IDMOTIVAZIONE)
		});
		lsDataItems.Add(new GDTIDataItem()
		{
			Source = src,
			Symbol = mib.CODICEETSO,
			Class = "Note",
			Frequency = frq,
			UoM = "NONE",
			ExtendedData = GetExtendedData(mib, mib.NOTE)
		});
		
	}
	

	lsDataItems.Dump();
	
}

Dictionary<GDTITimeRange, object> GetExtendedData(FLUSSOMIB mib, string v)
{
	DateTimeOffset s = RoundDown(mib.DATAORAINIZIO, TimeSpan.FromMinutes(1));
	DateTimeOffset e = RoundDown(mib.DATAORAFINE, TimeSpan.FromMinutes(1));
	
	Dictionary<GDTITimeRange, object> o = new Dictionary<GDTITimeRange, object>();
	
	o.Add(new GDTITimeRange(s, e), v);
	
	return o;
}

private DateTime RoundDown(DateTime dt, TimeSpan d)
{
	var res_ticks = dt.Ticks - (dt.Ticks % d.Ticks);
	return new DateTime(res_ticks);
}
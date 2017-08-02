<Query Kind="Statements">
  <Reference>C:\Work\TMS_PRJ\DEV\TMS\Common\TMS.Common\bin\Debug\TMS.Common.dll</Reference>
  <Namespace>TMS.Common.Utils.Serialization</Namespace>
  <Namespace>TMS.Common.Types.Schemas.VDT</Namespace>
  <Namespace>TMS.Common.Utils.Xml</Namespace>
  <Namespace>TMS.Common.Utils</Namespace>
</Query>

// Directory in
string sSourceDir = @"C:\Users\CoppolaF\Desktop\Ricerca VDT e MIB Produzone\Report_ritorno";

// Directory out
string sDestDir = @"C:\Temp";

// File(s) da splittare
string[] aFiles = Directory.GetFiles(sSourceDir, "*VDT.xml", SearchOption.TopDirectoryOnly);

// ----------------------------------------------------------------------
// Non toccare nulla sotto questa riga
// ----------------------------------------------------------------------




foreach (var f in aFiles)
{
	//const string newNamespaceUri = "http://www.terna.it/wsl/services/scweb/report";
	const string newNamespaceUri = "http://terna.it/scweb/xmlbeans";
	
	string s = ASCIIEncoding.ASCII.GetString(File.ReadAllBytes(Path.Combine(sSourceDir, f)));
	XmlDocument xmlDoc = new XmlDocument();
	xmlDoc.LoadXml(s);
	XmlAttribute xmlNsAttr = xmlDoc.DocumentElement.Attributes["xmlns"];
	
	if (xmlNsAttr != null)
	{
		xmlNsAttr.Value = newNamespaceUri;
		s = xmlDoc.OuterXml;
	}


	var o = XmlSerialization.Deserialize<VDTListType>(s);

	var grp = o.VDT.GroupBy(x => DateTimeUtility.StrFormat2DateTime(x.DATAORAINIZIO, "yyyy-MM-ddTHH:mm:ss").Date);
	
	foreach(var grpDay in grp)
	{
		DateTime flowDate = grpDay.Key;
		string unitName = grpDay.First().CODICEETSO;
		VDTListType newFileStub = new VDTListType();
		newFileStub.VDT = grpDay.ToArray();
		
		string newFileName = string.Format("{0}_VDT_{1}_SPLITTED.xml", unitName, flowDate.ToString("yyyyMMdd"));
		
		if(!Directory.Exists(Path.Combine(sDestDir, Path.GetFileNameWithoutExtension(f))))
		{
			Directory.CreateDirectory(Path.Combine(sDestDir, Path.GetFileNameWithoutExtension(f)));
		}
		
		File.WriteAllText(Path.Combine(sDestDir, Path.GetFileNameWithoutExtension(f), newFileName), XmlSerialization.Serialize(newFileStub));

		Console.WriteLine ("File written: " + newFileName);
	}

}
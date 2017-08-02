<Query Kind="Statements">
  <Reference>C:\Work\TMS\DEV\Tools\Energy.Licensing\Energy.Licensing\bin\Debug\Energy.Licensing.dll</Reference>
  <Namespace>Energy.Licensing</Namespace>
</Query>

string baseDir = @"C:\Work\TMS\DEV\Tools\Licenses\";

foreach(var fileName in Directory.GetFiles(baseDir, "License.xml", SearchOption.AllDirectories))
{
	// VERIFICA FIRMA e DESERIALIZZAZIONE
	if(!SignedXmlHelper.VerifyXml(fileName)) throw new Exception(string.Format("Invalid signature: {0}", fileName));
	XmlSerializationHelper.Deserialize<LicenseFile>(fileName).Features.Dump(fileName);
}
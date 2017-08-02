<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Security.dll</Reference>
  <Namespace>System.Security.Cryptography</Namespace>
  <Namespace>System.Security.Cryptography.Xml</Namespace>
</Query>

void Main()
{
	string keyFile = @"C:\Work\TMS\DEV\TMS\Common\TMS.Common\Types\Constants\DigitalSignatureKeyPair.xml";
	var rsaKey = RSASignatureHelper.LoadRSAKey(keyFile, false);
	
	ASCIIEncoding ByteConverter = new ASCIIEncoding();
	string dataString = "Some content to sign!!!!";
	byte[] bytesToSign = ByteConverter.GetBytes(dataString);

	var sig = RSASignatureHelper.GetExternalSignature(bytesToSign, rsaKey);
	var displayableSignature = Convert.ToBase64String(sig).Dump("Signature BASE64");
	BitConverter.ToString(sig).Dump("Signature hash");
	
	//bytesToSign[0] = 44; // Questo rompe la verifica
	var verified = RSASignatureHelper.VerifySignature(bytesToSign, Convert.FromBase64String(displayableSignature), rsaKey);
	
	verified.Dump("Verified?");

}

public static class RSASignatureHelper
{
   public static readonly string KeyContainerName = "DSIG_RSA_KEY";

   public static RSACryptoServiceProvider LoadRSAKey(string xmlResource, bool isEmbedded)
   {
       CspParameters cspParams = new CspParameters();
       RSACryptoServiceProvider rsaKey = new RSACryptoServiceProvider(cspParams);

       if (isEmbedded)
       {
           rsaKey.FromXmlString(GetAssemblyEmbeddedStreamContent(xmlResource));
       }
       else
       {
           rsaKey.FromXmlString(GetFileStreamContent(xmlResource));
       }

       return rsaKey;
   }

   public static string GetNewRSAKey(bool includePrivate)
   {
		return GetNewRSAKey(includePrivate, KeyContainerName);
   }

   public static string GetNewRSAKey(bool includePrivate, string keyContainerName)
   {
		CspParameters cspParams = new CspParameters();
		cspParams.KeyContainerName = keyContainerName;
		RSACryptoServiceProvider rsaKey = new RSACryptoServiceProvider(cspParams);
		return rsaKey.ToXmlString(includePrivate);
   }

   public static string GetAssemblyEmbeddedStreamContent(string resourceKey)
   {
		Assembly assembly = Assembly.GetExecutingAssembly();
		Stream s = assembly.GetManifestResourceStream(resourceKey);
		StreamReader sr = new StreamReader(s);
		return sr.ReadToEnd();
   }

   public static string GetFileStreamContent(string resourceFile)
   {
		return File.ReadAllText(resourceFile);
   }
	
	public static byte[] GetExternalSignature(byte[] inputData, RSA Key)
	{
		RSACryptoServiceProvider RSAalg = new RSACryptoServiceProvider();
		RSAalg.ImportParameters(Key.ExportParameters(true));
		return RSAalg.SignData(inputData, new SHA256CryptoServiceProvider());
	}
	
	public static bool VerifySignature(byte[] inputData, byte[] Signature, RSA Key)
	{
		RSACryptoServiceProvider RSAalg = new RSACryptoServiceProvider();
		RSAalg.ImportParameters(Key.ExportParameters(false));
		return RSAalg.VerifyData(inputData, new SHA256CryptoServiceProvider(), Signature); 
	}

}
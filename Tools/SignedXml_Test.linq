<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Security.dll</Reference>
  <Namespace>System.Security.Cryptography</Namespace>
  <Namespace>System.Security.Cryptography.Xml</Namespace>
</Query>

void Main()
{
	string keyFile = @"C:\Work\TMS\DEV\TMS\Common\TMS.Common\Types\Constants\DigitalSignatureKeyPair.xml";
	var rsaKey = SignedXmlHelper.LoadRSAKey(keyFile, false);

	string signeXmlContent = File.ReadAllText(@"C:\Users\Federico\Desktop\UP_NRGAMOLISE_1_GenericSerie_20150727_20150730033003.xml");
	SignedXmlHelper.VerifyXml(signeXmlContent, rsaKey).Dump("Verified?");
	
//	var rsaKey = new RSACryptoServiceProvider();
//	rsaKey.ExportParameters(false);
//	var signedContenxt = SignedXmlHelper.SignXml("<test>asd</test>", rsaKey).Dump("Signed Content");
//	SignedXmlHelper.VerifyXml(signedContenxt, rsaKey).Dump("Verified?");
}

// Define other methods and classes here
    public static class SignedXmlHelper
    {
        public static readonly string KeyContainerName = "XML_DSIG_RSA_KEY";

        public static RSACryptoServiceProvider LoadRSAKey(string xmlResource, bool isEmbedded)
        {
            // Create a new CspParameters object to specify
            // a key container.
            CspParameters cspParams = new CspParameters();
            cspParams.KeyContainerName = KeyContainerName;

            // Create a new RSA signing key and save it in the container. 
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


            // Create a new RSA signing key and save it in the container. 
            RSACryptoServiceProvider rsaKey = new RSACryptoServiceProvider(cspParams);

            return rsaKey.ToXmlString(includePrivate);
        }

        public static string SignXml(string xmlContent, RSA Key)
        {

            // Create a new XML document.
            XmlDocument xmlDoc = new XmlDocument();

            // Load an XML file into the XmlDocument object.
            xmlDoc.PreserveWhitespace = true;
            xmlDoc.LoadXml(xmlContent);

            // Sign the XML document. 
            SignXml(xmlDoc, Key);

            // Save the document.
            return xmlDoc.OuterXml;
        }

        public static bool VerifyXml(byte[] xmlContent, RSA Key)
        {
            string xmlString;

            System.Text.UTF8Encoding enc = new System.Text.UTF8Encoding();
            xmlString = enc.GetString(xmlContent);

            return VerifyXml(xmlString, Key);
        }

        public static bool VerifyXml(string xmlContent, RSA Key)
        {
            // Create a new XML document.
            XmlDocument xmlDoc = new XmlDocument();

            // Load an XML file into the XmlDocument object.
            xmlDoc.PreserveWhitespace = true;
            xmlDoc.LoadXml(xmlContent);

            // Verify the signature of the signed XML.
            bool result = VerifyXml(xmlDoc, Key);

            return result;
        }



        public static void SignXml(string xmlFilename)
        {

            // Create a new XML document.
            XmlDocument xmlDoc = new XmlDocument();

            // Load an XML file into the XmlDocument object.
            xmlDoc.PreserveWhitespace = true;
            xmlDoc.Load(xmlFilename);


#if DEBUG
            string keypairFileName = @".\Keys\KeyPair.xml";
#else
            string keypairFileName = @".\KeyPair.xml";
#endif

            RSACryptoServiceProvider rsaKey = LoadRSAKey(keypairFileName, false);

            // Sign the XML document. 
            SignXml(xmlDoc, rsaKey);

            // Save the document.
            xmlDoc.Save(xmlFilename);
        }

        public static bool VerifyXml(string xmlFileName)
        {
            RSACryptoServiceProvider rsaKey = LoadRSAKey("Energy.Licensing.Keys.PublicKey.xml", true);

            // Create a new XML document.
            XmlDocument xmlDoc = new XmlDocument();

            // Load an XML file into the XmlDocument object.
            xmlDoc.PreserveWhitespace = true;
            xmlDoc.Load(xmlFileName);

            // Verify the signature of the signed XML.
            bool result = VerifyXml(xmlDoc, rsaKey);

            return result;
        }

        static void SignXml(XmlDocument xmlDoc, RSA Key)
        {
            // Check arguments.
            if (xmlDoc == null)
                throw new ArgumentException("xmlDoc");
            if (Key == null)
                throw new ArgumentException("Key");

            // Create a SignedXml object.
            SignedXml signedXml = new SignedXml(xmlDoc);

            // Add the key to the SignedXml document.
            signedXml.SigningKey = Key;

            // Create a reference to be signed.
            Reference reference = new Reference();
            reference.Uri = "";

            // Add an enveloped transformation to the reference.
            XmlDsigEnvelopedSignatureTransform env = new XmlDsigEnvelopedSignatureTransform();
            reference.AddTransform(env);

            // Add the reference to the SignedXml object.
            signedXml.AddReference(reference);

            // Compute the signature.
            signedXml.ComputeSignature();

            // Get the XML representation of the signature and save
            // it to an XmlElement object.
            XmlElement xmlDigitalSignature = signedXml.GetXml();

            // Append the element to the XML document.
            xmlDoc.DocumentElement.AppendChild(xmlDoc.ImportNode(xmlDigitalSignature, true));

        }

        static Boolean VerifyXml(XmlDocument Doc, RSA Key)
        {
            // Check arguments.
            if (Doc == null)
                throw new ArgumentException("Doc");
            if (Key == null)
                throw new ArgumentException("Key");

            // Create a new SignedXml object and pass it
            // the XML document class.
            SignedXml signedXml = new SignedXml(Doc);

            // Find the "Signature" node and create a new
            // XmlNodeList object.
            XmlNodeList nodeList = Doc.GetElementsByTagName("Signature");

            // Throw an exception if no signature was found.
            if (nodeList.Count <= 0)
            {
                throw new CryptographicException("Verification failed: No Signature was found in the document.");
            }

            // This example only supports one signature for
            // the entire XML document.  Throw an exception 
            // if more than one signature was found.
            if (nodeList.Count >= 2)
            {
                throw new CryptographicException("Verification failed: More that one signature was found for the document.");
            }

            // Load the first <signature> node.  
            signedXml.LoadXml((XmlElement)nodeList[0]);

            // Check the signature and return the result.
            return signedXml.CheckSignature(Key);
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

    }
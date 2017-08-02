<Query Kind="Statements">
  <Namespace>System.Security.Cryptography</Namespace>
</Query>

//Generate a public/private key pair.
RSACryptoServiceProvider RSA = new RSACryptoServiceProvider(384);
//Save the public key information to an RSAParameters structure.
RSAParameters RSAKeyInfo = RSA.ExportParameters(false);

// Export public key only
RSA.ToXmlString(false).Dump();

// Export keypair
RSA.ToXmlString(true).Dump();

//RSA.Dump();



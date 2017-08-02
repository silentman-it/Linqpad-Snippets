<Query Kind="Program">
  <Namespace>System.Security.Cryptography</Namespace>
</Query>

void Main()
{
	var password = "admin";
	var salt = GenerateRandomData(100);
	
	salt.Dump("Salt");
	
	var hash = SHA256Encode(salt + password);
	
	hash.Dump("Hash");

}

// Define other methods and classes here
public static string MD5Encode(string clearText)
{
	Byte[] originalBytes;
	Byte[] encodedBytes;
	MD5 md5;

	md5 = new MD5CryptoServiceProvider();
	originalBytes = ASCIIEncoding.Default.GetBytes(clearText);
	encodedBytes = md5.ComputeHash(originalBytes);

	return Convert.ToBase64String(encodedBytes);
}

public static string SHA256Encode(string clearText)
{
	Byte[] originalBytes;
	Byte[] encodedBytes;
	SHA256 sha2;

	sha2 = new SHA256CryptoServiceProvider();
	originalBytes = ASCIIEncoding.Default.GetBytes(clearText);
	encodedBytes = sha2.ComputeHash(originalBytes);

	return Convert.ToBase64String(encodedBytes);
}

public static string SHA1Encode(string clearText)
{
	Byte[] originalBytes;
	Byte[] encodedBytes;
	SHA1 sha1;

	sha1 = new SHA1CryptoServiceProvider();
	originalBytes = ASCIIEncoding.Default.GetBytes(clearText);
	encodedBytes = sha1.ComputeHash(originalBytes);

	return Convert.ToBase64String(encodedBytes);
}

public static string GenerateRandomData(int bytes)
{
	byte[] randomData = new byte[bytes];
	var rng = new RNGCryptoServiceProvider();
	rng.GetBytes(randomData);

	return Convert.ToBase64String(randomData);
}
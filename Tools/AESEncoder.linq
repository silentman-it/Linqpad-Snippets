<Query Kind="Program">
  <Namespace>System.Security.Cryptography</Namespace>
</Query>

void Main()
{
	AES.Decrypt(AES.Encrypt("Assistenza17.1").Dump("Encrypt()")).Dump("Decrypt()");
}

public class AES
{
   static string passPhrase = "QwwWERwpppFw4r@#$R23r44t93j";
   static string saltValue = "345h3j987fh4hj";
   static string hashAlgorithm = "SHA1";
   static int passwordIterations = 2;
   static string initVector = "@1B2c3D4e5F6g7H8";
   static int keySize = 256;

   public static string Encrypt(string plainText,
                                string passPhrase,
                                string saltValue,
                                string hashAlgorithm,
                                int passwordIterations,
                                string initVector,
                                int keySize)
   {
       byte[] initVectorBytes = Encoding.ASCII.GetBytes(initVector);
       byte[] saltValueBytes = Encoding.ASCII.GetBytes(saltValue);
       byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

	   PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, saltValueBytes, hashAlgorithm, passwordIterations);

       byte[] keyBytes = password.GetBytes(keySize / 8);
	   RijndaelManaged symmetricKey = new RijndaelManaged();
       symmetricKey.Mode = CipherMode.CBC;
	   ICryptoTransform encryptor = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes);
       MemoryStream memoryStream = new MemoryStream();
       CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
       cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
       cryptoStream.FlushFinalBlock();
       byte[] cipherTextBytes = memoryStream.ToArray();
       memoryStream.Close();
       cryptoStream.Close();

	   string cipherText = Convert.ToBase64String(cipherTextBytes);

       return cipherText;
   }

   public static string Decrypt(string cipherText,
                                string passPhrase,
                                string saltValue,
                                string hashAlgorithm,
                                int passwordIterations,
                                string initVector,
                                int keySize)
   {
       byte[] initVectorBytes = Encoding.ASCII.GetBytes(initVector);
       byte[] saltValueBytes = Encoding.ASCII.GetBytes(saltValue);
       byte[] cipherTextBytes = Convert.FromBase64String(cipherText);
       PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, saltValueBytes, hashAlgorithm, passwordIterations);
       byte[] keyBytes = password.GetBytes(keySize / 8);
       RijndaelManaged symmetricKey = new RijndaelManaged();
       symmetricKey.Mode = CipherMode.CBC;
       ICryptoTransform decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes);
       MemoryStream memoryStream = new MemoryStream(cipherTextBytes);
       CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
       byte[] plainTextBytes = new byte[cipherTextBytes.Length];
       int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
       memoryStream.Close();
       cryptoStream.Close();
       string plainText = Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
       return plainText;
   }


   // Simple methods (use default parameters)
   public static string Encrypt(string plainText)
   {

       if (string.IsNullOrEmpty(plainText))
       {
           throw new ApplicationException("Invalid argument!");
       }

       string cipherText = AES.Encrypt(plainText,
                                       passPhrase,
                                       saltValue,
                                       hashAlgorithm,
                                       passwordIterations,
                                       initVector,
                                       keySize);

       return (cipherText);

   }

   public static string Decrypt(string cipherText)
   {

       if (string.IsNullOrEmpty(cipherText))
       {
           throw new ApplicationException("Invalid argument!");
       }

       string plainText = Decrypt(cipherText,
                                   passPhrase,
                                   saltValue,
                                   hashAlgorithm,
                                   passwordIterations,
                                   initVector,
                                   keySize);

       return (plainText);

   }

}
<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\SMDiagnostics.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Configuration.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.IdentityModel.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Runtime.Serialization.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Security.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.ServiceModel.Internals.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Web.ApplicationServices.dll</Reference>
  <NuGetReference>System.IdentityModel.Tokens.Jwt</NuGetReference>
  <NuGetReference>ZXing.Net</NuGetReference>
  <Namespace>Microsoft.IdentityModel.Tokens</Namespace>
  <Namespace>System.IdentityModel.Tokens.Jwt</Namespace>
  <Namespace>System.Security.Claims</Namespace>
  <Namespace>System.Security.Cryptography</Namespace>
  <Namespace>ZXing</Namespace>
  <Namespace>System.Drawing</Namespace>
</Query>

void Main()
{
	string pub = "<RSAKeyValue><Modulus>nRo2KylZbcjMBPFYxceAU2Ikw8C/WpJqmLjYKyfYrrxMrc7bC1nwDDRGoz5yQEBqH8pCXHrYVDmxmbvdrOLhTXKeKk5YNkST2tgGOBD8NthAgHpiCISUD9X49+P9IktKdB4owARpizi5keCAnnSxIdmn4TKm3JDP2tTX/1ujU3VlBVVCYdU5k/U+4OGG87apPcOrXd1GfTtVnhBftv2VtUo0YhdRWTlcJ2uU/Ck3b+Xge6PezTGPcv4I/Z6/FtAYij7fwc3G2rkfyd6cPfmmoNKoY+XrbXZbjrf69LMwH531eRxDyJrIkRj0E2rsLMIxm+KKb7w/G4j7PcBpHcM3Sw==</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";
	
	var bmp = (Bitmap)Bitmap.FromFile(@"C:\Temp\QRCode.png");
	
	string fmt, text;
	bmp.DecodeBarcode(out fmt, out text);
	
	if(fmt == null || text == null)
	{
		throw new Exception("Cannot decode barcode");
	}
	
	var token = JWTHelper.Instance.Validate(text, new string[] { "Issuer" }, new string[] { "Audience" }, pub);
	
	token.Dump();
	
}


public static class JWTHelperExtensions
{
	public static void DecodeBarcode(this Bitmap bmp, out string decodedFormat, out string decodedText)
	{
		decodedFormat = decodedText = null;
	
		IBarcodeReader reader = new BarcodeReader();
		var barcodeBitmap = new Bitmap(bmp);
		var result = reader.Decode(barcodeBitmap);
		if (result != null)
		{
			decodedFormat = result.BarcodeFormat.ToString();
			decodedText = result.Text;
		}
	}
}


public sealed class JWTHelper
{
   private JWTHelper()
   {
   }

   #region JWTHelper Singleton Class
   private static JWTHelper instance;
   private static object syncRoot = new Object();

   public static JWTHelper Instance
   {
       get
       {
           if (JWTHelper.instance == null)
           {
               lock (syncRoot)
               {
                   if (JWTHelper.instance == null)
                   {
                       JWTHelper newVal = new JWTHelper();

                       // Insure all writes used 
                       // to construct new value have been flushed. 
                       System.Threading.Thread.MemoryBarrier();

                       // publish the new value 
                       JWTHelper.instance = newVal;
                   }
               }
           }

           return JWTHelper.instance;
       }
   }
   #endregion

   public string DefaultPublicKey { get; set; }
   public string DefaultPublicAndPrivateKey { get; set; }
   public string DefaultIssuer { get; set; }
   public string DefaultAudience { get; set; }


   public string Generate(string userName, string roleName, TimeSpan? validity = null, string issuer = null, string audience = null, string publicAndPrivateRsaKey = null, Dictionary<string, string> extraClaims = null)
   {
		RSACryptoServiceProvider publicAndPrivate = new RSACryptoServiceProvider();        
		publicAndPrivate.FromXmlString(publicAndPrivateRsaKey ?? DefaultPublicAndPrivateKey);

		var signingCredentials = new SigningCredentials(new RsaSecurityKey(publicAndPrivate), SecurityAlgorithms.RsaSha256);

		validity = validity ?? TimeSpan.FromHours(1);

		var claimsIdentity = new ClaimsIdentity(new List<Claim>()
		{
			new Claim(ClaimTypes.NameIdentifier, userName),
			new Claim(ClaimTypes.Role, roleName),
		}, "Custom");

		if(extraClaims != null)
		{
			claimsIdentity.AddClaims(extraClaims.Select(x => new Claim(x.Key, x.Value)));
		}

		var securityTokenDescriptor = new SecurityTokenDescriptor()
		{
			Issuer = issuer ?? DefaultIssuer,
			Audience = audience ?? DefaultAudience,
			Subject = claimsIdentity,
			SigningCredentials = signingCredentials,
			Expires = DateTime.UtcNow.Add(validity.Value),
		};
		
		var tokenHandler = new JwtSecurityTokenHandler();
		var plainToken = tokenHandler.CreateToken(securityTokenDescriptor);
		var signedAndEncodedToken = tokenHandler.WriteToken(plainToken);
		
		return signedAndEncodedToken;		

   }

   public JwtSecurityToken Validate(string signedAndEncodedToken, IEnumerable<string> validIssuers = null, IEnumerable<string> validAudiences = null, string publicRsaKey = null)
   {
		var plainTextSecurityKey = publicRsaKey ?? DefaultPublicKey;
		var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(plainTextSecurityKey));
		
		RSACryptoServiceProvider publicOnly = new RSACryptoServiceProvider();
		publicOnly.FromXmlString(plainTextSecurityKey);

       var tokenValidationParameters = new TokenValidationParameters()
       {
           ValidAudiences = validAudiences ?? new string[] { DefaultAudience },
           ValidIssuers = validIssuers ?? new string[] { DefaultIssuer },
           IssuerSigningKey = new RsaSecurityKey(publicOnly),
		   ClockSkew = TimeSpan.Zero
       };

       var tokenHandler = new JwtSecurityTokenHandler();
       SecurityToken validatedToken;
       tokenHandler.ValidateToken(signedAndEncodedToken, tokenValidationParameters, out validatedToken);

       return (JwtSecurityToken)validatedToken;
   }
   
   
	public void GenerateRsaKeys(out string rsaPublicKey, out string rsaPublicAndPrivateKey)
	{
		RSACryptoServiceProvider myRSA = new RSACryptoServiceProvider(2048);
		RSAParameters publicKey = myRSA.ExportParameters(true);
		rsaPublicAndPrivateKey = myRSA.ToXmlString(true);
		rsaPublicKey = myRSA.ToXmlString(false);
	}


}
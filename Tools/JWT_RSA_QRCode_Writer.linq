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
  <Namespace>ZXing.Common</Namespace>
  <Namespace>System.Drawing</Namespace>
  <Namespace>ZXing.QrCode.Internal</Namespace>
  <Namespace>System.Drawing.Imaging</Namespace>
</Query>

void Main()
{
	string priv = "<RSAKeyValue><Modulus>nRo2KylZbcjMBPFYxceAU2Ikw8C/WpJqmLjYKyfYrrxMrc7bC1nwDDRGoz5yQEBqH8pCXHrYVDmxmbvdrOLhTXKeKk5YNkST2tgGOBD8NthAgHpiCISUD9X49+P9IktKdB4owARpizi5keCAnnSxIdmn4TKm3JDP2tTX/1ujU3VlBVVCYdU5k/U+4OGG87apPcOrXd1GfTtVnhBftv2VtUo0YhdRWTlcJ2uU/Ck3b+Xge6PezTGPcv4I/Z6/FtAYij7fwc3G2rkfyd6cPfmmoNKoY+XrbXZbjrf69LMwH531eRxDyJrIkRj0E2rsLMIxm+KKb7w/G4j7PcBpHcM3Sw==</Modulus><Exponent>AQAB</Exponent><P>3LeyGwscrONCz0q3TdwnAcYfllhKJBU6+Lq5HTNEzPGBx4pT6/zbDpBqzkPCMQjBT5Ddooa8cXvWweFX8/BlwhejwloQSQQsQpjsx1Zeo9VM8TPd220hdq6AicbXBB6xn1giTTgpLNgC23iDSaQyCO43MP+91Uk+BYasYQ2Y4ZU=</P><Q>tjc6FhjwFyXVjA5b1a5KIp9BNFjQUnTz6iULGpuDJwqqgr+31zTsEedOaxYs/P3abHAPOv7kZHo8eG+o2miuhAT8ZLyHaXyIlR9IqIfo4DwfpXG6Hfs/T29BApF2xBMc5zhLEctkq3MZEaZY0eK4G/1vVplKgPhBt9uguy9rPV8=</Q><DP>xeuzuqxsOlxQPLIzTY5tLBoNmTPyyAiBqSlHdffTajPmvJg+SQ+lH5pBLFV2faHsNv0gwe0wblMucQ0xYX3gwfuQzWcV02P6jH21VcsNcryDEogAGhPyPgqdGO9FQFyFyu2tVLFXuu65ITUgBZlHhNqYg0fRYsEa7LmnOiCHR0E=</DP><DQ>B3435ka6QMlugabpgdz78XaJEOUCFBH5IczKKxrwBr/6iQvnPHNRrd7MK70qtnCd9c2Z4rwumi8oo288C1c4rfciheX2z5CucStrKnsESYxPZNdLZwY48URK8RcYQjHjA2MeOyoZBq/h/QbRKjhfh88hL/d3x9vtn+jpMClgCJM=</DQ><InverseQ>LmOBgjJ2EXTTHyB0/TI3vPAsGp3lQxVn2O6GCwVWCDlLLEDDppYcBPVDaBE5XYunAKWY8iEzF4kkta3Xr1vlLAS70AqlycEvzF91VNnhyiMyP5SFShCDnerUsi65BO4tXCc68IV5Z7sARw6XLzUmC8DxYmJcdjsK3I8dt/4QDrs=</InverseQ><D>N4optk6S+BXx0pmEOb+S5Wef+bhtrdZvaviGK5OVrgiRzyx/Ed9E1vuwMUwFViOvoPd/SHSBgB05ZVEeSF2ZKyknM2upEd5iqw6N/Eo88CK5gvJBMCO2uHZ74x8twW7+rlhvLZshuP9f02ub7OjyKjZpfFrM1NM5OHLgvb0m23zpu9R/LOlubJ40rjFPwcq68UjbhxgELjerZQ1FWnmDaA9kUAXdxqJC6ZVTgeGkrB1zEhMal7NZcAkO8cWQc3MwLGLdhGHFBiHXXK8DKf2u7CU4hp/FvPhpcZsddZsAB/m6ivYxmFw/oW6Xg+PtbYQY0EEW+9J0sohr3uY/OoE0WQ==</D></RSAKeyValue>";
	
	var token = JWTHelper.Instance.Generate("fede@silentman.it", "USER", TimeSpan.FromMinutes(1), "Issuer", "Audience", priv);
	
	var bmp = token.Dump().ToQRCode().Dump();
	
	bmp.Save(@"C:\Temp\QRCode.png", ImageFormat.Png);
	
}

public static class JWTHelperExtensions
{
	public static Bitmap ToQRCode(this string token)
	{
		var writer = new BarcodeWriter
		{
			Format = BarcodeFormat.QR_CODE,
			Options = new EncodingOptions
			{
				Height = 300,
				Width = 300,
				Margin = 0
			},
		};
		
		writer.Options.Hints[EncodeHintType.ERROR_CORRECTION] = ErrorCorrectionLevel.L;
		
		var bmp = writer.Write(token);
		
		return bmp;
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
           IssuerSigningKey = new RsaSecurityKey(publicOnly)
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
<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\SMDiagnostics.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Configuration.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.IdentityModel.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Runtime.Serialization.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Security.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.ServiceModel.Internals.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Web.ApplicationServices.dll</Reference>
  <NuGetReference>System.IdentityModel.Tokens.Jwt</NuGetReference>
  <Namespace>Microsoft.IdentityModel.Tokens</Namespace>
  <Namespace>System.IdentityModel.Tokens.Jwt</Namespace>
  <Namespace>System.Security.Claims</Namespace>
  <Namespace>System.Security.Cryptography</Namespace>
</Query>

void Main()
{
	string pub, priv;
	
	JWTHelper.Instance.DefaultIssuer = "http://bee.leonardocompany.com/";
	JWTHelper.Instance.DefaultAudience = "http://bidevolution.leonardocompany.com/";
	
	JWTHelper.Instance.GenerateRsaKeys(out pub, out priv);
	
//	JWTHelper.Instance.DefaultPublicAndPrivateKey = priv;
//	JWTHelper.Instance.DefaultPublicKey = pub;
//	var enc = JWTHelper.Instance.Generate("fede@silentman.it", "Admin").Dump();
//	var v = JWTHelper.Instance.Validate(enc).Dump();
	

	// Senza chiavi di default
	var enc = JWTHelper.Instance.Generate("fede@silentman.it", "Admin", TimeSpan.FromMinutes(1), null, null, priv, new Dictionary<string, string> { { "bid.claim1", "test1" } }).Dump();
	var v = JWTHelper.Instance.Validate(enc, null, null, pub).Dump();
	
	//v.Claims.SingleOrDefault(x => x.Type == "role").Value.Dump();
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
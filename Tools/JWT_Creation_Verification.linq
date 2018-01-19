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
</Query>

void Main()
{
	JWTHelper.Instance.DefaultIssuer = "http://bidevolution.leonardocompany.com/";
	JWTHelper.Instance.DefaultAudience = "http://bidevolution.leonardocompany.com/";
	JWTHelper.Instance.DefaultSecurityKey = "UHxNtYMRYwvfpO1dS5pWLKL0M2DgOj40EbN4SoBWgfc";
	
	Dictionary<string, string> c = new Dictionary<string, string>();
	
	c["nameid"] = "5";
	c["unique_name"] = "federico";
	c["role"] = "User";
	c["email"] = "";
	c["given_name"] = "Federico";
	c["family_name"] = "Coppola";
	c["bidevo:culture"] = "it-IT";
	c["bidevo:timezone"] = "";
	
	
	var enc = JWTHelper.Instance.Generate(TimeSpan.FromDays(1000), null, null, null, c).Dump();

	var v = JWTHelper.Instance.Validate(enc).Dump();
	
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

   public string DefaultSecurityKey { get; set; }
   public string DefaultIssuer { get; set; }
   public string DefaultAudience { get; set; }


   public string Generate(TimeSpan? validity = null, string issuer = null, string audience = null, string securityKey = null, Dictionary<string, string> claims = null)
   {
		var plainTextSecurityKey = securityKey ?? DefaultSecurityKey;
		var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(plainTextSecurityKey));
		var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256Signature);
		
		validity = validity ?? TimeSpan.FromHours(1);
		
		var claimsIdentity = new ClaimsIdentity();
		
		if(claims != null)
		{
			claimsIdentity.AddClaims(claims.Select(x => new Claim(x.Key, x.Value)));
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

	public JwtSecurityToken Validate(string signedAndEncodedToken, IEnumerable<string> validIssuers = null, IEnumerable<string> validAudiences = null, string securityKey = null)
	{
		var plainTextSecurityKey = securityKey ?? DefaultSecurityKey;
		var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(plainTextSecurityKey));
		
		var tokenValidationParameters = new TokenValidationParameters()
		{
			ValidAudiences = validAudiences ?? new string[] { DefaultAudience },
			ValidIssuers = validIssuers ?? new string[] { DefaultIssuer },
			IssuerSigningKey = signingKey
		};
		
		var tokenHandler = new JwtSecurityTokenHandler();
		SecurityToken validatedToken;
		tokenHandler.ValidateToken(signedAndEncodedToken, tokenValidationParameters, out validatedToken);
		
		return (JwtSecurityToken)validatedToken;
	}


}
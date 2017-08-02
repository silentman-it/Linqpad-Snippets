<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Web.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Web.Extensions.dll</Reference>
  <Namespace>System.Net</Namespace>
  <Namespace>System.Web</Namespace>
  <Namespace>System.Web.Script.Serialization</Namespace>
  <Namespace>System.Collections.Specialized</Namespace>
  <Namespace>System.Security.Cryptography</Namespace>
</Query>

void Main()
{
//NameValueCollection dict = new NameValueCollection();
//dict["userName"] = "System";
//dict["password"] = Encode("papero11");
//string jsonToken = HttpPost("http://localhost:11229/JsonTmsMainService.svc/auth", dict);
//jsonToken.Dump();

WebRequest wr = WebRequest.Create("http://localhost:11229/JsonTmsMainService.svc/getunits/System");
wr.Headers.Add("X-TokenID", "d2f9d268a44e410a9e1bd3dcc9f2d111");
wr.Method = "GET";
WebResponse resp = wr.GetResponse();

if (resp != null) 
{
	StreamReader sr = new StreamReader (resp.GetResponseStream());
	sr.ReadToEnd ().Trim ().Dump();
}



}

// Define other methods and classes here
string HttpPost (string uri, NameValueCollection dict)
{ 
   // parameters: name1=value1&name2=value2	
   WebRequest webRequest = WebRequest.Create (uri);
   //string ProxyString = 
   //   System.Configuration.ConfigurationManager.AppSettings
   //   [GetConfigKey("proxy")];
   //webRequest.Proxy = new WebProxy (ProxyString, true);
   //Commenting out above required change to App.Config
   webRequest.ContentType = "application/x-www-form-urlencoded";
   webRequest.Method = "POST";
   
   string parameters = BuildQueryString(dict);
   byte[] bytes = Encoding.ASCII.GetBytes (parameters);
   Stream os = null;
   try
   { // send the Post
	  webRequest.ContentLength = bytes.Length;   //Count bytes to send
	  os = webRequest.GetRequestStream();
	  os.Write (bytes, 0, bytes.Length);         //Send it
   }
   catch (WebException ex)
   {
	  return ex.Message;
   }
   finally
   {
	  if (os != null)
	  {
		 os.Close();
	  }
   }
 
   try
   { // get the response
	  WebResponse webResponse = webRequest.GetResponse();
	  if (webResponse == null) 
		 { return null; }
	  StreamReader sr = new StreamReader (webResponse.GetResponseStream());
	  return sr.ReadToEnd ().Trim ();
   }
   catch (WebException ex)
   {
		return ex.Message;
   }

} // end HttpPost

string GetQueryString(NameValueCollection dict)
{
	JavaScriptSerializer oSerializer = new JavaScriptSerializer();
	return oSerializer.Serialize(dict);
}

public string BuildQueryString(NameValueCollection values)
{
StringBuilder sb = new StringBuilder();
for (var i =0; i < values.Count; ++ i)
{
sb.Append(i == 0 ? "?" : "&");
sb.Append(values.Keys[i]);
sb.Append("=");
sb.Append(HttpUtility.UrlEncode(values[i]));
}
return sb.ToString();
}

string Encode(string clearText)
{
	Byte[] originalBytes;
	Byte[] encodedBytes;
	MD5 md5;

	md5 = new MD5CryptoServiceProvider();
	originalBytes = ASCIIEncoding.Default.GetBytes(clearText);
	encodedBytes = md5.ComputeHash(originalBytes);

	return Convert.ToBase64String(encodedBytes);
}

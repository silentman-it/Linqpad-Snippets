<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Net.Http.dll</Reference>
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>Newtonsoft.Json</Namespace>
  <Namespace>Newtonsoft.Json.Linq</Namespace>
  <Namespace>System.Net</Namespace>
  <Namespace>System.Net.Http</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

void Main()
{
	var s = JsonConvert.SerializeObject(new
	{
		data = new { }
	});
	
	s.Dump();

	var body = new StringContent(s);
	var res = DoHttpPost("http://smsgateway.me/api/v3/messages/send?email=fede@silentman.it&password=bd829zp", body);

	res.Dump();
}

public string DoHttpPost(string url, HttpContent body = null)
{
  int timeout = 30000;
  using (HttpClient client = new HttpClient())
  {
      var tPost = client.PostAsync(url, body);
      HttpResponseMessage response = WaitForResult(tPost, timeout); ;

      using (HttpContent content = response.Content)
      {
          var tResult = content.ReadAsStringAsync();
          string result = WaitForResult(tResult, timeout);
          return result;
      }
  }
  
}

private static T WaitForResult<T>(Task<T> task, int timeout)
{
  task.Wait(timeout);
  if (!task.IsCompleted)
  {
      throw new TimeoutException("HTTP POST to Sms Gateway timed out");
  }
  return task.Result;
}

public async Task<string> DoHttpGetAsync(string url)
{
  using (HttpClient client = new HttpClient())
  using (HttpResponseMessage response = await client.GetAsync(url))
  using (HttpContent content = response.Content)
  {
      string result = await content.ReadAsStringAsync();
      return result;
  }
}

		

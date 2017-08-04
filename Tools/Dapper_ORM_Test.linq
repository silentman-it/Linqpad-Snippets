<Query Kind="Program">
  <Output>DataGrids</Output>
  <NuGetReference>Dapper</NuGetReference>
  <NuGetReference>Dapper.Contrib</NuGetReference>
  <Namespace>Dapper</Namespace>
  <Namespace>Dapper.Contrib.Extensions</Namespace>
</Query>

void Main()
{
	using(var conn = new SqlConnection(@"Data Source=172.19.122.63\,3433;Initial Catalog=EDG_PCE_DEV_COPPOLA;Persist Security Info=True;User ID=sa;Password=SelexElsag2012;MultipleActiveResultSets=True"))
	{
		conn.Query<Message>("select * from Messages").Where(x => x.FileName.Contains("Units")).Dump();
	}
}

// Define other methods and classes here
class Message
{
	public int MessageId;
	public string Status;
	public string FileName;
	
}

<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.ComponentModel.DataAnnotations.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Configuration.dll</Reference>
  <NuGetReference>Dapper</NuGetReference>
  <NuGetReference>Dapper.Contrib</NuGetReference>
  <NuGetReference>Oracle.ManagedDataAccess</NuGetReference>
  <Namespace>Dapper</Namespace>
  <Namespace>Oracle.ManagedDataAccess.Client</Namespace>
  <Namespace>System.ComponentModel.DataAnnotations</Namespace>
  <Namespace>System.ComponentModel.DataAnnotations.Schema</Namespace>
</Query>

void Main()
{
	var conn = new OracleConnection("User Id=ed_warehouse;Password=ed_warehouse;Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=win2008r2test2)(PORT=1521))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=ETR11DM1)))");
	
	//conn.Query<SerieDetail>("select * from series_details where rownum < 100").Dump();
	
	var sql = @"
		select * from series_details sd
		inner join series s on sd.serie_id = s.serie_id
		";
	conn.Query<SerieDetail, Serie, SerieDetail>(sql, (sd, s) => {
		sd.Serie = s;
		return sd;
	}, splitOn: "serie_id").AsQueryable().Take(10).Dump();
	
	
}

// Define other methods and classes here
[Table("SERIES")]
public class Serie
{
	public long SERIE_ID { get; set; }
	public string SOURCE_CD { get; set; }
	public string SYMBOL_CD { get; set; }
	public string CLASS_CD { get; set; }
	public string CHANNEL_CD { get; set; }
	public string CONTEXT_CD { get; set; }
	public string PRIMARY_KEY { get; set; }
	public string SECONDARY_KEY { get; set; }
	public string FREQUENCY_CD { get; set; }
	public string UOM_EXPRESSION { get; set; }
	
	public IEnumerable<SerieDetail> SerieDetails { get; set; }
}

[Table("SERIES_DETAILS")]
public class SerieDetail
{
	public long SERIE_ID { get; set; }
	public Serie SerieItem { get; set; }
	public DateTime VALIDITY_START_DATE { get; set; }
	public DateTime VALIDITY_END_DATE { get; set; }
	public int VALIDITY_START_DATE_OFFSET { get; set; }
	public int VALIDITY_END_DATE_OFFSET { get; set; }
	public DateTime TRANSACTION_START_TS { get; set; }
	public double VALUE { get; set; }
	public string LABEL { get; set; }
	public string GENERATION_CD { get; set; }
	
	public Serie Serie { get; set; }
}
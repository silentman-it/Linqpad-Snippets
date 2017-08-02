<Query Kind="Statements">
  <NuGetReference>Oracle.ManagedDataAccess</NuGetReference>
  <Namespace>Oracle.ManagedDataAccess.Client</Namespace>
</Query>

var conn = new OracleConnection("User Id=ed_tms;Password=ed_tms;Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=172.19.122.235)(PORT=1521))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=SVIL4)))");
var cmd = new OracleCommand("select count(1) from users", conn);
conn.Open();
cmd.ExecuteScalar().Dump();
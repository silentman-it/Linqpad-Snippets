<Query Kind="Statements">
  <Connection>
    <ID>66787ff1-b452-4d4c-ad74-cafaae6e0aaf</ID>
    <Persist>true</Persist>
    <Driver>EntityFrameworkDbContext</Driver>
    <CustomAssemblyPath>C:\Work\TMS\DEV\TMS\Common\TMS.Core\bin\x64\Debug\TMS.Core.dll</CustomAssemblyPath>
    <CustomTypeName>TMS.Core.Data.EntityFramework.ED_TMS_DbContext</CustomTypeName>
    <AppConfigPath>C:\Work\TMS\DEV\TMS\Web\TMS.WS\Web.config</AppConfigPath>
  </Connection>
  <Namespace>System.Data.Entity.Migrations</Namespace>
  <Namespace>System.Data.Entity.Migrations.Model</Namespace>
  <Namespace>System.Data.Entity.Migrations.Infrastructure</Namespace>
</Query>

TMS.Core.Data.EntityFramework.Migrations.Configuration cfg = new TMS.Core.Data.EntityFramework.Migrations.Configuration();

cfg.TargetDatabase = new DbConnectionInfo(this.Database.Connection.ConnectionString.Dump("Connection String"), "Oracle.ManagedDataAccess.Client");

DbMigrator mig = new DbMigrator(cfg);

mig.GetPendingMigrations().Dump("Pending");
mig.GetDatabaseMigrations().Dump("DB Version");
mig.GetLocalMigrations().Dump("Local Migrations");

//mig.Update();

//var pendingMigrations = mig.GetLocalMigrations().ToList();
//pendingMigrations.Insert(0, "0");
//var scriptMigrator = new MigratorScriptingDecorator(mig); // <-- now only one MigratorScriptingDecorator is created for the DbMigrator
//foreach (var migration in pendingMigrations.Zip(pendingMigrations.Skip(1), Tuple.Create))
//{
//    var sql = scriptMigrator.ScriptUpdate(migration.Item1, migration.Item2);
//    sql.Dump(string.Format("Migration from {0} to {1}", (migration.Item1 ?? "<null> "), (migration.Item2 ?? "<null> ")));
//}
//
//

//TMS.Core.Data.EntityFramework.Migrations.Initial init = new TMS.Core.Data.EntityFramework.Migrations.Initial();
//init.Down();
//
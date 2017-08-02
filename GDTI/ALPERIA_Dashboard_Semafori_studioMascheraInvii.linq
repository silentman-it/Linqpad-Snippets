<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.configuration.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Security.dll</Reference>
  <Reference>C:\Work\TMS\DEV\TMS\Common\TMS.Core\bin\x64\Debug\TMS.Common.dll</Reference>
  <Reference>C:\Work\TMS\DEV\TMS\Common\TMS.Core\bin\x64\Debug\TMS.Core.dll</Reference>
  <Namespace>System.Configuration</Namespace>
  <Namespace>TMS.Common.Extensions</Namespace>
  <Namespace>TMS.Core.Data.Simple</Namespace>
  <Namespace>TMS.Common.Utils</Namespace>
</Query>

void Main()
{
	//AppDomain.CurrentDomain.SetupInformation.ConfigurationFile.Dump();
	SetConnString("gdti_alperia_prod");
	
	var flowDate = DateTime.Parse("09/03/2017");
	
	var allDates = DateRange(flowDate.GetStartOfMonth(), flowDate.GetEndOfMonth());
	
	var sd =
		       SerieData.Instance.GetSerieData("ExcelSender", null, "NSCW_VDTPublished",   "Signal",  null, flowDate.GetStartOfMonth(), flowDate.GetEndOfMonth())
		.Union(SerieData.Instance.GetSerieData("ExcelSender", null, "NSCW_MIBPublished",   "Signal",  null, flowDate.GetStartOfMonth(), flowDate.GetEndOfMonth()))
		.Union(SerieData.Instance.GetSerieData("ExcelSender", null, "NSCW_MIRPPublished",  "Signal",  null, flowDate.GetStartOfMonth(), flowDate.GetEndOfMonth()))
	;
	
	sd.SelectMany(x => x.Data.Select(d => new {
		Unit = x.Serie.Symbol,
		Type = x.Serie.Class,
		FlowDate = d.Key,
		Value = d.Value.ToString()
	}))
	.GroupBy(x => new { x.Type, x.Unit })
	.Select(x => new {
		Type = x.Key.Type,
		Unit = x.Key.Unit,
		Data = allDates.LeftOuterJoin(x, o => o, i => i.FlowDate, (o,i) => i != null ? i : new {
			Unit = x.Key.Unit,
			Type = x.Key.Type,
			FlowDate = o,
			Value = string.Empty
		})
	})
	.SelectMany(x => x.Data)
	.GroupBy(x => x.Type)
	.Select(x => new {
		Type = x.Key,
		Table = x.ToPivotTable(r => r.Unit, c => c.FlowDate.ToString("dd/MM"), c => c.Any() ? c.FirstOrDefault().Value : string.Empty)
	})
	.Dump();
}

public IEnumerable<DateTime> DateRange(DateTime fromDate, DateTime toDate)
{
    return Enumerable.Range(0, toDate.Subtract(fromDate).Days + 1)
                     .Select(d => fromDate.AddDays(d));
}


void SetConnString(string db)
{
	// Trick per modificare la lista delle connstring
	// http://stackoverflow.com/questions/360024/how-do-i-set-a-connection-string-config-programatically-in-net
	typeof(ConfigurationElementCollection).GetField("bReadOnly", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(ConfigurationManager.ConnectionStrings, false);

	ConfigurationManager.ConnectionStrings.Add(new ConnectionStringSettings("Eu",  string.Format("data source={0};User ID=ed_framework;Password=ed_framework", db)));
	ConfigurationManager.ConnectionStrings.Add(new ConnectionStringSettings("TMS", string.Format("data source={0};User ID=ed_tms;Password=ed_tms", db)));

}


<Query Kind="Statements">
  <Connection>
    <ID>e8a404b2-70c3-4d9e-a2fe-04495d33ae00</ID>
    <Persist>true</Persist>
    <Server>meallinone1</Server>
    <SqlSecurity>true</SqlSecurity>
    <UserName>sa</UserName>
    <Password>AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAAhYrLhpjbjUKVEUXKx5QXqwAAAAACAAAAAAAQZgAAAAEAACAAAADr22UaK12Ns14Cjuhd5qJWGSeibWyblmhfOdfmaYE4OQAAAAAOgAAAAAIAACAAAAAxVPGiStuhe2q0FWtaMIjhpER2LT03UQ8NNiEGZOS1txAAAAB2OUc74e43fl0KB1ezMJnvQAAAAIqiFaahbfCdMRFXBSVDN/0vql05+HtjJrhwgoX/dk1R8YuNfIc3g6cm0x9FgnX/hvtDNES3UcNrE3xUS4GY9ME=</Password>
    <Database>EDG_NRG_ENERGY_TEST</Database>
    <ShowServer>true</ShowServer>
  </Connection>
  <Reference>C:\Work\TMS\DEV\TMS\Common\TMS.Common\bin\x64\Debug\TMS.Common.dll</Reference>
  <Namespace>TMS.Common.Extensions</Namespace>
</Query>

Offers
	.Where(x => x.FlowDate.Year == 2017)
	.GroupBy(x => new { x.OperatorId, x.FlowDate.Month, x.MarketId })
	.Select(x => new {
		Operator = x.Key.OperatorId,
		Month = x.Key.Month,
		Market = x.Key.MarketId,
		Offers = x.Count(),
		UnitsUsed = x.Select(u => u.UnitId).Distinct().Count()
	})
	.OrderBy(x => x.Operator).ThenBy(x => x.Month)
	.ToList()
	.ToPivotTable(r => r.Operator, c => c.Market, rc => rc)
	.Dump();



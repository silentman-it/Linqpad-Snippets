<Query Kind="Statements">
  <Connection>
    <ID>14934e7a-464b-4eb2-a08f-55c5da784ff7</ID>
    <Persist>true</Persist>
    <Server>bidevolution.database.windows.net</Server>
    <SqlSecurity>true</SqlSecurity>
    <UserName>bevoadmin</UserName>
    <Password>AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAAOxumCbBDN0q8h85F2b5bfgAAAAACAAAAAAAQZgAAAAEAACAAAABiEJQXIOxd8tUZY9BRgQFBDN30LMGzN0sR34MgtZOEiQAAAAAOgAAAAAIAACAAAAAT/TssX4BPyHZrgugYmtxmTWrKy3dTCh/Hvew/kol82BAAAAASuQuRLIE6DLs5HUitcUFVQAAAANglvLXGW4HC+BMgSh05N/2eimPurj7fXhZl4zI2JG9pH2B8NCiIlgNQc839zzAN1L5Lyl/RyqsCNwwbsL1qogI=</Password>
    <DbVersion>Azure</DbVersion>
    <Database>BIDEVO_DEV</Database>
    <ShowServer>true</ShowServer>
  </Connection>
</Query>

//var units = OperatorUnits
//	.Where(x => x.OperatorId == 404)
//	.Join(Units, o => o.UnitId, i => i.Id, (o,i) => i);
//	
//string[] markets = { "PCE", "MGP", "MI1", "MI2", "MI3", "MI4", "MI5", "MI6", "MI7" };
//
//var counterItem = Counters.SingleOrDefault(x => x.CounterId == "UnitRelateId");
//var c = (int)counterItem.CurrentValue;
//	
//foreach(var u in units)
//{
//	foreach(var m in markets)
//	{
//		UnitRelates.InsertOnSubmit(new UnitRelates()
//		{
//			Id = c++,
//			UnitId = u.Id,
//			Type = "MarketEnabling",
//			StringValue = m,
//			ValidFrom = DateTimeOffset.MinValue			
//		});
//		
//		Console.Write (".");
//	}
//}
//
//counterItem.CurrentValue = c;
//
//SubmitChanges();

UnitRelates.Dump();
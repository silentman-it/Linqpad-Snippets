<Query Kind="Statements">
  <Reference>C:\Work\TMS_PRJ\DEV\TMS\Common\Energy.RealTime.Core\bin\Debug\Energy.RealTime.Core.dll</Reference>
  <Reference>C:\Work\TMS_PRJ\DEV\TMS\Common\Energy.RealTime.Strategy\bin\Debug\Energy.RealTime.Strategy.dll</Reference>
  <Namespace>Energy.RealTime.Strategy.Utils</Namespace>
  <Namespace>Energy.RealTime.Core.Types.Plans</Namespace>
  <Namespace>Energy.RealTime.Core.Types</Namespace>
</Query>

//string[] values = "A,B,C,D,E,F".Split(',');
//
//Permutations<string> p = new Permutations<string>(values);
//Combinations<string> c = new Combinations<string>(values, 3);
//Variations<string> v = new Variations<string>(values, 3);
//
//p.Select(x => string.Join(", ", x) ).Dump();
//c.Select(x => string.Join(", ", x) ).Dump();
//v.Select(x => string.Join(", ", x) ).Dump();
//

int i = 1;
PVM pvm = new PVM();
for (int y = 0; y < 100; y++) pvm.Plan.Add(new PVPoint(TimeSpan.FromMinutes(i), 400 + (i++)));

i = 0;
PVtc pvtc = new PVtc();
for (int y = 0; y < 100; y++) pvtc.Plan.Add(new PVPoint(TimeSpan.FromMinutes(i), 300 + (i++)));

i = 0;
PVMC pvmc = new PVMC();
for (int y = 0; y < 100; y++) pvmc.Plan.Add(new PVPoint(TimeSpan.FromMinutes(i), 200 + (i++)));

var tmpJoin1 = pvm.Plan.Join(pvtc.Plan, m => m.TS, tc => tc.TS, (m, tc) => new { TS = m.TS, PVM = m.Val, PVTC = tc.Val });
var joinedPlans = tmpJoin1.Join(pvmc.Plan, t => t.TS, mc => mc.TS, (t, mc) => new { TS = t.TS, PVM = t.PVM, PVTC = t.PVTC, PVMC = mc.Val });

//joinedPlans.Dump();

int q = 1;
			
				// Subset di 15 minuti
				TimeSpan tsStart = TimeSpan.FromMinutes(q * 15);
				TimeSpan tsEnd = tsStart.Add(TimeSpan.FromMinutes(15));

				var subset = joinedPlans.Where(a => a.TS >= tsStart && a.TS < tsEnd);

				// Calcolo quantita' OBRS
				decimal obPos = 
					subset                    
					.Select(a => a.PVM > a.PVTC ? a.PVM - a.PVTC : 0)
					.Sum()
					/4;

				decimal obNeg =
					subset
					.Select(a => a.PVM < a.PVTC ? a.PVTC - a.PVM : 0)
					.Sum()
					/4;

				decimal rsPos =
					subset
					.Select(a => a.PVMC > a.PVM ? a.PVMC - a.PVM : 0)
					.Sum()
					/ 4;

				decimal rsNeg =
					subset
					.Select(a => a.PVMC < a.PVM ? a.PVM - a.PVMC : 0)
					.Sum()
					/ 4;

				decimal rsNetPos =
					subset
					.Select(a => a.PVM <= a.PVMC && a.PVM <= a.PVTC ? Math.Min(a.PVMC, a.PVTC) - a.PVM : 0)
					.Sum()
					/ 4;

				decimal rsNetNeg =
					subset
					.Select(a => a.PVM >= a.PVMC && a.PVM >= a.PVTC ? a.PVM - Math.Max(a.PVMC, a.PVTC) : 0)
					.Sum()
					/ 4;

obPos.Dump();
obNeg.Dump();
rsPos.Dump();
rsNeg.Dump();
rsNetPos.Dump();
rsNetNeg.Dump();
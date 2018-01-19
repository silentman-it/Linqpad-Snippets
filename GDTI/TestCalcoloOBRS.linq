<Query Kind="Statements">
  <Output>DataGrids</Output>
  <Reference>C:\Work\TMS\DEV\TMS\Common\Energy.RealTime.Core\bin\x64\Debug\Energy.RealTime.Core.dll</Reference>
  <Reference>C:\Work\TMS\DEV\TMS\Common\TMS.Common\bin\x64\Debug\TMS.Common.dll</Reference>
  <Namespace>Energy.RealTime.Core</Namespace>
  <Namespace>Energy.RealTime.Core.Adapters</Namespace>
  <Namespace>Energy.RealTime.Core.Types</Namespace>
  <Namespace>Energy.RealTime.Core.Types.Messages</Namespace>
  <Namespace>Energy.RealTime.Core.Types.Plans</Namespace>
  <Namespace>Energy.RealTime.Core.Utils</Namespace>
  <Namespace>TMS.Common.Utils.Serialization</Namespace>
</Query>

string fileNamePVTC = @"C:\Users\Federico\Desktop\GDTI\DEBUG_PVtc_UP_TORINONORD_1_Calculation_20171104.bin";
string fileNamePVMC = @"C:\Users\Federico\Desktop\GDTI\DEBUG_PVMC_UP_TORINONORD_1_Calculation_20171104.bin";
PVtc pvtc = BinarySerialization.Deserialize<PVtc>(File.ReadAllBytes(fileNamePVTC));
PVMC pvmc = BinarySerialization.Deserialize<PVMC>(File.ReadAllBytes(fileNamePVMC));

Console.Write ("Calculating PVTC values... ");
pvtc.Debug = false;
pvtc.Calculate();
Console.WriteLine ("Done!");

Console.Write ("Calculating PVMC values... ");
pvmc.Calculate();
Console.WriteLine ("Done!");

OBRS obrs = new OBRS(pvtc, pvmc);
obrs.Debug = false;

Console.Write ("Calculating OBRS values... ");
obrs.Compute();
Console.WriteLine ("Done!");

//////////////////////////////////////////////
// Risultato
//////////////////////////////////////////////

var plans = pvmc.OriginalPlan
.Join(pvmc.PVM,    o => o.TS, i => i.TS, (o,i) => new { TS = o.TS, PV = o.Val, PVM = i.Val })
.Join(pvtc.Plan,   o => o.TS, i => i.TS, (o,i) => new { TS = o.TS, PV = o.PV, PVM = o.PVM, PVTC = i.Val })
.Join(pvmc.Plan,   o => o.TS, i => i.TS, (o,i) => new { TS = o.TS, PV = o.PV, PVM = o.PVM, PVTC = o.PVTC, PVMC = i.Val })
.Dump("Piani")
.Join(obrs.OBPos,  o => o.TS, i => i.TS, (o,i) => new { TS = o.TS, PV = o.PV, PVM = o.PVM, PVTC = o.PVTC, PVMC = o.PVMC, OBPos = i.Val })
.Join(obrs.OBNeg,  o => o.TS, i => i.TS, (o,i) => new { TS = o.TS, PV = o.PV, PVM = o.PVM, PVTC = o.PVTC, PVMC = o.PVMC, OBPos = o.OBPos, OBNeg = i.Val })
.Join(obrs.RSPos,  o => o.TS, i => i.TS, (o,i) => new { TS = o.TS, PV = o.PV, PVM = o.PVM, PVTC = o.PVTC, PVMC = o.PVMC, OBPos = o.OBPos, OBNeg = o.OBNeg, RSPos = i.Val })
.Join(obrs.RSNeg,  o => o.TS, i => i.TS, (o,i) => new { TS = o.TS, PV = o.PV, PVM = o.PVM, PVTC = o.PVTC, PVMC = o.PVMC, OBPos = o.OBPos, OBNeg = o.OBNeg, RSPos = o.RSPos, RSNeg = i.Val })
.Join(obrs.RSNPos, o => o.TS, i => i.TS, (o,i) => new { TS = o.TS, PV = o.PV, PVM = o.PVM, PVTC = o.PVTC, PVMC = o.PVMC, OBPos = o.OBPos, OBNeg = o.OBNeg, RSPos = o.RSPos, RSNeg = o.RSNeg, RSNPos = i.Val })
.Join(obrs.RSNNeg, o => o.TS, i => i.TS, (o,i) => new { TS = o.TS, PV = o.PV, PVM = o.PVM, PVTC = o.PVTC, PVMC = o.PVMC, OBPos = o.OBPos, OBNeg = o.OBNeg, RSPos = o.RSPos, RSNeg = o.RSNeg, RSNPos = o.RSNPos, RSNNeg = i.Val })
.Dump("Risultato");
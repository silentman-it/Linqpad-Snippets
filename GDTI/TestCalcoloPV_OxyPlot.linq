<Query Kind="Program">
  <Output>DataGrids</Output>
  <Reference>C:\Work\TMS\DEV\TMS\Common\Energy.RealTime.Core\bin\x64\Debug\Energy.RealTime.Core.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\WPF\PresentationCore.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\WPF\PresentationFramework.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Xaml.dll</Reference>
  <Reference>C:\Work\TMS\DEV\TMS\Common\TMS.Common\bin\x64\Debug\TMS.Common.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\WPF\WindowsBase.dll</Reference>
  <NuGetReference Prerelease="true">OxyPlot.Wpf</NuGetReference>
  <Namespace>Energy.RealTime.Core</Namespace>
  <Namespace>Energy.RealTime.Core.Adapters</Namespace>
  <Namespace>Energy.RealTime.Core.Types</Namespace>
  <Namespace>Energy.RealTime.Core.Types.Messages</Namespace>
  <Namespace>Energy.RealTime.Core.Types.Plans</Namespace>
  <Namespace>Energy.RealTime.Core.Utils</Namespace>
  <Namespace>OxyPlot</Namespace>
  <Namespace>OxyPlot.Wpf</Namespace>
  <Namespace>System.Windows</Namespace>
  <Namespace>System.Windows.Controls</Namespace>
  <Namespace>System.Windows.Input</Namespace>
  <Namespace>TMS.Common.Utils.Serialization</Namespace>
  <Namespace>System.Globalization</Namespace>
</Query>

void Main()
{
	string fileName = @"C:\Users\Federico\Desktop\DEBUG_PVMC_UP_TURBIGO_4_Calculation_20171222.bin";
	string measureFile = @"";
	
	PVMC p = BinarySerialization.Deserialize<PVMC>(File.ReadAllBytes(fileName));
	//PVM p = BinarySerialization.Deserialize<PVM>(File.ReadAllBytes(fileName));
	//PVtc p = BinarySerialization.Deserialize<PVtc>(File.ReadAllBytes(fileName));
	
	p.Unit.Dump("Unit");
	p.FlowDate.Dump("FlowDate");
	
	///////////////////////
	p.Debug = false;
	///////////////////////
	

	//p.CommandsToIgnore.Add("0004040484");
	
	//p.CommandsToDebug.Add("0004317621");
	//p.StopAt("17:30:00");
	
	//p.Commands.Clear();

	//////////////////////////////////////////////
	// Dump Messaggi
	//////////////////////////////////////////////
	p.Commands
	.Where(x => x is BalanceOrderMessage)
	.Cast<BalanceOrderMessage>()
	.Select(x => new
	{
		x.FileName,
		x.Unit,
		Start = x.StartDate.Value,
		End = x.EndDate.Value,
		x.ContinuationType,
		x.PVPowerDelta_TINI,
		x.PVPowerDelta_TFIN,
		LinkOrder = x.IsLinkOrder
	})
	.Dump("Ordini Bilanciamento");
	
	//////////////////////////////////////////////
	// Event handlers
	//////////////////////////////////////////////
	//
	p.BeforeExecutingCommand += (o,e) => { Console.WriteLine ("BDE Start: ID={0} From={1} To={2}", e.Message.ID, e.Message.StartDate, e.Message.EndDate); };
	p.AfterExecutingCommand += (o,e) => { Console.WriteLine ("BDE End: ID={0} Status={1}", e.Message.ID, e.Message.Status); };
	p.SetupChanged += (o,e) => { Console.WriteLine ("SetupChanged: TS={0} From={1} To={2}", e.StartTS, e.From, e.To); };
	p.RampViolation += (o,e) => { Console.WriteLine ("Rampa violata! TS={0}, Requested={1}, Max={2}", e.TS.ToString(), e.RequestedRamp, e.MaxRamp); };
	p.CompletionTimeViolation += (o,e) => { Console.WriteLine ("Violato tempo di esecuzione! MsgId={0}, Expected={1}, Actual={2}", e.Message.ID, e.ExpectedCompletionTime, e.ActualCompletionTime); };
	p.CommandIgnored += (o,e) => { Console.WriteLine ("Messaggio ignorato. MsgId={0}, Reason={1}", e.Message.ID, e.Reason); };
	p.GenericLog += (o, e) => { Console.WriteLine("LOG: ts={0}. \"{1}\"", e.TS, e.Message); };
	//
	Console.WriteLine ("Calculation in progress!");
	p.InitWorkingRUP();
	p.Calculate();
	Console.WriteLine ("Calculation finished!");
	
	//////////////////////////////////////////////
	// Esito Messaggi
	//////////////////////////////////////////////
	//
	p.Commands
	.OrderBy(x => x.ID).Select(x => new { ID = x.ID, Status = x.Status, Type = x.GetType().Name, Detail = x.ToString(), Obj = x }).Dump("Esito BDE");
	//
	
	//////////////////////////////////////////////
	// Risultato
	//////////////////////////////////////////////
	
	p.Plan
	.Join(p.OriginalPlan, x => x.TS, x => x.TS, (x,y) => new { TS = x.TS, PV = y.Val, PVMC = x.Val })
	.Dump("Risultato");
	
	//////////////////////////////////////////////
	// Dump Risultato su file csv
	//////////////////////////////////////////////
	
	File.WriteAllText(fileName + ".DUMP.csv", p.Dump());
	
	p.PreviousWorkingRUP.Dump("Previous Working RUP");
	p.CurrentWorkingRUP.Dump("Current Working RUP");
	p.FollowingWorkingRUP.Dump("Following Working RUP");
	p.Unavailabilities.Dump("Unavailabilities");
	p.CountPlanSetupChanges().Dump("Setup Changes");
	
	p.Dump("Raw Object");
	
	var pm = FillPlotModel(p);
	
	AddMeasuresFromExternalFile(pm, measureFile);

	ExportImages(pm, fileName);
	
	Show(pm);
	

}

void ExportImages(PlotModel pm, string fileName)
{
	using(var stream = new FileStream(fileName + ".DUMP.png", FileMode.OpenOrCreate, FileAccess.Write))
	{
		PngExporter png = new PngExporter();
		png.Width = 1600;
		png.Height = 1200;
		png.Export(pm, stream);
	}
	
}

PlotModel FillPlotModel(PVtc pvtc)
{
	var pm = new PlotModel();
	pm.Title = string.Format("{0} {1}", pvtc.Unit.Name, pvtc.FlowDate.ToShortDateString());
	
	pm = InitPlotModel(pvtc, pm);
	
	// PVMC
	var s_pvtc =
		pm.Series.Where(x => x is OxyPlot.Series.StairStepSeries).Cast<OxyPlot.Series.StairStepSeries>().SingleOrDefault(x => (string)x.Tag == "Calc")
		?? new OxyPlot.Series.StairStepSeries();

	s_pvtc.Tag = "PVTC";
	s_pvtc.Title = "PVTC";
	s_pvtc.Color = OxyColors.Red;
	foreach(var pt in pvtc.Plan)
	{
		var d = pvtc.FlowDate + pt.TS;
		s_pvtc.Points.Add(new DataPoint(OxyPlot.Axes.DateTimeAxis.ToDouble(d), Convert.ToDouble(pt.Val)));
	}
	
	if(s_pvtc.PlotModel == null)
		pm.Series.Add(s_pvtc);

	// BDE
	var msgColl = pvtc.Commands
		.Where(x => (x is BalanceOrderMessage) && !x.HasBeenRevoked(pvtc))
		.Cast<BalanceOrderMessage>();
		
	foreach (var cmd in msgColl)
	{
		var anno = new OxyPlot.Annotations.LineAnnotation();
		anno.Type = OxyPlot.Annotations.LineAnnotationType.Vertical;
		var sd = pvtc.FlowDate + cmd.StartDate.GetTimeSpan(pvtc.FlowDate);
		anno.X = OxyPlot.Axes.DateTimeAxis.ToDouble(sd);
		anno.Color = OxyColors.Green;
		anno.Text = string.Format("{3} {0} {1:0.000} {2}", cmd.ID, cmd.PVPowerDelta_TFIN, cmd.ContinuationType == BalanceOrderType.RampAndStay ? "ST" : "MD", cmd.StartDate.Value.ToString("HH:mm"));
		pm.Annotations.Add(anno);		
	}
	return pm;
	

}

PlotModel FillPlotModel(PVM pvm)
{
	var pm = new PlotModel();
	pm.Title = string.Format("{0} {1}", pvm.Unit.Name, pvm.FlowDate.ToShortDateString());
	
	pm = InitPlotModel(pvm, pm);
	
	// PVMC
	var s_pvm = new OxyPlot.Series.StairStepSeries();

	s_pvm.Tag = "PVM";
	s_pvm.Title = "PVM";
	s_pvm.Color = OxyColors.Blue;
	foreach(var pt in pvm.Plan)
	{
		var d = pvm.FlowDate + pt.TS;
		s_pvm.Points.Add(new DataPoint(OxyPlot.Axes.DateTimeAxis.ToDouble(d), Convert.ToDouble(pt.Val)));
	}
	
	if(s_pvm.PlotModel == null)
		pm.Series.Add(s_pvm);

	// BDE
	var msgColl = pvm.Commands
		.Where(x => (x is BalanceOrderMessage) && !x.HasBeenRevoked(pvm))
		.Cast<BalanceOrderMessage>();
	foreach (var cmd in msgColl)
	{
		var anno = new OxyPlot.Annotations.LineAnnotation();
		anno.Type = OxyPlot.Annotations.LineAnnotationType.Vertical;
		var sd = pvm.FlowDate + cmd.StartDate.GetTimeSpan(pvm.FlowDate);
		anno.X = OxyPlot.Axes.DateTimeAxis.ToDouble(sd);
		anno.Color = OxyColors.Green;
		anno.Text = string.Format("{3} {0} {1:0.000} {2} {4} {5}",
					cmd.ID,
					cmd.PVPowerDelta_TFIN,
					cmd.ContinuationType == BalanceOrderType.RampAndStay ? "ST" : "MD",
					cmd.StartDate.Value.ToString("HH:mm"),
					cmd.IsLinkOrder ? "(Racc)" : "",
					cmd.Status != MessageStatus.Success ?  string.Format("[{0}]", cmd.Status.ToString().ToUpper()) : ""
					).Trim();
		pm.Annotations.Add(anno);		
	}
	return pm;
}

PlotModel FillPlotModel(PVMC pvmc)
{
	var pm = new PlotModel();
	pm.Title = string.Format("{0} {1}", pvmc.Unit.Name, pvmc.FlowDate.ToShortDateString());
	
	pm = InitPlotModel(pvmc, pm);
	
	
	// PVM
	var s_pvm = pm.Series.Where(x => x is OxyPlot.Series.StairStepSeries).Cast<OxyPlot.Series.StairStepSeries>().SingleOrDefault(x => (string)x.Tag == "Calc")
		?? new OxyPlot.Series.StairStepSeries();


	s_pvm.Tag = "PVM";
	s_pvm.Title = "PVM";
	s_pvm.Color = OxyColors.Blue;
	foreach(var pt in pvmc.PVM)
	{
		var d = pvmc.FlowDate + pt.TS;
		s_pvm.Points.Add(new DataPoint(OxyPlot.Axes.DateTimeAxis.ToDouble(d), Convert.ToDouble(pt.Val)));
	}
	
	if(s_pvm.PlotModel == null)
		pm.Series.Add(s_pvm);
	
	// PVMC
	var s_pvmc = new OxyPlot.Series.StairStepSeries();

	s_pvm.Tag = "PVMC";
	s_pvmc.Title = "PVMC";
	s_pvmc.Color = OxyColors.Red;
	foreach(var pt in pvmc.Plan)
	{
		var d = pvmc.FlowDate + pt.TS;
		s_pvmc.Points.Add(new DataPoint(OxyPlot.Axes.DateTimeAxis.ToDouble(d), Convert.ToDouble(pt.Val)));
	}
	
	if(s_pvmc.PlotModel == null)
		pm.Series.Add(s_pvmc);


	// BDE
	var msgColl = pvmc.Commands
		.Where(x => (x is BalanceOrderMessage) && !x.HasBeenRevoked(pvmc))
		.Cast<BalanceOrderMessage>();
	foreach (var cmd in msgColl)
	{
		var anno = new OxyPlot.Annotations.LineAnnotation();
		anno.Type = OxyPlot.Annotations.LineAnnotationType.Vertical;
		var sd = pvmc.FlowDate + cmd.StartDate.GetTimeSpan(pvmc.FlowDate);
		anno.X = OxyPlot.Axes.DateTimeAxis.ToDouble(sd);
		anno.Color = OxyColors.Green;
		anno.Text = string.Format("{3} {0} {1:0.000} {2} {4} {5}",
					cmd.ID,
					cmd.PVPowerDelta_TFIN,
					cmd.ContinuationType == BalanceOrderType.RampAndStay ? "ST" : "MD",
					cmd.StartDate.Value.ToString("HH:mm"),
					cmd.IsLinkOrder ? "(Racc)" : "",
					cmd.Status != MessageStatus.Success ?  string.Format("[{0}]", cmd.Status.ToString().ToUpper()) : ""
					).Trim();
		pm.Annotations.Add(anno);		
	}
	return pm;
}

PlotModel InitPlotModel(PVBase p, PlotModel pm)
{
	InitAxes(p, pm);
	
	AddRUP(p, pm, p.FlowDate.AddDays(-1));
	AddRUP(p, pm);
	AddRUP(p, pm, p.FlowDate.AddDays(+1));
	
	AddPV(p, pm, p.FlowDate.AddDays(-1));
	AddPV(p, pm);
	AddPV(p, pm, p.FlowDate.AddDays(+1));
	
	AddPVM(p, pm, p.FlowDate.AddDays(-1));
	
	return pm;

}

void InitAxes(PVBase p, PlotModel pm)
{
	// Axes
	pm.Axes.Clear();
	pm.Axes.Add(new OxyPlot.Axes.DateTimeAxis()
	{
		Position = OxyPlot.Axes.AxisPosition.Bottom,
		Key = "Time",
		StringFormat = "HH:mm",
		MajorGridlineStyle = LineStyle.Solid,
		MinorGridlineStyle = LineStyle.Dot,
		MajorGridlineColor = OxyColors.LightGray,
		MinorGridlineColor = OxyColors.LightGray,
		Minimum = OxyPlot.Axes.DateTimeAxis.ToDouble(p.FlowDate.AddHours(-2)),
		Maximum = OxyPlot.Axes.DateTimeAxis.ToDouble(p.FlowDate.AddDays(1).AddHours(2))
	});
	
	pm.Axes.Add(new OxyPlot.Axes.LinearAxis()
	{
		Position = OxyPlot.Axes.AxisPosition.Left,
		Key = "Plans",
		Minimum = 0,
		MaximumPadding = 0.1,
		MinimumPadding = 0.1,
		MajorGridlineStyle = LineStyle.Solid,
		MinorGridlineStyle = LineStyle.Dot,
		MajorGridlineColor = OxyColors.LightGray,
		MinorGridlineColor = OxyColors.LightGray
	});

}

void AddPV(PVBase p, PlotModel pm, DateTime? fd = null)
{
	var flowDate = fd ?? p.FlowDate;

	var plan = 
		flowDate == p.FlowDate ? p.OriginalPlan :
		flowDate >  p.FlowDate ? p.OriginalFollowingDayPlan :
		flowDate <  p.FlowDate ? p.OriginalPreviousDayPlan :
		null;
	// PV
	var s_pv =
		pm.Series.Where(x => x is OxyPlot.Series.StairStepSeries).Cast<OxyPlot.Series.StairStepSeries>().SingleOrDefault(x => (string)x.Tag == "PV")
		?? new OxyPlot.Series.StairStepSeries();
		
	s_pv.Tag = "PV";
	s_pv.Title = "PV";
	s_pv.Color = OxyColors.Black;
	foreach(var pt in plan)
	{
		var d = flowDate + pt.TS;
		s_pv.Points.Add(new DataPoint(OxyPlot.Axes.DateTimeAxis.ToDouble(d), Convert.ToDouble(pt.Val)));
	}
	
	if(s_pv.PlotModel == null)
		pm.Series.Add(s_pv);

}

void AddPVM(PVBase p, PlotModel pm, DateTime? fd = null)
{
	Type t = p.GetType();
	var flowDate = fd ?? p.FlowDate;

	var plan = 
		flowDate == p.FlowDate ? p.Plan :
		flowDate >  p.FlowDate ? p.FollowingDayPlan :
		flowDate <  p.FlowDate ? p.PreviousDayPlan :
		null;
		
	string title = string.Format("PVM {0}", 
		flowDate >  p.FlowDate ? "D+1" :
		flowDate <  p.FlowDate ? "D-1" :
		null).Trim();


	// PVM/C
	var s_pv =
		pm.Series.Where(x => x is OxyPlot.Series.StairStepSeries).Cast<OxyPlot.Series.StairStepSeries>().SingleOrDefault(x => (string)x.Tag == "Calc")
		?? new OxyPlot.Series.StairStepSeries();

	s_pv.Tag = "Calc";
	s_pv.Title = title;
	s_pv.Color = OxyColors.Blue;
	foreach(var pt in plan)
	{
		var d = flowDate + pt.TS;
		s_pv.Points.Add(new DataPoint(OxyPlot.Axes.DateTimeAxis.ToDouble(d), Convert.ToDouble(pt.Val)));
	}
	
	if(s_pv.PlotModel == null)
		pm.Series.Add(s_pv);

}


void AddRUP(PVBase p, PlotModel pm, DateTime? fd = null)
{
	var flowDate = fd ?? p.FlowDate;
	
	RUP wrup =
		flowDate == p.FlowDate ? p.CurrentWorkingRUP :
		flowDate >  p.FlowDate ? p.FollowingWorkingRUP :
		flowDate <  p.FlowDate ? p.PreviousWorkingRUP :
		null;
		
	RUP orup =
		flowDate == p.FlowDate ? p.OriginalDynamicRUP :
		flowDate >  p.FlowDate ? p.OriginalFollowingDynamicRUP :
		flowDate <  p.FlowDate ? p.OriginalPreviousDynamicRUP :
		null;
		

	// RUP
	var lsDynRUP = orup.Data.SelectMany(rd => rd.Assetti.AvailableOnly().Select(a => new { rd.TS, a.Id, a.PMin, a.PMax } )).GroupBy(x => x.Id);
	var lsStaticRUP = GetAllDayMinutes(flowDate).SelectMany(rd => p.OriginalStaticRUP.Assetti.AvailableOnly().Select(a => new { TS = rd.TimeOfDay, a.Id, a.PMin, a.PMax })).GroupBy(x => x.Id);
	var lsOriginalRUP = lsDynRUP.Any() ? lsDynRUP : lsStaticRUP;
	var lsWorkingRUP = wrup.Data.SelectMany(rd => rd.Assetti.AvailableOnly().Select(a => new { rd.TS, a.Id, a.PMin, a.PMax } )).GroupBy(x => x.Id);
	var joinedRUP = lsOriginalRUP.Join(lsWorkingRUP, o => o.Key, i => i.Key, (o, i) => new { Original = o, Working = i });
	
	var maxColors = 12;
	var palette = OxyPalettes.Hue(maxColors);
	int n = 0;
	
	var dimLevel = flowDate == p.FlowDate ? 0 : 0.4;
	foreach(var grp in joinedRUP)
	{
		var s_rup = new OxyPlot.Series.AreaSeries();
		s_rup.MarkerFill = OxyColors.Transparent;
		s_rup.Color = OxyColor.Interpolate(palette.Colors[n], OxyColors.Black, dimLevel);
		s_rup.StrokeThickness = 1;
		s_rup.DataFieldY = "PSMAX";
		s_rup.DataFieldY2 = "PSMIN";
		s_rup.Title = grp.Original.Key;
		s_rup.YAxisKey = "Plans";
		s_rup.RenderInLegend = false;
		
		var s_wrup = new OxyPlot.Series.AreaSeries();
		s_wrup.Color = OxyColors.Transparent;
		s_wrup.Color2 = OxyColors.Transparent;
		s_wrup.LineStyle = LineStyle.Dot;
		s_wrup.StrokeThickness = 1;
		s_wrup.DataFieldY = "PSMAX";
		s_wrup.DataFieldY2 = "PSMIN";
		s_wrup.Title = grp.Working.Key + " (Working)";
		s_wrup.YAxisKey = "Plans";
		s_wrup.RenderInLegend = false;
		
		// Original RUP
		foreach (var pt in grp.Original.OrderBy(x => x.TS))
		{
			var d = flowDate + pt.TS;
			s_rup.Points.Add(new DataPoint(OxyPlot.Axes.DateTimeAxis.ToDouble(d), Convert.ToDouble(pt.PMin)));
			s_rup.Points2.Add(new DataPoint(OxyPlot.Axes.DateTimeAxis.ToDouble(d), Convert.ToDouble(pt.PMax)));
			s_rup.Points.Add(new DataPoint(OxyPlot.Axes.DateTimeAxis.ToDouble(d.AddMinutes(1)), Convert.ToDouble(pt.PMin)));
			s_rup.Points2.Add(new DataPoint(OxyPlot.Axes.DateTimeAxis.ToDouble(d.AddMinutes(1)), Convert.ToDouble(pt.PMax)));
		}
		
		// Working RUP
		foreach (var pt in grp.Working.OrderBy(x => x.TS))
		{
			var d = flowDate + pt.TS;
			s_wrup.Points.Add(new DataPoint(OxyPlot.Axes.DateTimeAxis.ToDouble(d), Convert.ToDouble(pt.PMin)));
			s_wrup.Points2.Add(new DataPoint(OxyPlot.Axes.DateTimeAxis.ToDouble(d), Convert.ToDouble(pt.PMax)));
			s_wrup.Points.Add(new DataPoint(OxyPlot.Axes.DateTimeAxis.ToDouble(d.AddMinutes(1)), Convert.ToDouble(pt.PMin)));
			s_wrup.Points2.Add(new DataPoint(OxyPlot.Axes.DateTimeAxis.ToDouble(d.AddMinutes(1)), Convert.ToDouble(pt.PMax)));
		}
		
		pm.Series.Add(s_rup);
		pm.Series.Add(s_wrup);
		
		n = (++n) % maxColors;
	}


}

void Show(PlotModel model, double width = 1000, double height = 800)
{
	var w = new Window() { Title = "OxyPlot.Wpf.PlotView : " + model.Title, Width = width, Height = height };
	var plot = new PlotView();
	var ls = model.Series;
	var cmenu = new ContextMenu();
	
	ls.ToList().ForEach(x => {
		var mi = new MenuItem() { IsCheckable = true, IsChecked = true, Header = x.Title };
		mi.Click += (s,e) =>
		{
			var _mi = (e.Source as MenuItem);
			var header = _mi.Header.ToString();
			var ischecked = _mi.IsChecked;
			model.Series.First(serie => serie.Title == header).IsVisible = ischecked; 
			model.InvalidatePlot(true);
		};
		cmenu.Items.Add(mi);
	});
	
	
	var ctrl = new PlotController();
	ctrl.BindMouseDown(OxyMouseButton.Left, OxyPlot.PlotCommands.PanAt);
	ctrl.BindMouseEnter(OxyPlot.PlotCommands.HoverPointsOnlyTrack);
	ctrl.UnbindMouseDown(OxyMouseButton.Right);
	
	plot.ContextMenu = cmenu;
	plot.Model = model;
	plot.Controller = ctrl;
	w.Content = plot;
	
	//plot.Model.RenderingDecorator = rc => new XkcdRenderingDecorator(rc);
	w.Show();
}

void AddMeasuresFromExternalFile(PlotModel pm, string measureFile)
{
	if(!File.Exists(measureFile)) return;
	
	var lang = CultureInfo.CreateSpecificCulture("en-US");
	
	var measures = XElement
		.Load(measureFile)
		.Element("Body")
		.Dump("Check")
		.Elements()
		.ToDictionary(
			k => XmlConvert.ToDateTime(k.Attribute("StartMinute").Value, XmlDateTimeSerializationMode.Unspecified),
			v => Convert.ToDouble(v.Attribute("Value").Value, lang)
		).Dump("Measures");
		
	var s_measures = new OxyPlot.Series.StairStepSeries();
	s_measures.Title = "Measures";
	s_measures.Color = OxyColor.FromAColor(128, OxyColors.DarkGoldenrod);
	s_measures.StrokeThickness = 3.0;
	foreach(var pt in measures)
	{
		s_measures.Points.Add(new DataPoint(OxyPlot.Axes.DateTimeAxis.ToDouble(pt.Key), pt.Value));
	}
	pm.Series.Add(s_measures);
		
}

public static IEnumerable<DateTime> GetAllDayMinutes(DateTime d)
{
	var baseDate = d.Date;
	var allMinutes = Enumerable.Range(0, 60 * 24).Select(x => baseDate.AddMinutes(x));
	return allMinutes;
}
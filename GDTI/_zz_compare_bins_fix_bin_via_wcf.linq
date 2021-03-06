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
	PVMC p1 = BinarySerialization.Deserialize<PVMC>(File.ReadAllBytes(@"C:\Temp\dump_service.bin"));
	PVMC p2 = BinarySerialization.Deserialize<PVMC>(File.ReadAllBytes(@"C:\Temp\dump_corrotto.bin"));
	
	p1.Dump("Lato Service");
	p2.Dump("Lato Client");
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
	
//	using(var stream = new FileStream(fileName + ".DUMP.svg", FileMode.OpenOrCreate, FileAccess.Write))
//	{
//		OxyPlot.SvgExporter svg = new OxyPlot.SvgExporter();
//		svg.Width = 1600;
//		svg.Height = 1200;
//		svg.Export(pm, stream);
//	}

}

PlotModel FillPlotModel(PVBase pcalc)
{
	var pm = new PlotModel();
	pm.Title = string.Format("{0} {1} {2}", pcalc.GetType().Name, pcalc.Unit.Name, pcalc.FlowDate.ToShortDateString());
	
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
	
	// RUP
	var lsDynRUP = pcalc.OriginalDynamicRUP.Data.SelectMany(rd => rd.Assetti.AvailableOnly().Select(a => new { rd.TS, a.Id, a.PMin, a.PMax } )).GroupBy(x => x.Id);
	var lsStaticRUP = GetAllDayMinutes(pcalc.FlowDate).SelectMany(rd => pcalc.OriginalStaticRUP.Assetti.AvailableOnly().Select(a => new { TS = rd.TimeOfDay, a.Id, a.PMin, a.PMax })).GroupBy(x => x.Id);
	var lsOriginalRUP = lsDynRUP.Any() ? lsDynRUP : lsStaticRUP;
	var lsWorkingRUP = pcalc.CurrentWorkingRUP.Data.SelectMany(rd => rd.Assetti.AvailableOnly().Select(a => new { rd.TS, a.Id, a.PMin, a.PMax } )).GroupBy(x => x.Id);
	var joinedRUP = lsOriginalRUP.Join(lsWorkingRUP, o => o.Key, i => i.Key, (o, i) => new { Original = o, Working = i });
	
	foreach(var grp in joinedRUP)
	{
		var s_rup = new OxyPlot.Series.AreaSeries();
		s_rup.MarkerFill = OxyColors.Transparent;
		s_rup.StrokeThickness = 1;
		s_rup.DataFieldY = "PSMAX";
		s_rup.DataFieldY2 = "PSMIN";
		s_rup.Title = grp.Original.Key;
		s_rup.YAxisKey = "Plans";
		
		var s_wrup = new OxyPlot.Series.AreaSeries();
		s_wrup.Color = OxyColors.Transparent;
		s_wrup.Color2 = OxyColors.Transparent;
		//s_wrup.Fill = OxyColors.Transparent;
		s_wrup.LineStyle = LineStyle.Dot;
		s_wrup.StrokeThickness = 1;
		s_wrup.DataFieldY = "PSMAX";
		s_wrup.DataFieldY2 = "PSMIN";
		s_wrup.Title = grp.Working.Key + " (SB)";
		s_wrup.YAxisKey = "Plans";
		
		// Original RUP
		foreach (var pt in grp.Original.OrderBy(x => x.TS))
		{
			var d = pcalc.FlowDate + pt.TS;
			s_rup.Points.Add(new DataPoint(OxyPlot.Axes.DateTimeAxis.ToDouble(d), Convert.ToDouble(pt.PMin)));
			s_rup.Points2.Add(new DataPoint(OxyPlot.Axes.DateTimeAxis.ToDouble(d), Convert.ToDouble(pt.PMax)));
			s_rup.Points.Add(new DataPoint(OxyPlot.Axes.DateTimeAxis.ToDouble(d.AddMinutes(1)), Convert.ToDouble(pt.PMin)));
			s_rup.Points2.Add(new DataPoint(OxyPlot.Axes.DateTimeAxis.ToDouble(d.AddMinutes(1)), Convert.ToDouble(pt.PMax)));
		}
		
		// Working RUP
		foreach (var pt in grp.Working.OrderBy(x => x.TS))
		{
			var d = pcalc.FlowDate + pt.TS;
			s_wrup.Points.Add(new DataPoint(OxyPlot.Axes.DateTimeAxis.ToDouble(d), Convert.ToDouble(pt.PMin)));
			s_wrup.Points2.Add(new DataPoint(OxyPlot.Axes.DateTimeAxis.ToDouble(d), Convert.ToDouble(pt.PMax)));
			s_wrup.Points.Add(new DataPoint(OxyPlot.Axes.DateTimeAxis.ToDouble(d.AddMinutes(1)), Convert.ToDouble(pt.PMin)));
			s_wrup.Points2.Add(new DataPoint(OxyPlot.Axes.DateTimeAxis.ToDouble(d.AddMinutes(1)), Convert.ToDouble(pt.PMax)));
		}
		
		pm.Series.Add(s_rup);
		pm.Series.Add(s_wrup);
	}

	// PV
	var s_pv = new OxyPlot.Series.StairStepSeries();
	s_pv.Title = "PV";
	s_pv.Color = OxyColors.Black;
	foreach(var pt in pcalc.OriginalPlan)
	{
		var d = pcalc.FlowDate + pt.TS;
		s_pv.Points.Add(new DataPoint(OxyPlot.Axes.DateTimeAxis.ToDouble(d), Convert.ToDouble(pt.Val)));
	}
	pm.Series.Add(s_pv);

	// PVM/PVMC
	var s_pvmc = new OxyPlot.Series.StairStepSeries();
	s_pvmc.Title = pcalc.GetType().Name;
	s_pvmc.Color = OxyColors.Red;
	foreach(var pt in pcalc.Plan)
	{
		var d = pcalc.FlowDate + pt.TS;
		s_pvmc.Points.Add(new DataPoint(OxyPlot.Axes.DateTimeAxis.ToDouble(d), Convert.ToDouble(pt.Val)));
	}
	pm.Series.Add(s_pvmc);


	// BDE
	var msgColl = pcalc.Commands
		.Where(x => (x is BalanceOrderMessage) && !x.HasBeenRevoked(pcalc))
		.Cast<BalanceOrderMessage>();
	foreach (var cmd in msgColl)
	{
		var anno = new OxyPlot.Annotations.LineAnnotation();
		anno.Type = OxyPlot.Annotations.LineAnnotationType.Vertical;
		var sd = pcalc.FlowDate + cmd.StartDate.GetTimeSpan(pcalc.FlowDate);
		anno.X = OxyPlot.Axes.DateTimeAxis.ToDouble(sd);
		anno.Color = OxyColors.Green;
		anno.Text = string.Format("{3} {0} {1:0.000} {2}", cmd.ID, cmd.PVPowerDelta_TFIN, cmd.ContinuationType == BalanceOrderType.RampAndStay ? "ST" : "MD", cmd.StartDate.Value.ToString("HH:mm"));
		pm.Annotations.Add(anno);		
	}
	return pm;
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
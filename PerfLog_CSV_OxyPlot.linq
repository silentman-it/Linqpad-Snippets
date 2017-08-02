<Query Kind="Program">
  <Output>DataGrids</Output>
  <Reference>C:\Work\TMS\DEV\TMS\Common\Energy.RealTime.Core\bin\x64\Debug\Energy.RealTime.Core.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\WPF\PresentationCore.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\WPF\PresentationFramework.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Xaml.dll</Reference>
  <Reference>C:\Work\TMS\DEV\TMS\Common\TMS.Common\bin\x64\Debug\TMS.Common.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\WPF\WindowsBase.dll</Reference>
  <NuGetReference>CsvHelper.Excel</NuGetReference>
  <NuGetReference Prerelease="true">OxyPlot.Wpf</NuGetReference>
  <Namespace>Energy.RealTime.Core</Namespace>
  <Namespace>Energy.RealTime.Core.Adapters</Namespace>
  <Namespace>Energy.RealTime.Core.Types</Namespace>
  <Namespace>Energy.RealTime.Core.Types.Messages</Namespace>
  <Namespace>Energy.RealTime.Core.Types.Plans</Namespace>
  <Namespace>Energy.RealTime.Core.Utils</Namespace>
  <Namespace>OxyPlot</Namespace>
  <Namespace>OxyPlot.Wpf</Namespace>
  <Namespace>System.Globalization</Namespace>
  <Namespace>System.Windows</Namespace>
  <Namespace>System.Windows.Controls</Namespace>
  <Namespace>System.Windows.Input</Namespace>
  <Namespace>TMS.Common.Utils.Serialization</Namespace>
  <Namespace>CsvHelper</Namespace>
  <Namespace>CsvHelper.Configuration</Namespace>
  <Namespace>CsvHelper.TypeConversion</Namespace>
</Query>

void Main()
{
	//string fileName = @"C:\Users\Federico\Desktop\Win2008r2Test1DataCollector01_run20170711.csv";
	string fileName = @"C:\Users\Federico\Desktop\DataCollector01.csv";
	var reader = File.OpenText(fileName);
	CsvReader csv = new CsvReader(reader, new CsvConfiguration() { HasHeaderRecord = true, IgnoreQuotes = false, IgnoreHeaderWhiteSpace = true, TrimFields = true, IgnoreBlankLines = true, IgnoreReadingExceptions = true } );
	
	var dic = new Dictionary<DateTime, long>();
	
	while( csv.Read() )
	{
		try
		{	        
			var key = DateTime.ParseExact(csv.GetField<string>(0), "MM/dd/yyyy HH:mm:ss.fff", CultureInfo.InvariantCulture);
			var privateBytes = csv.GetField<long>(1);
			dic[key] = privateBytes;
		}
		catch (CsvTypeConverterException ex)
		{}
	}
	
	var pm = FillPlotModel(dic);
	
	Show(pm);
}


PlotModel FillPlotModel(Dictionary<DateTime, long> dic)
{
	var pm = new PlotModel();
	
	// Axes
	pm.Axes.Clear();
	pm.Axes.Add(new OxyPlot.Axes.DateTimeAxis()
	{
		Position = OxyPlot.Axes.AxisPosition.Bottom,
		Key = "Time",
		StringFormat = "dd/MM/yy HH:mm",
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

	// PV
	var s_memUsage = new OxyPlot.Series.StairStepSeries();
	s_memUsage.Title = "MemUsage";
	s_memUsage.Color = OxyColors.Red;
	foreach(var i in dic)
	{
		s_memUsage.Points.Add(new DataPoint(OxyPlot.Axes.DateTimeAxis.ToDouble(i.Key), i.Value));
	}
	pm.Series.Add(s_memUsage);
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

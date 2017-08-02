<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\WPF\PresentationCore.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\WPF\PresentationFramework.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Xaml.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\WPF\WindowsBase.dll</Reference>
  <NuGetReference>OxyPlot</NuGetReference>
  <Namespace>OxyPlot</Namespace>
  <Namespace>OxyPlot.Wpf</Namespace>
  <Namespace>System.Windows</Namespace>
  <Namespace>System.Windows.Controls</Namespace>
  <Namespace>System.Windows.Input</Namespace>
</Query>

void Main()
{
	var pm = new PlotModel();
	pm.Title = "Simple Line Plot";
	
	pm.Axes.Clear();
	pm.Axes.Add(new OxyPlot.Axes.LinearAxis() {
		Key = "Left",
		Minimum = 0,
		Position = OxyPlot.Axes.AxisPosition.Left,
	});
	
	pm.Axes.Add(new OxyPlot.Axes.LinearAxis() {
		Key = "Right",
		Minimum = 0,
		Maximum = 50,
		Position = OxyPlot.Axes.AxisPosition.Right,
	});
	

	
	var numSeries = 1;
	var numPoints = 10;
	
	var rnd = new Random(DateTime.Now.Millisecond);
	
	for (int i = 0; i < numSeries; i++)
	{
		var s = new OxyPlot.Series.LineSeries();
		s.Title = "Line series #" + i;
		for (int j = 0; j < numPoints; j++)
		{
			double v = rnd.NextDouble() * 100 + 250;
			s.Points.Add(new DataPoint (j, v));
			s.Color = OxyColor.FromAColor(255, OxyColors.DarkGreen);
			s.LineLegendPosition = OxyPlot.Series.LineLegendPosition.Start;
			s.StrokeThickness = 3;
		}		
		pm.Series.Add(s);
	}

	// Set Right axis
	var lineSerie = (pm.Series.FirstOrDefault() as OxyPlot.Series.DataPointSeries);
	var minY = lineSerie.Points.Min(x => x.Y);
	var maxY = lineSerie.Points.Max(x => x.Y);
	var rightAxis = pm.Axes.SingleOrDefault(x => x.Key == "Right");
	rightAxis.EndPosition = (minY / maxY) - 0.1;

	pm.LegendPlacement = LegendPlacement.Outside;
	
	//pm.RenderingDecorator = rc => new XkcdRenderingDecorator(rc);
	
	var ctrl = new PlotController();
	//ctrl.InputCommandBindings.Clear();
	ctrl.BindMouseDown(OxyMouseButton.Left, OxyPlot.PlotCommands.PanAt);
	ctrl.BindMouseEnter(OxyPlot.PlotCommands.HoverPointsOnlyTrack);
	ctrl.UnbindMouseDown(OxyMouseButton.Right);

	Show(pm, ctrl);

}

// Define other methods and classes here
void Show(PlotModel model, IPlotController controller = null, double width = 800, double height = 500)
{
	var w = new Window() { Title = "OxyPlot.Wpf.PlotView : " + model.Title, Width = width, Height = height };
	var plot = new PlotView();
	var cmenu = new ContextMenu();
	var ls = model.Series;
	
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
	
	plot.ContextMenu = cmenu;
	plot.Model = model;
	plot.Controller = controller;
	w.Content = plot;
	w.Show();
}
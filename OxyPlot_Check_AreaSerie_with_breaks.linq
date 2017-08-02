<Query Kind="Program">
  <Reference>C:\Work\TMS\DEV\GDTI\ThirdParty\OxyPlot.dll</Reference>
  <Reference>C:\Work\TMS\DEV\GDTI\ThirdParty\OxyPlot.Wpf.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\WPF\PresentationCore.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\WPF\PresentationFramework.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Xaml.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\WPF\WindowsBase.dll</Reference>
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
	
	var numSeries = 1;
	var numPoints = 10;
	
	var rnd = new Random(DateTime.Now.Millisecond);
	
	for (int i = 0; i < numSeries; i++)
	{
		var s = new OxyPlot.Series.AreaSeries();
		s.Title = "Line series #" + i;
		for (int j = 0; j < numPoints; j++)
		{
			double v = rnd.NextDouble() * 100;
			double v2 = v + 10;
			
			if(j == 5)
			{
				v = Double.NaN;
				v2 = Double.NaN;
			}
			
			
			s.Points.Add(new DataPoint (j, v));
			s.Points2.Add(new DataPoint (j, v2));

		}		
		pm.Series.Add(s.Dump());
	}

	pm.LegendPlacement = LegendPlacement.Outside;
	
	//pm.RenderingDecorator = rc => new XkcdRenderingDecorator(rc);
	
	var ctrl = new PlotController();

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
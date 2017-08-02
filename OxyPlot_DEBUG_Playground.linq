<Query Kind="Program">
  <Output>DataGrids</Output>
  <Reference>&lt;RuntimeDirectory&gt;\WPF\PresentationCore.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\WPF\PresentationFramework.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Xaml.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\WPF\WindowsBase.dll</Reference>
  <NuGetReference Prerelease="true">OxyPlot.Wpf</NuGetReference>
  <Namespace>OxyPlot</Namespace>
  <Namespace>OxyPlot.Series</Namespace>
  <Namespace>OxyPlot.Wpf</Namespace>
  <Namespace>System.Globalization</Namespace>
  <Namespace>System.Windows</Namespace>
  <Namespace>System.Windows.Controls</Namespace>
  <Namespace>System.Windows.Input</Namespace>
  <Namespace>System.Windows.Media</Namespace>
</Query>

void Main()
{
	var mod = new PlotModel { Title = "Pie Sample1" };

	mod.LegendPlacement = LegendPlacement.Inside;
	mod.LegendPosition = LegendPosition.RightTop;
	mod.IsLegendVisible = true;
	
	//mod.RenderingDecorator = rc => new XkcdRenderingDecorator(rc);
	
	dynamic seriesP1 = new PieSeries
	{
		Stroke = OxyColors.Black,
		StrokeThickness = 2.0,
		TickDistance = 10,
		InnerDiameter = 0.35,
		Diameter = 0.7,
		InsideLabelPosition = 2.5,
		AngleSpan = 360,
		StartAngle = -90,
		//Diameter = 0.8,
		ExplodedDistance = 0.1,
		TrackerFormatString = "{1}: {2} Smc",
		OutsideLabelFormat = "{0:0.###} Smc",
		InsideLabelFormat = null,
	};
	
	var forecast = 22500;
	var actual = 4260.43;
	
	mod.Title = string.Format("Prev: {0}" , forecast);
	
	seriesP1.Slices.Add(new PieSlice("Previsionale Residuo", forecast) { IsExploded = false, Fill = OxyColors.Green });
	seriesP1.Slices.Add(new PieSlice("Consumo", actual) { IsExploded = true, Fill = OxyColors.Blue });
	
	mod.Series.Add(seriesP1);
	
	
	
//	TextAnnotation annotation = new TextAnnotation();
//	annotation.Text = string.Format("{0:C2}", "asdasd");
//	annotation.TextColor = Colors.Red;
//	annotation.Stroke = Colors.Red;
//	annotation.StrokeThickness = 5;
//	annotation.TextPosition = new DataPoint(15, 90);
	
	var anno = new TextAnnotation();
	anno.Text = string.Format("{0:C2}", forecast + actual);
	
	mod.Annotations.Add(anno);
	
	
	Show(mod, 300, 300);

}

// Define other methods and classes here
void Show(PlotModel model, double width = 1000, double height = 800)
{
	var w = new Window() { Title = "OxyPlot.Wpf.PlotView : " + model.Title, Width = width, Height = height };
	var plot = new PlotView();
	var ctrl = new PlotController();
	plot.Model = model;
	plot.Controller = ctrl;
	w.Content = plot;
	w.Show();
}
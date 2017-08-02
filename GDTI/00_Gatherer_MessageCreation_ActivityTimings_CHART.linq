<Query Kind="Program">
  <Connection>
    <ID>5e7e6496-0017-451d-bc97-f509ff34b585</ID>
    <Persist>true</Persist>
    <Driver Assembly="IQDriver" PublicKeyToken="5b59726538a49684">IQDriver.IQDriver</Driver>
    <Provider>Devart.Data.Oracle</Provider>
    <CustomCxString>AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAA2gyzB2XF+E2Ar6/EAdc9fwAAAAACAAAAAAAQZgAAAAEAACAAAAA6jhzKPumX8w20cFDbqERoN/7fSRGrSBi2j6JfQC1YQQAAAAAOgAAAAAIAACAAAACqkTnGmH4A5Agu5mS/90RgeuGWPUlm30gfZI8fHNExPkAAAABIdu7uCQFu02frRBg5tZbt7/XoRJ76i/yYOD2BHlu5RNuoxgQb9Nv3ATQboAcgQd4SKpGdeO+o7D3y9V60JvgZQAAAAEiONrbfqF8mNlCx1WFGAblk/3T7XyBa3Mxl300jeLb65Usr7ncBm08C3R6Pv1qqby4F9SolBJkuoIeLpYBfvRc=</CustomCxString>
    <Server>gdti_tp</Server>
    <EncryptCustomCxString>true</EncryptCustomCxString>
    <NoPluralization>true</NoPluralization>
    <DisplayName>gdti_tp.ed_framework</DisplayName>
    <UserName>ed_framework</UserName>
    <Password>AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAA2gyzB2XF+E2Ar6/EAdc9fwAAAAACAAAAAAAQZgAAAAEAACAAAADq2zC7LHPUpWrbAsap1NH7zTgfBbiaU1CU+wd0cQo3/QAAAAAOgAAAAAIAACAAAADhjUBCvCJZXoFiCDeTBlceuNtsahjnF0GH/5NFznVAixAAAAD9xs4O+z0IqouMTvRwZTWAQAAAAJjlRb4Ryzd/1k+SWDHG5lmzs3AXp57vWgS213/FcaPM4ZKnOK62vFX1ToaOkTsconyzSet3yYMjrrTU4zP74oQ=</Password>
    <DriverData>
      <StripUnderscores>true</StripUnderscores>
      <QuietenAllCaps>true</QuietenAllCaps>
      <ConnectAs>Default</ConnectAs>
      <UseOciMode>true</UseOciMode>
    </DriverData>
  </Connection>
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
	//var dtFrom = DateTime.Parse("14:00:00");
	var dtFrom = DateTime.Today.AddDays(-2);
	var pm = new PlotModel();
	pm.Title = "Gatherer Message/Activities Gantt chart";
	
	////
	var myData = Messages
		.Join(Activities, o => o.Messageid, i => i.Messageid, (o, i) => new {
			MessageId = o.Messageid,
			ActivityId = i.Activityid,
			FileName = o.Filename,
			MessageStatus = o.Status,
			ActivityStatus = i.Status,
			MessageCreationDate = o.Creationdate,
			Activity = i.Name,
			ActivityStart = i.ExecutionStartTs,
			ActivityEnd = i.ExecutionEndTs,
			MessagePriority = o.Priority,
			Runner = o.Runnerid,
			Queue = o.Runnerqueueid
		})
		.Where(x => x.ActivityEnd != null)
		.Where(x => x.ActivityStart >= dtFrom)
		.ToList()
		.OrderByDescending(x => x.MessageCreationDate)
		.Take(18)
		//.Dump()
		;

	////
	
	pm.Axes.Clear();
	
	pm.Axes.Add(new OxyPlot.Axes.DateTimeAxis() {
		Position = OxyPlot.Axes.AxisPosition.Bottom,
		Title = "Time"
	});
	
	pm.Axes.Add(new OxyPlot.Axes.CategoryAxis() {
		Position = OxyPlot.Axes.AxisPosition.Left,
		Title = "Act"
	});

	int n = 0;
	var activityDurationSerie = new OxyPlot.Series.IntervalBarSeries();
	var messageCreationSerie = new OxyPlot.Series.ScatterSeries();
	foreach(var item in myData)
	{
		var barItem = new OxyPlot.Series.IntervalBarItem(
			OxyPlot.Axes.DateTimeAxis.ToDouble(item.ActivityStart.Value),
			OxyPlot.Axes.DateTimeAxis.ToDouble(item.ActivityEnd.Value),
			item.FileName) {  CategoryIndex = n++, Color = item.ActivityStatus == "SUC" ? OxyColors.Green : OxyColors.Red };
		activityDurationSerie.Items.Add(barItem);
		
		var creationItem = new OxyPlot.Series.ScatterPoint(
			OxyPlot.Axes.DateTimeAxis.ToDouble(item.MessageCreationDate),
			barItem.CategoryIndex) { Value = barItem.CategoryIndex, Size = 2.0 };
			
		messageCreationSerie.Points.Add(creationItem);
		
	}

	pm.Series.Add(activityDurationSerie);
	pm.Series.Add(messageCreationSerie);

	pm.LegendPlacement = LegendPlacement.Outside;
	
	//pm.RenderingDecorator = rc => new XkcdRenderingDecorator(rc);
	
	Show(pm);

}

// Define other methods and classes here
void Show(PlotModel model, IPlotController controller = null, double width = 800, double height = 500)
{
	var w = new Window() { Title = "OxyPlot.Wpf.PlotView : " + model.Title, Width = width, Height = height };
	var plot = new PlotView();
	plot.Model = model;
	plot.Controller = controller;
	w.Content = plot;
	w.Show();
}
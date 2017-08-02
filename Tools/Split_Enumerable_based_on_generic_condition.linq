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
	List<DataPoint> p1 = new List<DataPoint>();
	List<DataPoint> p2 = new List<DataPoint>();
	
	p1.Add(new DataPoint(1, Double.NaN));
	p1.Add(new DataPoint(5, 5));
	p1.Add(new DataPoint(6, 5));
	p1.Add(new DataPoint(5, 5));
	p1.Add(new DataPoint(8, Double.NaN));
	p1.Add(new DataPoint(8.5, Double.NaN));
	p1.Add(new DataPoint(9, 5));
	p1.Add(new DataPoint(10, 5));
	p1.Add(new DataPoint(11, 5));
	p1.Add(new DataPoint(12, 5));
	p1.Add(new DataPoint(Double.NaN, Double.NaN));
	p1.Add(new DataPoint(15, 5));
	
	p2.Add(new DataPoint(5, 5));
	p2.Add(new DataPoint(6, 5));
	p2.Add(new DataPoint(5, 5));
	p2.Add(new DataPoint(8, 5));
	p2.Add(new DataPoint(8.5, Double.NaN));
	p2.Add(new DataPoint(9, 5));
	p2.Add(new DataPoint(10, Double.NaN));
	p2.Add(new DataPoint(11, 5));
	p2.Add(new DataPoint(12, 5));
	p2.Add(new DataPoint(Double.NaN, Double.NaN));
	p2.Add(new DataPoint(15, 5));

	p1.Chunk(x => Double.IsNaN(x.Y)).Dump();
}

// Define other methods and classes here
public static class MyExts
{
	public static IEnumerable<IEnumerable<T>> Chunk<T>(this IEnumerable<T> source, Func<T, bool> splitCondition)
	{
		source = source.SkipWhile(x => splitCondition(x));
		while (source.Any())
		{
			yield return source.TakeWhile(x => !splitCondition(x));
			source = source.SkipWhile(x => !splitCondition(x)).SkipWhile(x => splitCondition(x));
		}
	}
}
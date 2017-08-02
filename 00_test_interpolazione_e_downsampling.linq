<Query Kind="Program">
  <Output>DataGrids</Output>
</Query>

void Main()
{
	TimeSpan tsOriginalDelta = TimeSpan.FromMinutes(1);
	int numOfSeries = 1;
	int numOfOutputSamples = 50;
	List<SerieItem> ls = GetInputData(tsOriginalDelta, numOfSeries);
	
	ls.Dump("ORIGINAL");
	
	Stopwatch sw = Stopwatch.StartNew();
	ls = PerformDownSampling(ls, numOfOutputSamples);
	Console.WriteLine("PerformDownSampling() completed in {0} msec.", sw.Elapsed.TotalMilliseconds.ToString());
	
	ls.Dump("DOWNSAMPLED");
}

private static List<SerieItem> PerformDownSampling(List<SerieItem> ls, int nSamples)
{
  TimeSpan tsMin = ls.Min(x => x.ts);
  TimeSpan tsMax = ls.Max(x => x.ts);

  List<SerieItem> lsOut = new List<SerieItem>();

  var grp = ls.GroupBy(k => k.serie);

  foreach (var grpItem in grp)
  {
      lsOut.AddRange(PerformDownSampling(grpItem.ToList(), nSamples, grpItem.Key, tsMin, tsMax));
  }

  return lsOut;

}

private static List<SerieItem> PerformDownSampling(List<SerieItem> ls, int nSamples, string serie, TimeSpan tsMin, TimeSpan tsMax)
{
  List<SerieItem> lsOut = new List<SerieItem>();
  ls = ls.OrderBy(x => x.ts).ToList();
  double rangeMilliSec = tsMax.Subtract(tsMin).TotalMilliseconds;
  double deltaMilliSec = rangeMilliSec / (double)(nSamples-1);
  TimeSpan tsDelta = TimeSpan.FromMilliseconds(deltaMilliSec);

  for (int i = 0; i < nSamples; i++)
  {
      TimeSpan ts = tsMin.Add(TimeSpan.FromMilliseconds(deltaMilliSec * i));
      var a = GetLastMin(ls, ts);
      var b = GetFirstMax(ls, ts);

      var interpoladedValue = ComputeLinearInterpolation(a.ts.TotalMilliseconds, a.val, b.ts.TotalMilliseconds, b.val, ts.TotalMilliseconds);

      lsOut.Add(new SerieItem() { serie = serie, ts = ts, val = interpoladedValue });
  }

  return lsOut;
}

private static SerieItem GetFirstMax(List<SerieItem> ls, TimeSpan ts)
{
  var x = ls.SkipWhile(k => k.ts < ts).FirstOrDefault();
  return x ?? ls.Last();
}

private static SerieItem GetLastMin(List<SerieItem> ls, TimeSpan ts)
{
  var x = ls.TakeWhile(k => k.ts < ts).LastOrDefault();
  return x ?? ls.First();
}

private static List<SerieItem> GetInputData(TimeSpan tsDelta, int n)
{
  Random r = new Random(DateTime.Now.Millisecond);

  List<SerieItem> lsOut = new List<SerieItem>();

  for (int i = 1; i <= n; i++)
	{
      string serie = string.Format("serie_{0}", i);
      //TimeSpan ts = TimeSpan.FromMinutes(r.NextDouble() * 5);
	  TimeSpan ts = TimeSpan.Zero;
      while(ts < TimeSpan.FromDays(1))
      {
          lsOut.Add(new SerieItem() { ts = ts, serie = serie, val = Math.Sin((Math.PI*2) * ts.TotalDays) * 100 });
          ts = ts.Add(tsDelta);
      }
	 
	}
  return lsOut;
}

public static double ComputeLinearInterpolation(double x0, double y0, double x1, double y1, double x)
{
  if (x1 == x0)
  {
      //throw new ApplicationException("x1 cannot be equal to x0");
      return (y0+y1)/2D;
  }

  return y0 * (x - x1) / (x0 - x1) + y1 * (x - x0) / (x1 - x0);
}

class SerieItem
{
   public string serie { get; set; }
   public TimeSpan ts { get; set; }
   public double val { get; set; }

   public override string ToString()
   {
       return string.Format("[{0}] ts = {1} val = {2}", serie, ts, val);
   }
}
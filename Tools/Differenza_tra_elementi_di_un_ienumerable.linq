<Query Kind="Statements" />

List<Tuple<DateTime, long>> ls = new List<Tuple<DateTime, long>>();
Random r = new Random();

for (int i = 0; i < 20; i++)
{
	ls.Add(new Tuple<DateTime, long>(DateTime.Now, DateTime.Now.Ticks));
	Thread.Sleep(r.Next(100, 1000));
}

//ls.Dump();


var differences = ls.Zip(ls.Skip(1), (x, y) => new Tuple<TimeSpan, long>(y.Item1 - x.Item1, y.Item2 - x.Item2));
differences.Dump();
<Query Kind="Statements">
  <Output>DataGrids</Output>
</Query>

Dictionary<TimeSpan, decimal> pointCollection = new Dictionary<TimeSpan, decimal>();
Random r = new Random(DateTime.Now.Millisecond);
TimeSpan ts = TimeSpan.Zero;
var delta = 1M;
decimal last = 0M;

for (int i = 0; i < 1440; i++)
{
	pointCollection[ts] = last;
	ts += TimeSpan.FromMinutes(1);
	last = last + delta;
	
	var change = r.Next(1,200);
	if(change == 1)
	{
		delta = r.Next(-1000, 1000) / 1000M;
	}
}
	
pointCollection.Dump("Input");
	
//
// CALC DISCONTINUITY POINTS


var discontinuityPoints =
	pointCollection.Skip(1)
		.Zip(pointCollection, (c, p) => new { Prev_Key = p.Key, Prev_Val = p.Value, Curr_Key = c.Key, Curr_Val = c.Value })
		.Zip(pointCollection.Skip(2), (cp, n) => new { Prev_Key = cp.Prev_Key, Prev_Val = cp.Prev_Val, Curr_Key= cp.Curr_Key, Curr_Val = cp.Curr_Val, Next_Key = n.Key, Next_Val = n.Value })
		.Dump("Expanded")
		.Where(x => x.Curr_Val - x.Prev_Val != x.Next_Val - x.Curr_Val)
		.Select(x => new { Key = x.Curr_Key, Value = x.Curr_Val })
		.ToList();

// Add first & last
if(discontinuityPoints.First().Key != pointCollection.First().Key)
	discontinuityPoints.Insert(0, new { Key = pointCollection.First().Key, Value = pointCollection.First().Value });
	
if(discontinuityPoints.Last().Key != pointCollection.Last().Key)
	discontinuityPoints.Add(new { Key = pointCollection.Last().Key, Value = pointCollection.Last().Value });
	
discontinuityPoints.ToDictionary(k => k.Key, v => v.Value)
	.Dump("Set Points");
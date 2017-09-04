<Query Kind="Program">
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

void Main()
{
	var ts = new CancellationTokenSource();
	CancellationToken ct = ts.Token;		

	Action<object> a1 = (o) =>
	{
		Tuple<string, int> parm = (Tuple<string, int>)o;
		Random rnd = new Random(DateTime.Now.Millisecond);
		
		try
		{	        
			int count = 10;
			while(count-- > 0)
			{
				// Graceful death
				if(ct.IsCancellationRequested)
				{
					Console.WriteLine ("Task {0}: Another task raised an exception, end at {1}!", parm.Item1, count);
					break;
				}
					
				Console.WriteLine ("Task {0}: {1}", parm.Item1, count);
				Thread.Sleep(parm.Item2);
				
				// Simula una bomba random
				if(parm.Item1 == "BOMBER")
				{
					if(rnd.Next(5) == 0)
					{
						Console.WriteLine ("Task {0}: {1}", parm.Item1, "*** BOOOM! ***");
						throw new Exception("Random bomb exploded!");
					}
				}
				
			}
			
			Console.WriteLine ("Task {0} end!", parm.Item1);
		}
		catch (Exception)
		{
			ts.Cancel();
			throw;
		}
	};
	
	List<Task> lsTask = new List<Task>();
	lsTask.Add(Task.Factory.StartNew(a1,(new Tuple<string, int>("A", 1000)),ct));
	lsTask.Add(Task.Factory.StartNew(a1,(new Tuple<string, int>("B", 800)),ct));
	lsTask.Add(Task.Factory.StartNew(a1,(new Tuple<string, int>("C", 400)),ct));
	lsTask.Add(Task.Factory.StartNew(a1,(new Tuple<string, int>("D", 200)),ct));
	lsTask.Add(Task.Factory.StartNew(a1,(new Tuple<string, int>("E", 2000)),ct));
	lsTask.Add(Task.Factory.StartNew(a1,(new Tuple<string, int>("F", 50)),ct));
	lsTask.Add(Task.Factory.StartNew(a1,(new Tuple<string, int>("G", 333)),ct));
	lsTask.Add(Task.Factory.StartNew(a1,(new Tuple<string, int>("H", 2000)),ct));
	lsTask.Add(Task.Factory.StartNew(a1,(new Tuple<string, int>("BOMBER", 1000)),ct));

	try
	{	        
		Task.WaitAll(lsTask.ToArray());
	}
	catch (AggregateException)
	{
		Console.WriteLine ("Exception in one task was caught");
	}
	
	
	Console.WriteLine ("End!");
}
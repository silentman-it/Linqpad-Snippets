<Query Kind="Program">
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

void Main()
{
	bool completed;
	var o = TimedExecutionTask.Run(() => { while(true);return "asd"; }, TimeSpan.FromSeconds(5), out completed);
	
	completed.Dump("Completed?");
}

public static class TimedExecutionTask
{
	public static T Run<T>(Func<T> func, TimeSpan timeout, out bool isCompleted)  
	{
		var ts = new CancellationTokenSource();
		CancellationToken ct = ts.Token;		
	
		T result = default(T);  
		Task t = Task.Factory.StartNew(() => result = func(), ct);  
		t.Wait(timeout);
		
		isCompleted = t.IsCompleted;
		if(!isCompleted)
		{
			ts.Cancel();
		}
		return result;  
	}  
	
	public static T Run<T>(Func<T> func, TimeSpan timeout)  
	{  
		bool isCompleted;
		return Run(func, timeout, out isCompleted);  
	}
}

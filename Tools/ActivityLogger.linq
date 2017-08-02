<Query Kind="Program" />

void Main()
{
	using(ActivityLogger.CreateNew("RandomActivity", (a) => Console.Write("{0}....", a), (a,ts) => Console.WriteLine ("{0} took {1}", a, ts.ToString())))
	{
		Thread.Sleep(2000);
	}
}

// Define other methods and classes here
class ActivityLogger : IDisposable
{
   #region ActivityType Property
   private string _ActivityType;
   public string ActivityType
   {
       get
       {
           return _ActivityType;
       }
       set
       {
           _ActivityType = value;
       }
   }
   #endregion

   #region MyStopwatch Property
   private Stopwatch _MyStopwatch;
   public Stopwatch MyStopwatch
   {
       get
       {
           return _MyStopwatch;
       }
       set
       {
           _MyStopwatch = value;
       }
   }
   #endregion

   Action<string, TimeSpan> OnFinished { get; set; }

   public ActivityLogger(string activityType, Action<string> beforeStarting, Action<string, TimeSpan> onFinished)
   {
       ActivityType = activityType;
       OnFinished   = onFinished;

       if (beforeStarting != null)
       {
           beforeStarting.Invoke(activityType);
       }

       MyStopwatch = Stopwatch.StartNew();
   }

   public void Dispose()
   {
       MyStopwatch.Stop();
       OnFinished.Invoke(ActivityType, MyStopwatch.Elapsed);
   }

   public static ActivityLogger CreateNew(string activityType, Action<string, TimeSpan> onFinished)
   {
       return new ActivityLogger(activityType, null, onFinished);
   }

   public static ActivityLogger CreateNew(string activityType, Action<string> beforeStarting, Action<string, TimeSpan> onFinished)
   {
       return new ActivityLogger(activityType, beforeStarting, onFinished);
   }
}

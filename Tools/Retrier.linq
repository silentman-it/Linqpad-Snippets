<Query Kind="Program" />

void Main()
{
	Random rnd = new Random();

	Retrier.Factory.TryWithDelay(() =>
	{
		for (int i = 0; i < 50; i++)
		{
			if(rnd.Next(1,20) == 5)
				throw new Exception();
				
			Console.WriteLine ("OK {0}", i);
		}
		
		Console.WriteLine ("SUCCESS!!!");	
	}, 10, 1000);
}

// Define other methods and classes here
public class Retrier : Retrier<object>
{
   public static Retrier Factory
   {
       get
       {
           return new Retrier();
       }
   }
   
   public void Try(Action func, int maxRetries)
   {
      base.Try(() =>
	  {
	  	func();
		return null;
	  }, maxRetries);
	  
   }
   
   public void TryWithDelay(Action func, int maxRetries, int delayInMilliseconds)
   {
      base.TryWithDelay(() =>
	  {
	  	func();
		return null;
	  }, maxRetries, delayInMilliseconds);
   }
   
}

public class Retrier<TResult>
{
   public Retrier()
   {
   }

   public static Retrier<TResult> Factory
   {
       get
       {
           return new Retrier<TResult>();
       }
   }


   #region ExceptionCaught EventArgs
   public class ExceptionCaughtEventArgs : EventArgs
   {
       public Exception Exception { get; set; }
       public int Trial { get; set; }
   }
   #endregion

   #region ExceptionCaught Event
   public delegate void ExceptionCaughtEventHandler(object sender, ExceptionCaughtEventArgs args);

   public event ExceptionCaughtEventHandler ExceptionCaught;

   protected virtual void OnExceptionCaught(object sender, ExceptionCaughtEventArgs args)
   {
       if (ExceptionCaught != null)
       {
           ExceptionCaught(sender, args);
       }
   }
   #endregion

   public TResult Try(Func<TResult> func, int maxRetries)
   {
       return TryWithDelay(func, maxRetries, 0);
   }

   public TResult TryWithDelay(Func<TResult> func, int maxRetries, int delayInMilliseconds)
   {
       TResult returnValue = default(TResult);
       int numTries = 0;
       bool succeeded = false;
       while (numTries < maxRetries)
       {
           try
           {
               returnValue = func();
               succeeded = true;
           }
           catch (Exception e)
           {
               OnExceptionCaught(this, new ExceptionCaughtEventArgs() { Exception = e, Trial = numTries + 1 });
           }
           finally
           {
               numTries++;
           }
           if (succeeded)
           {
               return returnValue;
           }

           System.Threading.Thread.Sleep(delayInMilliseconds);
       }

       return default(TResult);
   }
}
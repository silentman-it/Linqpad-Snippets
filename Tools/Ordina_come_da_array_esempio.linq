<Query Kind="Program" />

void Main()
{
	string[] example = { "MSD4", "MSD3", "MSD2", "MSD1" };
	string[] unsorted = { "MSD4", "MSD1", "dh", "MSD2", "dhdgh" };
	
	unsorted
		.Where(x => example.Contains(x))
		//.ToList()
		.OrderBy(x => x, ExampleComparer<string>.SortAsExample(example)).Dump();
	
	
}

// Define other methods and classes here

//class ExampleComparer : IComparer<string>
//{
//	List<string> exampleArray;
//	
//	public ExampleComparer(IEnumerable<string> ex)
//	{
//		exampleArray = ex.ToList();
//	}	
//
//	public int Compare(string a, string b)
//	{
//		if(!exampleArray.Contains(a) || !exampleArray.Contains(b))
//			throw new Exception("Gli elementi da comparare devono essere contenuti nell'array di esempio");
//	
//		if(exampleArray.IndexOf(a) > exampleArray.IndexOf(b))
//			return 1;
//		if(exampleArray.IndexOf(a) < exampleArray.IndexOf(b))
//			return -1;
//		else
//			return 0;
//	}
//	
//	public static IComparer<string> SortAsExample(IEnumerable<string> ex)
//	{      
//		return (IComparer<string>) new ExampleComparer(ex);
//	}	
//}
//

private class ExampleComparer<T> : IComparer<T>
{
	List<T> exampleArray;
	
	public ExampleComparer(IEnumerable<T> ex)
	{
		exampleArray = ex.ToList();
	}	

	public int Compare(T a, T b)
	{
		if(!exampleArray.Contains(a) || !exampleArray.Contains(b))
			throw new Exception("Gli elementi da comparare devono essere contenuti nell'array di esempio");
	
		if(exampleArray.IndexOf(a) > exampleArray.IndexOf(b))
			return 1;
		if(exampleArray.IndexOf(a) < exampleArray.IndexOf(b))
			return -1;
		else
			return 0;
	}
	
	public static IComparer<T> SortAsExample(IEnumerable<T> ex)
	{      
		return (IComparer<T>) new ExampleComparer<T>(ex);
	}	
}
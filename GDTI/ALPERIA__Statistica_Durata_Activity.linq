<Query Kind="Program">
  <NuGetReference>ClosedXML</NuGetReference>
  <Namespace>ClosedXML.Excel</Namespace>
  <Namespace>System.Dynamic</Namespace>
  <Namespace>System.Globalization</Namespace>
</Query>

void Main()
{
	var xlsFileName = @"C:\Users\Federico\Desktop\ACTIVITIES.xlsx";
	
	var workbook = new XLWorkbook(xlsFileName);
	
	IXLWorksheet ws1;
	var found = workbook.TryGetWorksheet("ED_FRAMEWORK.ACTIVITIES", out ws1); 
	
	if(!found) throw new Exception("Worksheet not found!");
	
	IXLRow r = ws1.Row(2);
	
	List<Activity> ls = new List<Activity>();
	
	while(!r.IsEmpty())
	{
		try
		{	        
			var a = new Activity {
				ActivityId = Convert.ToInt32(r.Cell("A").Value),
				MessageId  = Convert.ToInt32(r.Cell("B").Value),
				Name       = r.Cell("D").Value.ToString(),
				Status     = r.Cell("F").Value.ToString(),
				ExecutionStart = r.Cell("S").IsEmpty() ? (DateTime?)null : DateTime.ParseExact(r.Cell("S").Value.ToString().Replace(".000000000", ""), "dd-MMM-yy hh.mm.ss tt", CultureInfo.GetCultureInfo("en-US")),
				ExecutionEnd   = r.Cell("T").IsEmpty() ? (DateTime?)null : DateTime.ParseExact(r.Cell("T").Value.ToString().Replace(".000000000", ""), "dd-MMM-yy hh.mm.ss tt", CultureInfo.GetCultureInfo("en-US")),
			};
			
			a.Duration = a.ExecutionEnd - a.ExecutionStart;
			
			ls.Add(a);
			
			r = r.RowBelow();
			
		}
		catch (Exception ex)
		{
			Console.WriteLine("Error at row " + r.RowNumber());
			throw;
		}
		
	}
	
	
	ls
	.OrderByDescending(x => x.Duration)
	.Dump();
}

// Define other methods and classes here
class Activity
{
	public int ActivityId { get; set; }
	public int MessageId { get; set; }
	public string Name { get; set; }
	public string Status { get; set; }
	public DateTime? ExecutionStart { get; set; }
	public DateTime? ExecutionEnd { get; set; }
	public TimeSpan? Duration { get; set; }
}
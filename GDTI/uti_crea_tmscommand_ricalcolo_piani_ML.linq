<Query Kind="Statements" />

string xmlString = "<TmsCommands xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" Type=\"REAL_TIME\"><Command><Integer Name=\"FormulaId\">%%FORMULAID%%</Integer><DateTime Name=\"ValidityStartDate\">%%STARTDATE%%</DateTime><Integer Name=\"ValidityStartDateOffset\">0</Integer><DateTime Name=\"ValidityEndDate\">%%ENDDATE%%</DateTime><Integer Name=\"ValidityEndDateOffset\">0</Integer><String Name=\"PlanType\">%%PLANTYPE%%</String><Boolean Name=\"DebugMode\">false</Boolean></Command></TmsCommands>";

var outDir = @"c:\temp\inbox";
DateTime dtFrom = DateTime.Parse("01/01/2014");
DateTime dtTo = DateTime.Parse("02/01/2014");
var formulaId = 1871;

string[] plans = { "PVtc", "PVM", "PVMC" };
for (DateTime d = dtFrom; d < dtTo; d = d.AddDays(+1))
{
	foreach (var pt in plans)
	{
		var fileContent = xmlString
			.Replace("%%STARTDATE%%", d.ToString("yyyy-MM-ddTHH:mm:ss"))
			.Replace("%%ENDDATE%%", d.AddDays(1).ToString("yyyy-MM-ddTHH:mm:ss"))
			.Replace("%%PLANTYPE%%", pt)
			;
		var fileName = Path.Combine(outDir, string.Format("TMS_Command_{0}_{1}_handmade.xml", d.ToString("yyyyMMdd"), pt));
		
		Console.Write ("Writing {0}... ", fileName);
		//File.WriteAllText(fileName, fileContent);
		
		fileContent.Dump();
		Console.WriteLine ("OK!");
	}
}
<Query Kind="Program">
  <Connection>
    <ID>67df69ce-0a34-4cd8-9acc-b62224eadf78</ID>
    <Persist>true</Persist>
    <Driver>EntityFrameworkDbContext</Driver>
    <CustomAssemblyPathEncoded>&lt;MyDocuments&gt;\Visual Studio 2012\Projects\ProvaEF5\MyEntities\bin\Debug\MyEntities.dll</CustomAssemblyPathEncoded>
    <CustomAssemblyPath>C:\Users\CoppolaF\Documents\Visual Studio 2012\Projects\ProvaEF5\MyEntities\bin\Debug\MyEntities.dll</CustomAssemblyPath>
    <CustomTypeName>MyEntities.MyModelContainer</CustomTypeName>
    <AppConfigPath>C:\Users\CoppolaF\Documents\Visual Studio 2012\Projects\ProvaEF5\MyEntities\bin\Debug\MyEntities.dll.config</AppConfigPath>
  </Connection>
  <Output>DataGrids</Output>
</Query>

void Main()
{
	string filePath = @"C:\Temp\EMODS_Dump\EMODS_Dump.txt";
	int c = 0;
	
	var sdg = new SerieDetailGroup();
	var flow = new Flow();
	
	using (StreamReader sr = new StreamReader(filePath))
	{
		string line;
		while ((line = sr.ReadLine()) != null)
		{
			var aLine = line.Split(',');

			try
			{	        
				string src = aLine[0].Trim('"');
				string sym = aLine[1].Trim('"');
				string cls = aLine[2].Trim('"');
				string chn = aLine[3].Trim('"');
				string ctx = aLine[4].Trim('"');
				
				DateTime startDate = DateTime.Parse(aLine[5]);
				int startDateOffset = Int32.Parse(aLine[6]);
				DateTime endDate = DateTime.Parse(aLine[7]);
				int endDateOffset = Int32.Parse(aLine[8]);
				
				string sv = string.Format("{0},{1}", aLine[9], aLine[10]);
				double v = Double.Parse(sv);
				
				var serie = AddSerie(src, sym, cls, chn, ctx);
			
				SerieDetail sd = new SerieDetail
				{
					Serie = serie,
					//SerieDetailGroup = sdg,
					TimeStamp = DateTime.Now,
					DoubleValue = v,
					ValidityStartDate = startDate,
					ValidityEndDate = endDate
				};
				
				sdg.SerieDetail.Add(sd);
				
				if(c++ % 1000 == 0) Console.WriteLine ("SDG count = {0}", sdg.SerieDetail.Count());
			}
			catch (Exception ex)
			{
				ex.Message.Dump("ERRORE!");
				continue;
			}
		
		}
		
		flow.SerieDetailGroup.Add(sdg);

	}
	
	SaveChangesEx();
}

// Define other methods and classes here

Serie AddSerie(string src, string sym, string cls, string chn, string ctx)
{
	var serie = SerieSet.FirstOrDefault(x => x.Source.Name == src && x.Symbol.Name == sym && x.Class.Name == cls && x.Channel.Name == chn && x.Context.Name == ctx);
	if(serie == null)
	{
		throw new Exception("SERIE NOT FOUND!!!!");
	}
	
	return serie;
}

void SaveChangesEx()
{
	Stopwatch sw = Stopwatch.StartNew();
	SaveChanges();				
	sw.Stop();
	Console.WriteLine("Wrote in {0} secs", sw.Elapsed.TotalSeconds);
}
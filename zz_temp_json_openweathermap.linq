<Query Kind="Program">
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>Newtonsoft.Json</Namespace>
  <Namespace>Newtonsoft.Json.Converters</Namespace>
  <Namespace>Newtonsoft.Json.Linq</Namespace>
  <Namespace>Newtonsoft.Json.Serialization</Namespace>
  <Namespace>System.Globalization</Namespace>
</Query>

void Main()
{
	var fileName = @"C:\Users\CoppolaF\Desktop\meteo_limj_2013.json";
	
	JObject o = (JObject)JsonConvert.DeserializeObject(File.ReadAllText(fileName));
	
	foreach (var e in o["list"])
	{
		var dt = ConvertToDateTime(e["dt"].ToString());
		
		WriteLine(e, dt, "humidity", "ma", null);
		WriteLine(e, dt, "humidity", "mi", null);
		WriteLine(e, dt, "humidity", "v", null);
		WriteLine(e, dt, "pressure", "ma", null);
		WriteLine(e, dt, "pressure", "mi", null);
		WriteLine(e, dt, "pressure", "v", null);
		WriteLine(e, dt, "temp", "ma", null, d => d - 273.15);
		WriteLine(e, dt, "temp", "mi", null, d => d - 273.15);
		WriteLine(e, dt, "temp", "v", null, d => d - 273.15);
		WriteLine(e, dt, "wind", "speed", "ma", null);
		WriteLine(e, dt, "wind", "speed", "mi", null);
		WriteLine(e, dt, "wind", "speed", "v", null);
		WriteLine(e, dt, "wind", "deg", "v", null);
	}	

}

// Define other methods and classes here
DateTime ConvertToDateTime(string jDate)
{
    DateTime dt = new DateTime(1970,1,1,0,0,0,0);
    dt = dt.AddSeconds(Convert.ToDouble(jDate)).ToLocalTime().Dump();
	return dt;
}

void WriteLine(JToken t, DateTime dt, string cls, string chn, string ctx, Func<double, double> fu = null)
{
	NumberFormatInfo nfi = new NumberFormatInfo();
	nfi.NumberDecimalSeparator = ".";

	JToken v = null;
	try
	{

		v = ctx != null ? t[cls][chn][ctx] : t[cls][chn];
		double d;
			d = Convert.ToDouble(v.ToString());
		if(fu != null)
			d = fu.Invoke(d);
			
		File.AppendAllText(@"c:\temp\Weather_Data_Dump.csv",
			string.Format("{0};{1};{2};{3};{4};{5};{6};\r\n",
				"OpenWeather",
				"LIMJ",
				cls,
				chn,
				ctx,
				dt.ToString("dd/MM/yyyy HH:mm:ss"),
				d.ToString(nfi)));
			
	}
	catch(Exception)
	{
		Console.WriteLine ("ERROR converting \"{0}\". Path = {1}/{2}/{3}", v, cls, chn, ctx);
		return;
	}
			
}
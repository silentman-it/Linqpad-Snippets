<Query Kind="Program">
  <Namespace>System.Collections.Generic</Namespace>
  <Namespace>System.Globalization</Namespace>
</Query>

void Main()
{
	double num;
	var o = BinarySerialization.Deserialize<List<Tuple<DateTime, string, string, object>>>(File.ReadAllBytes(@"C:\Temp\plainData.bin"));
	var errData = o
		// Solo PSMIN e PSMAX
		.Where(x => x.Item3.IsIn(FlowNameConst.PSMIN, FlowNameConst.PSMAX))
		.Dump()
		.Select(x => new { TS = x.Item1, Assetto = x.Item2, Chan = x.Item3, Val = Double.Parse(x.Item4.ToString()) })
		// Raggruppo per Timestamp
		.GroupBy(x => x.TS)
		.Select(x => new
		{
			TS = x.Key,
			// Applico il corretto ordinamento: questa lista dovrebbe essere ordinata per Val
			OrderedValues = x
				// Raggruppo per assetto
				.GroupBy(gb => gb.Assetto)
				// Ordino la lista raggruppata per il valore di PSMIN
				.OrderBy(ob => ob.Single(s => s.Chan  == FlowNameConst.PSMIN).Val)
				// Unisco tutto in un'unica lista
				.SelectMany(sm => sm.ToList())

		})
		// Seleziono le sottoliste non ordinate per valore
		//.Where(x => !x.OrderedValues.IsOrderedBy(iso => iso.Val))
		.Dump()
		;
		
}






// Define other methods and classes here
static class BinarySerialization
{
   public static byte[] Serialize<T>(T p)
   {
       if (p == null)
           return null;

       MemoryStream writeStream = new MemoryStream();
       System.Runtime.Serialization.Formatters.Binary.BinaryFormatter formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
       formatter.Serialize(writeStream, p);
       writeStream.Seek(0, SeekOrigin.Begin);
       byte[] buff = new byte[writeStream.Length];
       writeStream.Read(buff, 0, Convert.ToInt32(writeStream.Length));
       writeStream.Close();
       return buff;
   }

   public static T Deserialize<T>(byte[] p)
   {
       MemoryStream readStream = new MemoryStream(p);
       System.Runtime.Serialization.Formatters.Binary.BinaryFormatter formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
       T readData = (T)formatter.Deserialize(readStream);
       readStream.Close();
       return readData;
   }
}

public static class NumericTypesExtensions
{
   public static bool IsInRange<T>(this T v, T lowerLimit, T upperLimit)
       where T: IComparable
   {
       return (v.CompareTo(lowerLimit) >= 0 && v.CompareTo(upperLimit) <= 0);
   }

}

public static class IEnumerableExtensions
{
	public static bool IsOrderedBy<T, K>(this IEnumerable<T> list, Func<T, K> memberSelector)
		where K: IComparable
	{
		return list.Zip(list.Skip(1), (a, b) => new { a, b }).All(p => memberSelector(p.a).CompareTo(memberSelector(p.b)) <= 0);
	}

	public static bool IsOrdered<T>(this IEnumerable<T> list)
		where T: IComparable
	{
		return list.Zip(list.Skip(1), (a, b) => new { a, b }).All(p => p.a.CompareTo(p.b) <= 0);
	}
}

public static class StringExtensions
{
   public static bool IsNullOrEmpty(this string s)
   {
       return string.IsNullOrEmpty(s);
   }

   public static bool MatchesRegEx(this string s, string regEx, RegexOptions options = RegexOptions.IgnoreCase)
   {
       Match match = Regex.Match(s, regEx, options);
       return (match.Success);
   }

   public static Match RegEx(this string s, string regEx)
   {
       return Regex.Match(s, regEx, RegexOptions.IgnoreCase);
   }

   public static bool IsIn(this string s, params string[] args)
   {
       foreach (var item in args)
       {
           if (s.MatchesRegEx(item))
               return true;
       }

       return false;
   }
}

public static class FlowNameConst
{
	public static readonly string PSMIN    = "soglia_limite_potenza_min";
	public static readonly string PSMAX    = "soglia_limite_potenza_max";
	public static readonly string PTMIN    = "telemisurapotmin";
	public static readonly string PTMAX    = "telemisurapotmax";
	public static readonly string TRISP    = "temporisposta";
	public static readonly string GPA      = "gradientepotsalire";
	public static readonly string GPD      = "gradientepotscendere";
	public static readonly string TRAMPA   = "temporampa";
	public static readonly string TDERAMPA = "tempoderampa";
	public static readonly string TAVA     = "tempoavviamento";
	public static readonly string TARA     = "tempoarresto";
	public static readonly string BRS      = "semibandaregolazione";
	public static readonly string PNR      = "PNR";
}
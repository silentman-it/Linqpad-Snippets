<Query Kind="Program">
  <Connection>
    <ID>c0b901f2-09b0-4fb4-8578-e8c692ab9ed2</ID>
    <Persist>true</Persist>
    <Driver>EntityFrameworkDbContext</Driver>
    <CustomAssemblyPath>C:\Work\TMS\DEV\TMS\Common\GDTI.Data\bin\Debug\GDTI.Data.dll</CustomAssemblyPath>
    <CustomTypeName>GDTI.Data.EDMWarehouseEntities</CustomTypeName>
    <AppConfigPath>C:\Work\TMS\DEV\TMS\Web\TMS.WS\Web.config</AppConfigPath>
  </Connection>
  <Reference>C:\Work\TMS\DEV\TMS\Activities\GDTI.Activities.RUP\bin\Debug\GDTI.Activities.RUP.dll</Reference>
  <Reference>C:\Work\TMS\DEV\TMS\Common\GDTI.Data\bin\Debug\GDTI.Data.dll</Reference>
  <Namespace>GDTI.Data</Namespace>
  <Namespace>GDTI.Activities.RUP.Schemas.DynamicRUP.Outbound</Namespace>
</Query>

void Main()
{
	var rupLocale = GDTIData.Instance.Get("RUPLocal", "UP_TURBIGO_4", null, null, null, DateTimeOffset.Parse("26/10/2014"), DateTimeOffset.Parse("27/10/2014"));
	var rupTerna  = GDTIData.Instance.Get("RUPTerna", "UP_TURBIGO_4", null, "VariationID", null, DateTimeOffset.Parse("26/10/2014"), DateTimeOffset.Parse("27/10/2014"));
	
	var unitName = "UP_TURBIGO_4";
	var data = rupLocale.Concat(rupTerna);
	
	var table = data
		.SelectMany(d1 => d1.ExtendedData.Select(d2 => new { Unita = d1.Symbol, Fascia = d1.Class, Channel = d1.Channel, Start = d2.Key.Start, End = d2.Key.End, Value = d2.Value }))
		.GroupBy(x => new { x.Start, x.End, x.Fascia })
		.GroupBy(x => new { x.Key.Start, x.Key.End })
		.Dump()
		.Select(vdt => new FLUSSOVDT()
		{
			CODICEETSO = unitName,
			DATAORAINIZIO = vdt.Key.Start.DateTime,
			DATAORAFINE = vdt.Key.End.DateTime,
//			IDMOTIVAZIONE = vdt.
		})
		.Dump()
	
	
		
		
		
//		.ToPivotTable(x => x.Key.ToString("dd/MM/yyyy HH:mm:ss zzzz"), x => x.Channel, x => x.FirstOrDefault().Value)
//		.AsEnumerable()
//		.GroupBy(x => x["Assetto"])
//		.Dump()
		;

	
//	table
//		.AsEnumerable()
//		.Select(x => new FLUSSOVDT
//		{
//			CODICEETSO = unitName,
//			DATAORAINIZIO = DateTime.Parse((string)x["Key"]),
//			DATAORAFINE = DateTime.Parse((string)x["Key"]).AddMinutes(15),
//			IDMOTIVAZIONE = (string)x["MotivationID"],
//			NOTE = (string)x["Note"],
//		})
	//.Dump()
	;
	
	
}

// Define other methods and classes here
public static class IEnumerableExtensions
{
        public static DataTable ToPivotTable<TSource, TFirstKey, TSecondKey, TValue>(this IEnumerable<TSource> source, Func<TSource, TFirstKey> firstKeySelector, Func<TSource, TSecondKey> secondKeySelector, Func<IEnumerable<TSource>, TValue> aggregate)
        {
            DataTable dt = new DataTable();
            List<DataColumn> lsCols = new List<DataColumn>();
            var dic = source.ToPivotDictionary(firstKeySelector, secondKeySelector, aggregate);
            dt.Columns.Add("Key", typeof(TFirstKey));
            foreach (var kvp1 in dic)
            {
                foreach (var kvp2 in kvp1.Value)
                {
                    var colName = kvp2.Key.ToString();

                    if (!lsCols.Any(x => x.ColumnName == colName))
                        lsCols.Add(new DataColumn(colName, typeof(TValue)));
                }
            }

            lsCols = lsCols.OrderBy(x => x.ColumnName).ToList();
            foreach (var c in lsCols)
            {
                dt.Columns.Add(c);
            }

            foreach (var kvp1 in dic)
            {
                foreach (var kvp2 in kvp1.Value)
                {
                    DataRow dr = dt.Select(string.Format("Key = '{0}'", kvp1.Key.ToString().CrappyEscapeForSQL())).FirstOrDefault();
                    if (dr == null)
                    {
                        dr = dt.Rows.Add(kvp1.Key);
                    }

                    dr[kvp2.Key.ToString()] = (TValue)kvp2.Value;
                }
            }
            return dt;
        }

        private static Dictionary<TFirstKey, Dictionary<TSecondKey, TValue>> ToPivotDictionary<TSource, TFirstKey, TSecondKey, TValue>(this IEnumerable<TSource> source, Func<TSource, TFirstKey> firstKeySelector, Func<TSource, TSecondKey> secondKeySelector, Func<IEnumerable<TSource>, TValue> aggregate)
        {
            var retVal = new Dictionary<TFirstKey, Dictionary<TSecondKey, TValue>>();

            var l = source.ToLookup(firstKeySelector);
            foreach (var item in l)
            {
                var dict = new Dictionary<TSecondKey, TValue>();
                retVal.Add(item.Key, dict);
                var subdict = item.ToLookup(secondKeySelector);
                foreach (var subitem in subdict)
                {
                    dict.Add(subitem.Key, aggregate(subitem));
                }
            }

            return retVal;
        }
		
        private static string CrappyEscapeForSQL(this string str)
        {
            str = str.Replace("'", "''");
            return str;
        }

}
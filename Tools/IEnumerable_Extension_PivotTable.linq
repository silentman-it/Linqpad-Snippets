<Query Kind="Program">
  <Connection>
    <ID>1b664880-aaa6-49a8-86d0-43deea4970dd</ID>
    <Persist>true</Persist>
    <Driver>EntityFrameworkDbContext</Driver>
    <CustomAssemblyPath>C:\Work\EMODS\DEV\EMODS2\Data\EMODS.Data\bin\Debug\EMODS.Data.dll</CustomAssemblyPath>
    <CustomTypeName>EMODS.Data.EDMWarehouseContainer</CustomTypeName>
    <AppConfigPath>C:\Work\EMODS\DEV\EMODS2\Data\EMODS.Data\bin\Debug\EMODS.Data.dll.config</AppConfigPath>
  </Connection>
</Query>

void Main()
{
	
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
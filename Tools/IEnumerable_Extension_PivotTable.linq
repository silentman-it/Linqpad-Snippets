<Query Kind="Program">
  <Connection>
    <ID>6cf3b10a-51ab-4f64-aff4-c9e0ed61c18b</ID>
    <Persist>true</Persist>
    <Driver Assembly="IQDriver" PublicKeyToken="5b59726538a49684">IQDriver.IQDriver</Driver>
    <Provider>Devart.Data.Oracle</Provider>
    <CustomCxString>AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAAkOYp3WDye0ORXtly5hEZsAAAAAACAAAAAAAQZgAAAAEAACAAAAAnehYk33TCap+ZG+UitfxwOC9oYbSTudp2ebMbXBYItwAAAAAOgAAAAAIAACAAAABXdokyfHUvMkkoFPua2dx25HCX4SyNHMiMvz/viDFwksAAAAByE7r+f5bdw3jEGoMz5r9sfEQmhKcPknE9LmAHX1Rm7JlQ1tR4H295xj1qjXGqF9ZqsfJbWS5TgYTIfPhAZMnvG11X3Sdk1/qz8NyuS99jphEOjRR06uyPZSnL5iUkFGvvEkdrZrNuHr8fN9ukhjBq1buwBlaBEP8olgziGGGbZS9RZJGhb8BxgY1pHwDFsMbYt6UkJi5LG4InFEnEWb0AMHmNPfoydgnxTJYIkQY6RFKuPTb6S4k8wATdWspk14dAAAAAi05jGAFAdJRT00qgnT9FJMC/euvvYcY77EQAar41LU6yGj4HqxqqP756qFgaCxwhsaht+7DdejIYcZYYtCjLag==</CustomCxString>
    <EncryptCustomCxString>true</EncryptCustomCxString>
    <DisplayName>win2008r2test1.ed_tms</DisplayName>
    <UserName>ed_tms</UserName>
    <Password>AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAAXw9k02xJM0uVTAPS/QGCJwAAAAACAAAAAAAQZgAAAAEAACAAAACe4YWygoqaWlbRNHnNvUQ3XVr51Qn6HnGNE3fdXLAYMwAAAAAOgAAAAAIAACAAAAAYsyvVah0qDenyPdC1okQt3PabIEfI8ViVYb6MYTMw+hAAAAA51YDTrFdVNTnCRwlTg9IkQAAAAD/K4CqVbS+7Z1JUOjLsnWftsxY1iKJ939pZxe9Rf+tfeFsDn13uggPUulCmATHRZTq/0qQ93/vxovGdeVw77ww=</Password>
    <Server>(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=Win2008R2Test1.edg.grptop.net)(PORT=1521))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=ETR11DM1)))</Server>
    <DriverData>
      <StripUnderscores>true</StripUnderscores>
      <QuietenAllCaps>true</QuietenAllCaps>
      <ConnectAs>Default</ConnectAs>
      <UseOciMode>true</UseOciMode>
    </DriverData>
  </Connection>
</Query>

void Main()
{
	ActivitiesAssociations
		.OrderBy(x => x.ReferenceNoTx)
		.ToList()
		.ToPivotTable(x => x.ReferenceNoTx, x => x.ActivityCd, x => x.Any() ? true : false)
		.ToListOf<UnitActivityAssociation>()
		.Dump();

}

public class UnitActivityAssociation
{
	public string Key { get; set; }
	public bool? GAS_CONS { get; set; }
	public bool? REAL_TIME { get; set; }
	public bool? RUP_MGMT { get; set; }
}



    public static class IEnumerableExtensions
    {
	
        public static List<T> ToListOf<T>(this DataTable dt)
        {
            const BindingFlags flags = BindingFlags.Public | BindingFlags.Instance;
            var columnNames = dt.Columns.Cast<DataColumn>()
                .Select(c => c.ColumnName)
                .ToList();
            var objectProperties = typeof(T).GetProperties(flags);
            var targetList = dt.AsEnumerable().Select(dataRow =>
            {
                var instanceOfT = Activator.CreateInstance<T>();

                foreach (var properties in objectProperties.Where(properties => columnNames.Contains(properties.Name) && dataRow[properties.Name] != DBNull.Value))
                {
                    properties.SetValue(instanceOfT, dataRow[properties.Name], null);
                }
                return instanceOfT;
            }).ToList();

            return targetList;
        }


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



<Query Kind="Program">
  <Connection>
    <ID>b318cdfc-42de-4061-9e9c-f72e11ab4749</ID>
    <Persist>true</Persist>
    <Driver Assembly="IQDriver" PublicKeyToken="5b59726538a49684">IQDriver.IQDriver</Driver>
    <Provider>Devart.Data.Oracle</Provider>
    <CustomCxString>AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAAhYrLhpjbjUKVEUXKx5QXqwAAAAACAAAAAAAQZgAAAAEAACAAAACNY7QniVA5iCr7nNeCi1Rfa0BH2RvdxvE1rrSPl+a48AAAAAAOgAAAAAIAACAAAABSPC80vVX3+R99L5Wou35OBm0p6rWhUhBjUHd4KTHDwsAAAABPgF0K40EexJgVv82s+EmxAaWeEuvh0F4XXJfuKkeQU+QaUzRWrytXVRXo5zOqggoTxvdVbvjLynw54K0rkwKoIf3eaInd/3NMHJCJx+spiaTZSnxvhA1zy2/x8+Orv4DPqMcjRc6vLNchzeiVGXDOWixIkVruuZnZW3XAjrhPmYi0mRIaIY63G4+xH29sMlCP4H4RCefC+tNye/baTFY4z+oDgl7gCzNZIFdmuhbLLgzDCJKOZUkJZxHFZawurRVAAAAA7uVF/zN0hNlGz9IQkZbNTPxrYBDkHd92BPtknR4DPkS2x9mdDKFZbmo2pQqBKKqOP13m9o4AcN01uLp8BWrplg==</CustomCxString>
    <EncryptCustomCxString>true</EncryptCustomCxString>
    <DisplayName>win2008r2test1.ed_warehouse</DisplayName>
    <Server>(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=Win2008R2Test1.edg.grptop.net)(PORT=1521))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=ETR11DM1)))</Server>
    <Password>AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAAhYrLhpjbjUKVEUXKx5QXqwAAAAACAAAAAAAQZgAAAAEAACAAAACsu7A3V7ZNkIdBvvvsBOHioixuOuuCEN/hL3c2M4pmtQAAAAAOgAAAAAIAACAAAADq5uXdajbbs3dCAeNbf9deFLC/AJ4CdJhB45G7YOUdghAAAACuwd7Q3DzsOOq7U3X0z6ANQAAAABJxGhDmdIUUphBdnaPGqwcU7XtKC5uu5dOel2LDs6WvmVnDujNsCxxNfYisDM58CKcQmfb8JFbZQcD1gGgSosU=</Password>
    <UserName>ed_warehouse</UserName>
    <DriverData>
      <StripUnderscores>true</StripUnderscores>
      <QuietenAllCaps>true</QuietenAllCaps>
      <ConnectAs>Default</ConnectAs>
      <UseOciMode>true</UseOciMode>
    </DriverData>
  </Connection>
  <Output>DataGrids</Output>
</Query>

void Main()
{
	List<Tuple<string, string, string>> lsCombinations = new List<Tuple<string, string, string>>()
	{
		new Tuple<string, string, string>( "UP_TURBIGO_4",    "19/07/2017", null ),
		new Tuple<string, string, string>( "UP_TURBIGO_4",    "20/07/2017", null ),
		
//		new Tuple<string, string, string>( "UP_TORINONORD_1", "09/10/2015", null         ),
//		new Tuple<string, string, string>( "UP_MONCALRPW_2",  "05/07/2015", "07/07/2015" ),
//		new Tuple<string, string, string>( "UP_MONCALRPW_2",  "21/08/2015", "23/08/2015" ),
//		new Tuple<string, string, string>( "UP_MONCALRPW_2",  "13/10/2015", "15/10/2015" ),
//		new Tuple<string, string, string>( "UP_TURBIGO_4",    "03/09/2015", "05/09/2015" ),
//		new Tuple<string, string, string>( "UP_TURBIGO_4",    "01/07/2015", "03/07/2015" ),
	};
	
	
	foreach(var item in lsCombinations)
	{
		var unit = item.Item1;
		var dtStart = DateTime.Parse(item.Item2);
		var dtEnd = string.IsNullOrEmpty(item.Item3) ? DateTime.Parse(item.Item2).AddDays(1) : DateTime.Parse(item.Item3).AddDays(1);
	
		string Source = "Terna";
		string Symbol = unit;
		string Class = "DispatchOrderMessage";
		string Channel = null;
		string Context = null;
		decimal? PrimaryKey = null;
		decimal? SecondaryKey = null;
		
		var lsSeries =
		Series
		.Where( x =>
			(Source != null ? x.SourceCd == Source : true)    &&
			(Symbol != null ? x.SymbolCd == Symbol : true)    &&
			(Class != null ? x.ClassCd == Class : true)       &&
			(Channel != null ? x.ChannelCd == Channel : true) &&
			(Context != null ? x.ContextCd == Context : true) &&
			(PrimaryKey != null ? x.PrimaryKey == PrimaryKey : true) &&
			(SecondaryKey != null ? x.SecondaryKey == SecondaryKey : true)
			)
		//.Dump()
		;
		
		var lsSid = lsSeries.Select(x => x.SerieID);
		
		SeriesDetails
		.Where(x => lsSid.Contains(x.SerieID) && x.ValidityStartDate >= dtStart && x.ValidityStartDate < dtEnd)
		.Select(x => new { x.StreamID, x.Serie.SymbolCd, x.Serie.ChannelCd, x.Value, x.Label, x.ValidityStartDate, x.ValidityEndDate })
		.ToList()
		.Dump("Prima di pivottare")
		
		.ToPivotTablePrivate(
			x => x.StreamID,
			x => x.ChannelCd,
			x => {
				var sd = x.FirstOrDefault();
				if(sd != null)
					if(sd.ChannelCd == "StartDate" || sd.ChannelCd == "EndDate")
						return (new DateTime(Convert.ToInt64(sd.Value.Value))).ToString();
					else if(sd.Value.HasValue)
						return sd.Value.ToString();
					else
						return sd.Label;
				else
					return null;
			})
		.Dump(string.Format("BDE List for {0} (Dates {1} - {2})", unit, dtStart.ToShortDateString(), dtEnd.AddDays(-1).ToShortDateString()));
	}
}

static class MyExt
{
	public static DataTable ToPivotTablePrivate<TSource, TFirstKey, TSecondKey, TValue>(this IEnumerable<TSource> source, Func<TSource, TFirstKey> firstKeySelector, Func<TSource, TSecondKey> secondKeySelector, Func<IEnumerable<TSource>, TValue> aggregate)
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
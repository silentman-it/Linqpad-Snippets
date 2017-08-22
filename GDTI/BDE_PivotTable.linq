<Query Kind="Program">
  <Connection>
    <ID>19bf2bdc-4e48-4a27-8303-e8bba9a3a113</ID>
    <Persist>true</Persist>
    <Driver Assembly="IQDriver" PublicKeyToken="5b59726538a49684">IQDriver.IQDriver</Driver>
    <Provider>Devart.Data.Oracle</Provider>
    <CustomCxString>AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAAg4VPWvAGA0K8d2W/MBVMggAAAAACAAAAAAAQZgAAAAEAACAAAAB5+dg7ce7N2OIKjlzuCxIsiB1ZOSCk+f162xYyPGi/ZQAAAAAOgAAAAAIAACAAAAATVeksXKZ2T7nLZGEMcpDEQ2FHHJUiLk4DZStm+NVyX0AAAACn/0arCv5bzP1sx9wMecLmK7Uvij2GCfzcgrrljCWD2NCUBLtLcgtrTl76nPkyvAKNPCZlBkhglrCIj5UvIbPSQAAAAPKuKL8JhSU93kGq1GdzT/8aGb5JFPmNqphjKLUun56xu5ZL18soi2X5fqsMrntRXJ3mRdsSTDXDad5f221TOPM=</CustomCxString>
    <Server>svil4</Server>
    <EncryptCustomCxString>true</EncryptCustomCxString>
    <DisplayName>svil4.ed_warehouse</DisplayName>
    <UserName>ed_warehouse</UserName>
    <Password>AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAAg4VPWvAGA0K8d2W/MBVMggAAAAACAAAAAAAQZgAAAAEAACAAAAC7h7/cZm/4xnKUUbteeWDXgzflTLeUAzNSBQtjf8V/uQAAAAAOgAAAAAIAACAAAADF9uIiwH1UV8MNRpaGxUPdRHZA6UsGW5DNmlDavj1uMBAAAADiD4sb59R+MLfU7VwLb0LCQAAAAIoRGCSOOeTtpYxXn1zNUoaUTuhYm6qr3+F7qX/pPvPFJlTTm9AfWYY4WsiCBOSLdti+xPbsgrVifrKI3CzwpjA=</Password>
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
		new Tuple<string, string, string>( "UP_TURBIGO_4",    "14/02/2017", null ),
		
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
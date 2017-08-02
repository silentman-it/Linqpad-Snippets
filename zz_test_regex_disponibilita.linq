<Query Kind="Statements">
  <Namespace>System.Globalization</Namespace>
</Query>

// SET_DATE_yyyymmdd_FROM_hhmm_TO_hhmm
// SET_DATE_yyyymmdd_TO_hhmm
// SET_DATE_yyyymmdd_FROM_hhmm

// DELETE_DATE_yyyymmdd

// COPY_yyyymmdd_yyyymmdd_TO_yyyymmdd_yyyymmdd
// COPY_yyyymmdd_TO_yyyymmdd_yyyymmdd

string s = "SET_DATE_20130127_TO_2013";

string regex = "^SET_DATE_([0-9]{8})$";
if(Regex.IsMatch(s, regex))
{
	var token = Regex.Split(s, regex).Where(x => !string.IsNullOrEmpty(x)).ToList().Dump("SET_DATE_FULL");
	var date = DateTime.ParseExact(token[0], "yyyyMMdd", null, DateTimeStyles.None);
}

regex = "^SET_DATE_([0-9]{8})_FROM_([0-9]{4})_TO_([0-9]{4})$";
if(Regex.IsMatch(s, regex))
{
	var token = Regex.Split(s, regex).Where(x => !string.IsNullOrEmpty(x)).ToList().Dump("SET_DATE_RANGE");
	var date = DateTime.ParseExact(token[0], "yyyyMMdd", null, DateTimeStyles.None);
	var hfrom = TimeSpan.ParseExact(token[1], "hhmm", null);
	var hto = TimeSpan.ParseExact(token[2], "hhmm", null);

}

regex = "^SET_DATE_([0-9]{8})_FROM_([0-9]{4})$";
if(Regex.IsMatch(s, regex))
{
	var token = Regex.Split(s, regex).Where(x => !string.IsNullOrEmpty(x)).ToList().Dump("SET_DATE_FROM");
	var date = DateTime.ParseExact(token[0], "yyyyMMdd", null, DateTimeStyles.None);
	var hfrom = TimeSpan.ParseExact(token[1], "hhmm", null);
}

regex = "^SET_DATE_([0-9]{8})_TO_([0-9]{4})$";
if(Regex.IsMatch(s, regex))
{
	var token = Regex.Split(s, regex).Where(x => !string.IsNullOrEmpty(x)).ToList().Dump("SET_DATE_TO");
	var date = DateTime.ParseExact(token[0], "yyyyMMdd", null, DateTimeStyles.None);
	var hto = TimeSpan.ParseExact(token[1], "hhmm", null);
}

regex = "^DELETE_DATE_([0-9]{8})$";
if(Regex.IsMatch(s, regex))
{
	var token = Regex.Split(s, regex).Where(x => !string.IsNullOrEmpty(x)).ToList().Dump("DELETE_DATE");
	var date = DateTime.ParseExact(token[0], "yyyyMMdd", null, DateTimeStyles.None);
}

regex = "^COPY_([0-9]{8})_([0-9]{8})_TO_([0-9]{8})_([0-9]{8})$";
if(Regex.IsMatch(s, regex))
{
	var token = Regex.Split(s, regex).Where(x => !string.IsNullOrEmpty(x)).ToList().Dump("COPY_RANGE_TO_RANGE");
	var date1 = DateTime.ParseExact(token[0], "yyyyMMdd", null, DateTimeStyles.None);
	var date2 = DateTime.ParseExact(token[1], "yyyyMMdd", null, DateTimeStyles.None);
	var date3 = DateTime.ParseExact(token[2], "yyyyMMdd", null, DateTimeStyles.None);
	var date4 = DateTime.ParseExact(token[3], "yyyyMMdd", null, DateTimeStyles.None);
	
}

regex = "^COPY_([0-9]{8})_TO_([0-9]{8})_([0-9]{8})$";
if(Regex.IsMatch(s, regex))
{
	var token = Regex.Split(s, regex).Where(x => !string.IsNullOrEmpty(x)).ToList().Dump("COPY_DATE_TO_RANGE");
	var date1 = DateTime.ParseExact(token[0], "yyyyMMdd", null, DateTimeStyles.None);
	var date2 = DateTime.ParseExact(token[1], "yyyyMMdd", null, DateTimeStyles.None);
	var date3 = DateTime.ParseExact(token[2], "yyyyMMdd", null, DateTimeStyles.None);
}

<Query Kind="Program">
  <Namespace>System.Globalization</Namespace>
</Query>

void Main()
{
	TimeZoneInfo.GetSystemTimeZones()
		.Select(tz => new
		{
			TZ = tz.StandardName,
			Offset = tz.BaseUtcOffset,
			Now = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, tz),
			TransitionDates = GetDateTimes(2017, tz) }).Dump();
	//GetDateTimes(2016, TimeZoneInfo.Local).Dump();
}

// Define other methods and classes here
private IEnumerable<DateTime> GetDateTimes(int Year, TimeZoneInfo tzi, string which = null)
{
	var adjRules = tzi.GetAdjustmentRules().Where(x => x.DateStart.Year <= Year && x.DateEnd.Year >= Year);
	foreach(var ar  in adjRules)
	{
		if(which == null || which.ToLower() == "start")
			yield return GetTransitionDate(Year, ar.DaylightTransitionStart);
		if(which == null || which.ToLower() == "end")
			yield return GetTransitionDate(Year, ar.DaylightTransitionEnd);
	}
}

private DateTime GetTransitionDate(int year, TimeZoneInfo.TransitionTime transition)
{
	if (transition.IsFixedDateRule)
		return new DateTime(year, transition.Month, transition.Day, transition.TimeOfDay.Hour, transition.TimeOfDay.Minute, transition.TimeOfDay.Second);	
	
	Calendar cal = CultureInfo.CurrentCulture.Calendar;
	int startOfWeek = transition.Week * 7 - 6;
	int firstDayOfWeek = (int) cal.GetDayOfWeek(new DateTime(year, transition.Month, 1)); 
	int transitionDay; 
	int changeDayOfWeek = (int) transition.DayOfWeek;
	if (firstDayOfWeek <= changeDayOfWeek)
		transitionDay = startOfWeek + (changeDayOfWeek - firstDayOfWeek);
	else     
		transitionDay = startOfWeek + (7 - firstDayOfWeek + changeDayOfWeek);
	
	// Adjust for months with no fifth week 
	if (transitionDay > cal.GetDaysInMonth(year, transition.Month))  
		transitionDay -= 7;

	return new DateTime(year, transition.Month, transitionDay, transition.TimeOfDay.Hour, transition.TimeOfDay.Minute, transition.TimeOfDay.Second);
}
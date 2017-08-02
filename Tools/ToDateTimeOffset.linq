<Query Kind="Program" />

void Main()
{
	//var baseDate = DateTime.Parse("27/03/2016");
	var baseDate = DateTime.Parse("30/10/2016");
	
	//var amb = DateTime.Parse("25/10/2015 02:32").ToDateTimeOffset().OrderBy(x => x).Dump();
	
	var ls = Enumerable.Range(0, 96)
		.SelectMany(x => baseDate.AddMinutes(x * 15).ToDateTimeOffset())
		.Select(x => new {
			DTO = x,
			DT = x.DateTime,
			Offset = x.GetMorandoOffset(),
			BackToDTO = x.DateTime.GetDateTimeOffset(x.GetMorandoOffset())
		})
		.OrderBy(x => x.DTO)
		.Dump();
}

// Define other methods and classes here
public static class MyExt
{
	public static IEnumerable<DateTimeOffset> ToDateTimeOffset(this DateTime dt)
	{
		DateTimeOffset dto = new DateTimeOffset(dt);

		var isAmbiguous = TimeZoneInfo.Local.IsAmbiguousTime(dt);
		var isInvalid = TimeZoneInfo.Local.IsInvalidTime(dt);
		
		if(!isInvalid)
		{
			yield return dto;
		}
		if(isAmbiguous && TimeZoneInfo.Local.SupportsDaylightSavingTime)
		{
			var offset = TimeZoneInfo.Local.GetAdjustmentRules().FirstOrDefault().DaylightDelta;
			
			yield return new DateTimeOffset(dto.DateTime, dto.Offset.Add(offset));
		}
	}
	
	public static bool IsDaylightSavingTime(this DateTimeOffset dto)
	{
		return TimeZoneInfo.Local.IsDaylightSavingTime(dto);
	}

	public static int GetDailyHours(this DateTime dt)
	{
		if(dt.IsTransitionDate())
		{
			var start = dt.Date;
			var end = start.AddDays(1);
			return TimeZoneInfo.Local.IsDaylightSavingTime(start) && !TimeZoneInfo.Local.IsDaylightSavingTime(end) ? 25 : 23;
		}
		return 24;
	}
	
	public static int GetDailyHours(this DateTimeOffset dto)
	{
		if(dto.IsTransitionDate())
		{
			var start = dto.Date;
			var end = start.AddDays(1);
			return TimeZoneInfo.Local.IsDaylightSavingTime(start) && !TimeZoneInfo.Local.IsDaylightSavingTime(end) ? 25 : 23;
		}
		return 24;
	}

	public static bool IsTransitionDate(this DateTime d)
	{
		d = d.Date;
		return TimeZoneInfo.Local.IsDaylightSavingTime(d) != TimeZoneInfo.Local.IsDaylightSavingTime(d.AddDays(1).AddSeconds(-1));
	}
	
	public static bool IsTransitionDate(this DateTimeOffset d)
	{
		d = d.Date;
		return TimeZoneInfo.Local.IsDaylightSavingTime(d) != TimeZoneInfo.Local.IsDaylightSavingTime(d.AddDays(1).AddSeconds(-1));
	}
	
	public static int GetMorandoOffset(this DateTimeOffset dto)
	{
		return 
			dto.GetDailyHours() == 24                                ? 0 :
			dto.GetDailyHours() == 23 && !dto.IsDaylightSavingTime() ? -1 :
			dto.GetDailyHours() == 23 && dto.IsDaylightSavingTime()  ? 0  :
			dto.GetDailyHours() == 25 && dto.IsDaylightSavingTime()  ? 1  :
			dto.GetDailyHours() == 25 && !dto.IsDaylightSavingTime() ? 0  :
			0;
	}
	
	public static DateTimeOffset GetDateTimeOffset(this DateTime dt, int offset)
	{
		var tzi = TimeZoneInfo.Local;
		var baseUtcOffset = tzi.BaseUtcOffset;
		var adjustmentRule = tzi.GetAdjustmentRules().FirstOrDefault();
		var dstUtcOffset = baseUtcOffset + adjustmentRule.DaylightDelta;
		
		var tsOffset =
			dt.GetDailyHours() == 24                 ? baseUtcOffset :
			dt.GetDailyHours() == 23 && offset == -1 ? baseUtcOffset :
			dt.GetDailyHours() == 23 && offset == 0  ? dstUtcOffset  :
			dt.GetDailyHours() == 25 && offset == 1  ? dstUtcOffset  :
			dt.GetDailyHours() == 25 && offset == 0  ? baseUtcOffset :
			baseUtcOffset;

		return new DateTimeOffset(dt, tsOffset);
	}
	

}
<Query Kind="Program">
  <Reference>C:\Work\TMS\DEV\TMS\Common\TMS.Common\bin\x64\Debug\TMS.Common.dll</Reference>
  <Namespace>System.Globalization</Namespace>
  <Namespace>TMS.Common.Extensions</Namespace>
  <Namespace>TMS.Common.Utils</Namespace>
</Query>

void Main()
{
	DateTime[] dateTypes = {
		DateTime.Parse("26/03/2017"), // 23
		DateTime.Parse("29/10/2017"), // 25
		DateTime.Parse("21/01/2017")  // 24
	};
	
	foreach (var baseDate in dateTypes)
	{
		baseDate.GetHourCount().Dump("Now testing, NH = ");
		
		baseDate.IsTransitionDate().Dump("IsTransitionDate() ?");

		var d1 = baseDate.GetAllQuarterHours();
		
		var checkData = d1.Select(x => new
		{
			DTO1 = x,
			ID = x.GetQuarterIndex(),
			DTO2 = baseDate.GetDateTimeOffsetFromQuarterIndex(x.GetQuarterIndex()),
			Old_IntervalType = DateTimeUtility.DateTime2IntervalType(baseDate, x.GetQuarterIndex(), TMS.Common.Types.Enums.FrequencyType.FifteenMinutes),
			MorandoOffset = x.GetMorandoOffset(),
			IsDST = x.IsDaylightSavingTime(),
			DTO3 = x.DateTime.GetDateTimeOffsetFromMorandoOffset(x.GetMorandoOffset())
		})
		.Select(x => new
		{
			x.DTO1,
			x.ID,
			x.DTO2,
			Old_IntervalType = x.Old_IntervalType.ToString(),
			x.MorandoOffset,
			x.IsDST,
			x.DTO3,
			Check = x.DTO1 == x.DTO2 && x.DTO1 == x.DTO3 && x.Old_IntervalType.ValidityStartDateOffset == x.MorandoOffset
		})
		.Dump()
		;
	
		// TESTS
		
		checkData.All(x => x.Check).Dump("CONVERT-CONVERTBACK TEST OK ?");
		
		checkData
			.Select(x => x.ID)
			.Distinct()
			.Zip(Enumerable.Range(1, baseDate.GetHourCount()), (a,b) => new { a, b })
			.All(x => x.a == x.b)
			.Dump("DISTINCT INDICES TEST OK ?");
		
	}

	
	
}
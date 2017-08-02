<Query Kind="Statements" />

DateTime dto = new DateTime(2016,10,30,2,0,0).Dump();
//DateTimeOffset dto = new DateTimeOffset(2014,10,26,0,30,0, TimeSpan.FromHours(1)).Dump();

var tzi = TimeZoneInfo.Local;
//var tzi = TimeZoneInfo.FindSystemTimeZoneById("Alaskan Standard Time");

//tzi.BaseUtcOffset.Dump();
//tzi.GetAdjustmentRules().Select(x => x.DaylightDelta).Dump();


tzi.GetAdjustmentRules().Dump();
tzi.IsAmbiguousTime(dto).Dump();
tzi.IsInvalidTime(dto).Dump();

tzi.IsDaylightSavingTime(DateTime.Now).Dump("Now is DST?");

tzi.GetAmbiguousTimeOffsets(dto).Dump();
<Query Kind="Statements">
  <NuGetReference>CacheManager.SystemRuntimeCaching</NuGetReference>
  <Namespace>CacheManager.Core</Namespace>
  <Namespace>CacheManager.Core.Internal</Namespace>
</Query>

var cache = CacheFactory.Build<DateTime>("myCacheName", settings =>
{
	settings
		.WithSystemRuntimeCacheHandle("handle1")
		.WithExpiration(ExpirationMode.Sliding, TimeSpan.FromSeconds(10));

		
});

cache.OnRemoveByHandle += (s,e) => {  e.Dump(); };

cache.Add("Federico", DateTime.Parse("21/01/1975"), "BD");
cache.Add("Federico", DateTime.Parse("21/01/1999"), "BD1");



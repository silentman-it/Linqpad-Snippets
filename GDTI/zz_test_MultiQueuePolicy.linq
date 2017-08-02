<Query Kind="Program" />

void Main()
{
	int QueuesPerPriorityPool = 1;
	int MaxPriority = 5;
	
	List<RoundRobinPolicyManager> pools = new List<RoundRobinPolicyManager>();
	
	pools.Add(new RoundRobinPolicyManager(0,2));
	pools.Add(new RoundRobinPolicyManager(1,3));
	pools.Add(new RoundRobinPolicyManager(2,4));
	pools.Add(new RoundRobinPolicyManager(3,5));
	
	pools[0].GetNextResult();
	pools[0].GetNextResult();
	pools[0].GetNextResult();
	pools[1].GetNextResult();
	pools[1].GetNextResult();
	pools[1].GetNextResult();
	pools[1].GetNextResult();
	pools[1].GetNextResult();
	pools[2].GetNextResult();
	pools[2].GetNextResult();
	pools[2].GetNextResult();
	pools[2].GetNextResult();
	pools[2].GetNextResult();
	pools[2].GetNextResult();
	pools[2].GetNextResult();
	pools[3].GetNextResult();
	pools[3].GetNextResult();
	pools[3].GetNextResult();
	pools[3].GetNextResult();
	pools[3].GetNextResult();
	pools[3].GetNextResult();
	pools[3].GetNextResult();
	pools[3].GetNextResult();
	pools[3].GetNextResult();
	pools[3].GetNextResult();
	pools[3].GetNextResult();

	
	pools.Dump();
}

// Define other methods and classes here
class RoundRobinPolicyManager
{
	int MaxPoolSize = 10;
	int PoolSize = 1;
	int NextResult = 0;
	int PoolId = -1;
	public RoundRobinPolicyManager(int poolId, int poolSize)
	{
		PoolId = poolId;
		PoolSize = poolSize;
	}

	public int GetNextResult()
	{
		if(PoolSize <= 0) return 0;
		
		int result = NextResult;
		NextResult = (NextResult + 1) % PoolSize;
		result = PoolId * MaxPoolSize + result;
		result.Dump("New value for pool #" + PoolId);
		return result;
	}
	
	public override string ToString()
	{
		return string.Format("Pool #{0} = {1} of {2}", PoolId, NextResult, PoolSize);
	}
}

<Query Kind="Program" />

void Main()
{
	
}

// From: http://omegacoder.com/?p=1032
public static class SequenceExtensions
{
    /// <summary>
    /// Take a sequence of numbers and if there are any gaps greater than 1 between the numbers,
    /// report true.
    /// </summary>
    /// <param name="sequence">A set of numbers to check.</param>
    /// <returns>True if the there is a break in the sequence of numbers.</returns>
    public static bool IsSequenceBroken(this IEnumerable<int> sequence)
    {
        bool broken = false;
 
        if (sequence != null)
        {
            var sequenceAsList = sequence.ToList();
 
            if (sequenceAsList.Any())
            {
                int lastValue = sequence.First();
 
                broken = sequence.Any(value =>
                                        {
                                            if ((value - lastValue) > 1)
                                                return true;
 
                                            lastValue = value;
 
                                            return false;
                                        });
            }
        }
 
        return broken;
    }
 
    /// <summary>
    /// Take a sequence of numbers and report the missing numbers. Stop at first break found.
    /// </summary>
    /// <param name="sequence">Set of Numbers</param>
    /// <returns>True of sequence has missing numbers</returns>
    public static IEnumerable<int> SequenceFindMissings(this IList<int> sequence)
    {
 
        var missing = new List<int>();
 
        if ((sequence != null) && (sequence.Any()))
        {
            sequence.Aggregate((seed, aggr) =>
                                {
                                    var diff = (aggr - seed) - 1;
 
                                    if (diff > 0)
                                        missing.AddRange(Enumerable.Range((aggr - diff), diff));
 
                                    return aggr;
                                });
        }
 
        return missing;
 
    }
 
    /// <summary>
    /// A missing break start in a sequence is where the drop off occurs in the sequence.
    /// For example 3, 5, has a missing break start of the #3 for #4 is the missing.
    /// </summary>
    /// <param name="sequence">Set of Numbers</param>
    /// <returns>The list of break numbers which exist before the missing numbers.</returns>
    public static IEnumerable<int> SequenceReportMissingsBreakStarts(this IList<int> sequence)
    {
 
        var breaks = new List<int>();
 
        if ((sequence != null) && (sequence.Any()))
        {
 
            sequence.Aggregate((seed, aggr) =>
                                {
                                    var diff = (aggr - seed) - 1;
 
                                    if (diff > 0)
                                        breaks.Add(seed);
                                    return aggr;
                                });
        }
 
        return breaks;
 
    }
}
namespace Primes;

public static class Prime
{
    public static IEnumerable<int> FactorsOf(int number)
    {
        for (var factorCandidate = 2; number > 1; ++factorCandidate)
        {
            while (number % factorCandidate == 0)
            {
                yield return factorCandidate;
                number /= factorCandidate;
            }
        }
    }
}
using FluentAssertions;

namespace Primes.Tests;

public static class AssertionExtensions
{
    public static void Are<TValue>(this IEnumerable<TValue> actual, IEnumerable<TValue> expected)
    {
        actual.Should().BeEquivalentTo(expected);
    }
}
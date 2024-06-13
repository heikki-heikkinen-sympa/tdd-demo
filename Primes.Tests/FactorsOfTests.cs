using FluentAssertions;
using Xunit;

namespace Primes.Tests;


public class FactorsOfTests
{
    [Fact]
    public void FactorsOf_1_is_empty()
    {
        Prime.FactorsOf(1).Should().BeEmpty();
    }

    [Fact]
    public void FactorsOf_4_are_2_and_2()
    {
        Prime.FactorsOf(4).Should().BeEquivalentTo([2, 2]);
    }

    [Theory]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(5)]
    [InlineData(7)]
    [InlineData(int.MaxValue)]
    public void FactorsOf_a_prime_number_is_the_prime_number_itself(int primeNumber)
    {
        Prime.FactorsOf(primeNumber).Should().BeEquivalentTo([primeNumber]);
    }

    [Fact]
    public void FactorsOf_6_are_2_and_3()
    {
        Prime.FactorsOf(6).Should().BeEquivalentTo([2, 3]);
    }

    [Fact]
    public void FactorsOf_8_are_2_and_2_and_2()
    {
        Prime.FactorsOf(8).Should().BeEquivalentTo([2, 2, 2]);
    }

    [Fact]
    public void FactorsOf_9_are_3_and_3_and_3()
    {
        Prime.FactorsOf(9).Should().BeEquivalentTo([3, 3]);
    }

    [Fact]
    public void FactorsOf_a_product_of_prime_numbers_are_the_prime_numbers_themselves()
    {
        Prime.FactorsOf(2 * 2 * 3 * 5 * 7 * 11 * 11 * 13).Are([2, 2, 3, 5, 7, 11, 11, 13]);
    }
}
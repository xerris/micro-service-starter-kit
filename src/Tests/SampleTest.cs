using FluentAssertions;

namespace Skillz.Tests;

public class SampleTest
{
    [Fact]
    public void Math()
        => (10 * 10).Should().Be(100);
}
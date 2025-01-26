namespace CacheApp.UtilsTests.GOFPatternsTests.BuilderTests;

using System;
using CacheApp.Utils.GOFPatterns.Builder;
using Xunit;

public class BuilderByMethodChainingTests
{
    [Fact]
    public void ItCreatesObject()
    {
        // arrange

        // act
        var product = ProductV2
            .InitializeProduct(Guid.NewGuid())
            .ValidatePropertyA("hello")
            .SetPropertyA("hello")
            .SetPropertyB(42)
            .SetPropertyC(42M);

        // assert
        Assert.Equal("hello", product.PropertyA);
        Assert.Equal(42, product.PropertyB);
        Assert.Equal(42M, product.PropertyC);
    }
}

using System;
using CacheApp.Utils.GOFPatterns.Builder;
using Xunit;

namespace CacheApp.Utils.Tests;

public class ByilderByMethodChainingTests
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
        Assert.Equal(product.PropertyA, "hello");
        Assert.Equal(product.PropertyB, 42);
        Assert.Equal(product.PropertyC, 42M);
    }
}

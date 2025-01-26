using System;

namespace CacheApp.Utils.GOFPatterns.Builder;

public class BuilderByMethodChaining { }

public record ProductV2
{
    public Guid Id { get; }
    public string? PropertyA { get; private set; }
    public int PropertyB { get; private set; }
    public decimal PropertyC { get; private set; }

    private ProductV2(Guid id)
    {
        Id = id;
    }

    public static ProductV2 InitializeProduct(Guid id)
    {
        return new ProductV2(id);
        // some other initialization
    }

    public ProductV2 ValidatePropertyA(string propertyA)
    {
        //some validation
        return this;
    }

    public ProductV2 SetPropertyA(string propertyA)
    {
        return this with { PropertyA = propertyA };
    }

    private void ValidatePropertyB(int propertyB)
    {
        //some validation logic;
    }

    public ProductV2 SetPropertyB(int propertyB)
    {
        ValidatePropertyB(propertyB);
        return this with { PropertyB = propertyB };
    }

    private void ValidatePropertyC(decimal propertyC)
    {
        //some validation logic indeed
    }

    public ProductV2 SetPropertyC(decimal propertyC)
    {
        return this with { PropertyC = propertyC };
    }
}

public class Class
{
    public static void Main(params string[] p)
    {

    }
}

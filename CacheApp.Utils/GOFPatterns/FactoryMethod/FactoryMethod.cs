namespace CacheApp.Utils.GOFPatterns.FactoryMethod;

// applicability
// 1. when you don't know beforehand the exact types and dependencies of the object your code should work with
// 2. when you want to provide a way to extend components
// 3. save system resources by reusing existing objects instead of rebuilding them each time (singleton?)

public class FactoryMethod
{
    // move general items into interface
    public interface IProduct
    {
        string Name { get; }
    }

    // create concrete realization
    public class ProductA : IProduct
    {
        public string Name => "Product A";

        // some other functionality
    }

    // abstract creator that will have also some additional general logic
    public interface ICreator<T>
        where T : IProduct
    {
        T CreateProduct();
    }

    // concrete creator also with some specific for this type logic
    public class CreatorA : ICreator<ProductA>
    {
        public ProductA CreateProduct() => new();
    }
}

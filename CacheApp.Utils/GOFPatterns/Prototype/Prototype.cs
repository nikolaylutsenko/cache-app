namespace CacheApp.Utils.GOFPatterns.Prototype;

// moves logic of cloning inside the object
// so objects create copies of themselves

// or maybe used instead of inheritance for creation of objects family

public interface IPrototype<T>
{
    T Clone();
}

public class ProductA(string Category) : IPrototype<ProductA>
{
    public ProductA Clone()
    {
        return new ProductA(Category);
    }
}

public class ProductB(string Name, decimal Price, string Category) : ProductA(Category)
{
    public new ProductB Clone()
    {
        return new ProductB(Name, Price, Category);
    }
}

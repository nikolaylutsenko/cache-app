namespace CacheApp.Utils.GOFPatterns.AbstractFactory;

// advanced factory method, works well with family of related products

public class AbstractFactory
{
    public interface IProductA
    {
        string Name { get; }
    }

    public interface IProductB
    {
        string Name { get; }
    }

    public class ProductA1 : IProductA
    {
        public string Name => "Product A 1";

        // some other functionality
    }

    // create concrete realization
    public class ProductB1 : IProductB
    {
        public string Name => "Product B 1";

        // some other functionality
    }

    public class ProductA2 : IProductA
    {
        public string Name => "Product A 2";

        // some other functionality
    }

    // create concrete realization
    public class ProductB2 : IProductB
    {
        public string Name => "Product B 2";

        // some other functionality
    }

    public interface ICreator
    {
        IProductA CreateProductA();
        IProductB CreateProductB();
    }

    public class CreatorA1 : ICreator
    {
        public IProductA CreateProductA()
        {
            return new ProductA1();
        }

        public IProductB CreateProductB()
        {
            return new ProductB1();
        }
    }

    public class CreatorA2 : ICreator
    {
        public IProductA CreateProductA()
        {
            return new ProductA1();
        }

        public IProductB CreateProductB()
        {
            return new ProductB1();
        }
    }
}

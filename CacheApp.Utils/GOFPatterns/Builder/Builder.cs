namespace CacheApp.Utils.GOFPatterns.Builder;

using System;

/*
    construct complex object step by step
    same code for different objects creation
*/

//TODO: ask if we can create only object with some list of para

public interface IBuilder
{
    void BuildPartA();
    void BuildPartB();
    void BuildPartC();
}

// object that has to be created
public class Product
{
    private List<object> _parts = new List<object>();

    public void Add(string part)
    {
        _parts.Add(part);
    }

    public bool IsEmpty()
    {
        return _parts.Count == 0;
    }
}

// manages what to add to product
// returns created product by GetProduct() method and reset product to default
public class ConcreteBuilder
{
    private Product _product = new Product();

    public ConcreteBuilder()
    {
        this.Reset();
    }

    public void Reset()
    {
        _product = new Product();
    }

    public void BuildPartA()
    {
        _product.Add("Part A");
    }

    public void BuildPartB()
    {
        _product.Add("Part B");
    }

    public void BuildPartC()
    {
        _product.Add("Part C");
    }

    public Product GetProduct()
    {
        // probably we need to have some security mechanism
        // to prevent returning of empty Product
        if (_product.IsEmpty())
            throw new Exception("Empty product");

        Product result = _product;
        Reset();
        return result;
    }
}

// manages what object exactly to build
// (can be avoided so then you'll need to call methods by your own)
public class Director
{
    private IBuilder _builder;

    public void BuildMinimal()
    {
        _builder.BuildPartA();
    }

    public void BuildFull()
    {
        _builder.BuildPartA();
        _builder.BuildPartB();
        _builder.BuildPartC();
    }
}

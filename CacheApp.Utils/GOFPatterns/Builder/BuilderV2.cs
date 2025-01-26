using System;

namespace CacheApp.Utils.GOFPatterns.Builder;

/*
* If we have object that can have multiple variants depends on some conditions
* we can (probably) split it's inner structure by functionality and use Builder to
* create what kind of we need right now
*/

public interface IType { }

public interface ITypeA : IType
{
    public string? PropertyA { get; set; }
}

public class TypeA : ITypeA
{
    public string? PropertyA { get; set; }
}

public interface ITypeA2 : ITypeA
{
    public new string? PropertyA { get; set; }
    public string? PropertyA2 { get; set; }
}

public class TypeA2 : ITypeA2
{
    public string? PropertyA { get; set; }
    public string? PropertyA2 { get; set; }
}

public interface ITypeB : IType
{
    public string? PropertyB { get; set; }
}

public class TypeB : ITypeB
{
    public string? PropertyB { get; set; }
}

public interface IBuilderV2
{
    IType BuidlTypeA();
    IType BuildTypeA2();
    IType BuidlTypeB();
}

public class BuilderV2 : IBuilderV2
{
    public IType BuidlTypeA()
    {
        return new TypeA();
    }

    public IType BuidlTypeB()
    {
        return new TypeB();
    }

    public IType BuildTypeA2()
    {
        return new TypeA2();
    }
}

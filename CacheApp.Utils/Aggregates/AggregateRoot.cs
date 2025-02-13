﻿namespace CacheApp.Utils.Aggregates;

public abstract class AggregateRoot<TIdentity> : EntityBase<TIdentity>
    where TIdentity : IdentityBase
{
    protected AggregateRoot(TIdentity id)
        : base(id) { }
}

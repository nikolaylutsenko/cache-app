namespace CacheApp.Utils.ResultPattern;

public sealed record Error(string Code, string? Message = null);

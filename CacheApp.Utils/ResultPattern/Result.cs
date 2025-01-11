namespace CacheApp.Utils.ResultPattern;

public class Result<TValue>
{
    public TValue? Value { get; }
    public Error? Error { get; }

    private bool _isSuccess;

    private Result(TValue value)
    {
        _isSuccess = true;
        Value = value;
        this.Error = default;
    }

    private Result(Error error)
    {
        _isSuccess = false;
        Value = default;
        this.Error = error;
    }

    public static implicit operator Result<TValue>(TValue value) => new(value);

    public static implicit operator Result<TValue>(Error error) => new(error);

    public Result<TValue> Match(
        Func<TValue, Result<TValue>> success,
        Func<Error, Result<TValue>> failure
    )
    {
        if (_isSuccess)
        {
            return success(Value!);
        }
        return failure(this.Error!);
    }
}

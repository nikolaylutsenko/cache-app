using System.Diagnostics.Contracts;

namespace CacheApp.Utils.ResultPattern;

// adjusted according to https://devblogs.microsoft.com/ise/next-level-clean-architecture-boilerplate/
public struct Result<T>
{
    private enum ResultState
    {
        Null,
        Failure,
        Success,
    }

    public T? Value { get; }
    public Error? Error { get; }

    //private bool _isSuccess;
    private readonly ResultState _state;
    public readonly bool IsSuccess => _state == ResultState.Success;
    public readonly bool IsFailure => _state == ResultState.Failure;
    public readonly bool IsNull => _state == ResultState.Null;

    private Result(T value)
    {
        Value = value;
        this.Error = default;
        _state = ResultState.Success;
    }

    private Result(Error error)
    {
        Value = default;
        this.Error = error;
        _state = ResultState.Failure;
    }

    public static implicit operator Result<T>(T? value) =>
        value is not null ? new Result<T>(value) : new Result<T>();

    public static implicit operator Result<T>(Error error) => new(error);

    [Pure] //TODO: check what is Pure
    public TR Match<TR>(
        Func<T, TR> onSuccess,
        Func<Error, TR> onFailure,
        Func<TR>? onNull = null
    ) =>
        IsSuccess ? onSuccess(Value!)
        : IsFailure ? onFailure(Error!)
        : onNull is not null ? onNull()
        : throw new InvalidOperationException(
            "Result is null, but no onNull function was provided."
        );
}

namespace ZooShop.Application.Common;

public readonly struct Result<TValue, TError>
{
    private readonly TValue _value;
    private readonly TError _error;
    private readonly bool _isError;

    public bool IsError => _isError;
    public bool IsSuccess => !_isError;

    private Result(TValue value)
    {
        _value = value ?? throw new ArgumentNullException(nameof(value));
        _error = default!;
        _isError = false;
    }

    private Result(TError error)
    {
        _error = error ?? throw new ArgumentNullException(nameof(error));
        _value = default!;
        _isError = true;
    }

    public static implicit operator Result<TValue, TError>(TValue value) => new(value);
    public static implicit operator Result<TValue, TError>(TError error) => new(error);

    public TResult Match<TResult>(
        Func<TValue, TResult> success,
        Func<TError, TResult> failure) =>
        IsSuccess ? success(_value) : failure(_error);
}

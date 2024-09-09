using FryTech.Essentials.Results.Exceptions;

namespace FryTech.Essentials.Results;

public interface IServiceResult
{
    public string? Message { get; }

    public bool IsSuccessful { get; }

    public IServiceResultType Type { get; }

    public IServiceError? Error { get; }

    /// <summary>
    /// Throws the <see cref="ServiceErrorException"/>> if result is not successful
    /// </summary>
    public void EnsureSuccessful();
}

public interface IServiceResult<out TData> : IServiceResult
{
    public TData Data { get; }
}

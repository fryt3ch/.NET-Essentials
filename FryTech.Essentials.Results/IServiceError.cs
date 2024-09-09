namespace FryTech.Essentials.Results;

public interface IServiceError
{
    string? Message { get; }
}

public interface IServiceError<out TData> : IServiceError
{
    TData? Data { get; }
}

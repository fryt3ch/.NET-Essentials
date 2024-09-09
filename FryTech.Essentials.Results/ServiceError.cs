namespace FryTech.Essentials.Results;

public class ServiceError : IServiceError
{
    public string? Message { get; set; }

    public ServiceError(string? message = null)
    {
        Message = message;
    }
}

public class ServiceError<TData> : ServiceError, IServiceError<TData>
{
    public TData? Data { get; set; }

    public ServiceError(string message, TData? data = default) : base(message)
    {
        Data = data;
    }

    public ServiceError(TData? data = default)
    {
        Data = data;
    }
}

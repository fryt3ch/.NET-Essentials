namespace FryTech.Essentials.Results.Exceptions;

public class ServiceErrorException : Exception
{
    public IServiceResultType? ServiceResultType { get; init; }

    public IServiceError? ServiceError { get; init; }

    public ServiceErrorException()
    {
    }

    public ServiceErrorException(string message)
        : base(message)
    {
    }

    public ServiceErrorException(IServiceError serviceError)
    {
        ServiceError = serviceError;
    }

    public ServiceErrorException(string message, IServiceError serviceError)
        : this(message)
    {
        ServiceError = serviceError;
    }
}

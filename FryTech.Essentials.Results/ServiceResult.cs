using FryTech.Essentials.Results.Exceptions;

namespace FryTech.Essentials.Results;

public class ServiceResult : IServiceResult
{
    public string? Message { get; set; }

    public virtual bool IsSuccessful => Type.IsSuccessful;

    public IServiceResultType Type { get; set; }

    public IServiceError? Error { get; set; }

    public ServiceResult(IServiceResultType type)
    {
        Type = type;
    }

    public virtual ServiceResult WithType(IServiceResultType type)
    {
        Type = type;

        return this;
    }

    public virtual ServiceResult WithMessage(string? message)
    {
        Message = message;

        return this;
    }

    public virtual ServiceResult WithError(IServiceError? error)
    {
        Error = error;

        return this;
    }

    /// <summary>
    /// Присваивает <see cref="Error"/> сущность <see cref="ServiceError"/> с заданным сообщением.
    /// </summary>
    /// <param name="errorMessage">Сообщение</param>
    /// <returns></returns>
    public virtual ServiceResult WithError(string errorMessage)
    {
        Error = new ServiceError(errorMessage);

        return this;
    }

    public virtual ServiceResult Derive(IServiceResult result)
    {
        return WithType(result.Type).WithError(result.Error).WithMessage(result.Message);
    }

    public virtual void EnsureSuccessful()
    {
        if (IsSuccessful)
        {
            return;
        }

        if (Error is null)
        {
            throw new ServiceErrorException();
        }

        throw new ServiceErrorException(Error) { ServiceResultType = Type, };
    }

    /// <summary>
    /// Creates new instance using <see cref="HttpServiceResultType.Success"/> as <see cref="IServiceResult.Type"/>
    /// </summary>
    /// <returns>Successful instance</returns>
    public static ServiceResult CreateSuccessful()
    {
        return new ServiceResult(HttpServiceResultType.Success);
    }

    /// <summary>
    /// Creates new instance using <see cref="HttpServiceResultType.InternalError"/> as <see cref="IServiceResult.Type"/>
    /// </summary>
    /// <returns>Successful instance</returns>
    public static ServiceResult CreateFailed()
    {
        return new ServiceResult(HttpServiceResultType.InternalError);
    }

    public static ServiceResult FromException(Exception exception)
    {
        IServiceResultType? resultType = null;
        IServiceError? error = null;

        if (exception is ServiceErrorException serviceErrorException)
        {
            resultType = serviceErrorException.ServiceResultType;
            error = serviceErrorException.ServiceError;
        }

        if (resultType is null)
        {
            resultType = HttpServiceResultType.InternalError;
        }

        if (error is null)
        {
            error = new ServiceError(exception.Message);
        }

        return new ServiceResult(resultType).WithError(error);
    }
}

public class ServiceResult<TData> : ServiceResult, IServiceResult<TData>
{
    public TData Data { get; set; } = default!;

    public ServiceResult(IServiceResultType type) : base(type)
    {

    }

    public override ServiceResult<TData> WithType(IServiceResultType type)
    {
        base.WithType(type);

        return this;
    }

    public override ServiceResult<TData> WithMessage(string? message)
    {
        base.WithMessage(message);

        return this;
    }

    public override ServiceResult<TData> WithError(IServiceError? error)
    {
        base.WithError(error);

        return this;
    }

    public override ServiceResult<TData> WithError(string errorMessage)
    {
        base.WithError(errorMessage);

        return this;
    }

    public override ServiceResult<TData> Derive(IServiceResult result)
    {
        base.Derive(result);

        if (result is IServiceResult<TData> resultWithData)
        {
            WithData(resultWithData.Data);
        }

        return this;
    }

    public ServiceResult<TData> WithData(TData data)
    {
        Data = data;

        return this;
    }

    /// <inheritdoc cref="ServiceResult.CreateSuccessful"/>
    public static new ServiceResult<TData> CreateSuccessful()
    {
        return new ServiceResult<TData>(HttpServiceResultType.Success);
    }

    /// <inheritdoc cref="ServiceResult.CreateFailed"/>
    public static new ServiceResult<TData> CreateFailed()
    {
        return new ServiceResult<TData>(HttpServiceResultType.InternalError);
    }
}

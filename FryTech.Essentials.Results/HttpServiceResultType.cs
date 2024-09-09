namespace FryTech.Essentials.Results;

public class HttpServiceResultType : IServiceResultType
{
    /// <summary>
    /// Represents 200 - OK
    /// </summary>
    public static IServiceResultType Success { get; } = new HttpServiceResultType(true, 200);

    /// <summary>
    /// Represents 201 - Created
    /// </summary>
    public static IServiceResultType Created { get; } = new HttpServiceResultType(true, 201);

    /// <summary>
    /// Represents 404 - NotFound
    /// </summary>
    public static IServiceResultType NotFound { get; } = new HttpServiceResultType(false, 404);

    /// <summary>
    /// Represents 400 - BadRequest
    /// </summary>
    public static IServiceResultType BadRequest { get; } = new HttpServiceResultType(false, 400);

    /// <summary>
    /// Represents 500 - InternalServerError
    /// </summary>
    public static IServiceResultType InternalError { get; } = new HttpServiceResultType(false, 500);
    
    public bool IsSuccessful { get; }

    public int HttpStatusCode { get; }

    public HttpServiceResultType(bool isSuccessful, int httpStatusCode)
    {
        IsSuccessful = isSuccessful;
        HttpStatusCode = httpStatusCode;
    }
}

using System.Net;

namespace TodoApi.Commons.Models;

public interface IApiResponse<out T>
{
    public string Message { get; }
    public int Code { get; }
    public string SubCode { get; }
    public T? Data { get; }
    public IEnumerable<ErrorResponse>? Errors { get; }
    public static T? Default { get; } = default;
}

public sealed record ApiResponse<T>(
    string Message,
    int Code,
    T? Data = default,
    string SubCode = "0",
    IEnumerable<ErrorResponse>? Errors = default) : IApiResponse<T>
{
}

public sealed record ErrorResponse(string Field, string ErrorMessage);

public static class ApiResponseExtensions
{
    public static IApiResponse<T> ToOkApiResponse<T>(this T data, string message = "Success", string subCode = "0")
        => ToApiResponse(data, message, (int)HttpStatusCode.OK, subCode);

    public static IApiResponse<T> ToNotFoundApiResponse<T>(this T? data, string message = "Not Found",
        string subCode = "0")
        => ToApiResponse<T>(default, message, (int)HttpStatusCode.NotFound, subCode);

    public static IApiResponse<T> ToUnAuthorizedApiResponse<T>(this T? data, string message = "UnAuthorized",
        string subCode = "0")
        => ToApiResponse<T>(default, message, (int)HttpStatusCode.Unauthorized, subCode);

    public static IApiResponse<T> ToForbiddenApiResponse<T>(this T? data, string message = "Forbidden",
        string subCode = "0")
        => ToApiResponse<T>(default, message, (int)HttpStatusCode.Forbidden, subCode);

    public static IApiResponse<T> ToFailedDependencyApiResponse<T>(this T? data, string message = "Failed Dependency",
        string subCode = "0", IEnumerable<ErrorResponse>? errors = null)
        => ToApiResponse<T>(default, message, (int)HttpStatusCode.FailedDependency, subCode);

    public static IApiResponse<T> ToServerErrorApiResponse<T>(this T? data, string message = "Internal Server Error",
        string subCode = "0", IEnumerable<ErrorResponse>? errors = null)
        => ToApiResponse<T>(default, message, (int)HttpStatusCode.InternalServerError, subCode);

    public static IApiResponse<T> ToCreatedApiResponse<T>(this T? data, string message = "Created",
        string subCode = "0")
        => ToApiResponse(data, message, (int)HttpStatusCode.Created, subCode);

    public static IApiResponse<T> ToNoContentApiResponse<T>(this T? data, string message = "No Content",
        string subCode = "0")
        => ToApiResponse<T>(default, message, (int)HttpStatusCode.NoContent, subCode);

    public static IApiResponse<T> ToPaymentRequiredApiResponse<T>(this T? data, string message = "Payment Required",
        string subCode = "0")
        => ToApiResponse<T>(default, message, (int)HttpStatusCode.PaymentRequired, subCode);

    public static IApiResponse<T> ToBadRequestApiResponse<T>(this T? data, string message = "Bad Request",
        string subCode = "0", IEnumerable<ErrorResponse>? errors = null)
        => ToApiResponse<T>(default, message, (int)HttpStatusCode.BadRequest, subCode, errors);

    public static IApiResponse<T> ToApiResponse<T>(this T? data, string message, int statusCode, string subCode = "0",
        IEnumerable<ErrorResponse>? errors = null)
        => new ApiResponse<T>(message, statusCode, data, subCode, errors);
}
using System;

namespace API.Errors;

//other way of making constructor: putting props directly in ()
public class ApiException(int statusCode, string message, string? details)
{
    //read only
    public int StatusCode { get; } = statusCode;
    public string Message { get; } = message;
    public string? Details { get; } = details;
}

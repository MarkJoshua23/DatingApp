using System;
using System.Net;
using System.Text.Json;
using API.Errors;
using Microsoft.AspNetCore.Mvc;

namespace API.Middleware;

//middleware responsible for catching exceptions
public class ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
{
    //needs to be named invokeasync
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            //just let the context pass to the next middleware
            await next(context);
        }
        //catch the error from middlewares
        catch (Exception ex)
        {
            //logs in teminal
            logger.LogError(ex, ex.Message);

            //return the error in json response
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var response = env.IsDevelopment()
            //response in dev
            ? new ApiException(context.Response.StatusCode, ex.Message, ex.StackTrace)
            //response in prod
            : new ApiException(context.Response.StatusCode, ex.Message, "Internal Server Error");

            var options = new JsonSerializerOptions{
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            var json = JsonSerializer.Serialize(response, options);
            //send the json of error directly to the client
            await context.Response.WriteAsync(json);
        }
    }
}


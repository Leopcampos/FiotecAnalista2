﻿using FiotecInfodengue.Api.Meddleware.Model;
using FiotecInfodengue.Domain.Exceptions;
using Newtonsoft.Json;
using System.Net;

namespace FiotecInfodengue.Api.Meddleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
        => _next = next;

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (EmailException ex)
        {
            await HandleExceptionAsync(context, ex, HttpStatusCode.BadRequest);
        }
        catch (ApplicationException ex)
        {
            await HandleExceptionAsync(context, ex, HttpStatusCode.BadRequest);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex, HttpStatusCode.InternalServerError);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception, HttpStatusCode statusCode)
    {
        context.Response.StatusCode = (int)statusCode;
        context.Response.ContentType = "application/json";

        var errorMessage = exception.Message;

        var errorResultModel = new ErrorResultModel
        {
            StatusCode = context.Response.StatusCode,
            Message = errorMessage
        };

        var jsonResponse = JsonConvert.SerializeObject(errorResultModel);
        await context.Response.WriteAsync(jsonResponse);
    }
}
using System.Net;
using System.Text.Json;

using FluentValidation;

using Microsoft.AspNetCore.Mvc;

using OrderManagement.Domain.Exceptions;

namespace OrderManagement.Api.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    private readonly ILogger<ExceptionMiddleware> _logger;


    public ExceptionMiddleware(
        RequestDelegate next,
        ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }


    public async Task InvokeAsync(
        HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(
                context,
                ex);
        }
    }


    private async Task HandleExceptionAsync(
        HttpContext context,
        Exception exception)
    {
        var statusCode = HttpStatusCode.InternalServerError;


        var problemDetails = new ProblemDetails
        {
            Instance = context.Request.Path
        };


        switch (exception)
        {
            case ValidationException validationException:

                statusCode =
                    HttpStatusCode.BadRequest;


                problemDetails.Title =
                    "Erro de validação";


                problemDetails.Detail =
                    "Um ou mais campos são inválidos.";


                problemDetails.Extensions["errors"] =
                    validationException.Errors
                        .GroupBy(x => x.PropertyName)
                        .ToDictionary(
                            x => x.Key,
                            x => x.Select(e => e.ErrorMessage)
                        );

                break;



            case DomainException domainException:

                statusCode =
                    HttpStatusCode.BadRequest;


                problemDetails.Title =
                    "Regra de negócio inválida";


                problemDetails.Detail =
                    domainException.Message;

                break;



            case UnauthorizedAccessException unauthorizedException:

                statusCode =
                    HttpStatusCode.Unauthorized;


                problemDetails.Title =
                    "Não autorizado";


                problemDetails.Detail =
                    unauthorizedException.Message;

                break;



            case KeyNotFoundException keyNotFoundException:

                statusCode =
                    HttpStatusCode.NotFound;


                problemDetails.Title =
                    "Recurso não encontrado";


                problemDetails.Detail =
                    keyNotFoundException.Message;

                break;



            default:

                _logger.LogError(
                    exception,
                    "Erro não tratado");


                problemDetails.Title =
                    "Erro interno";


                problemDetails.Detail =
                    "Ocorreu um erro inesperado.";

                break;
        }


        problemDetails.Status =
            (int)statusCode;


        context.Response.ContentType =
            "application/problem+json";


        context.Response.StatusCode =
            (int)statusCode;


        await context.Response.WriteAsync(
            JsonSerializer.Serialize(
                problemDetails));
    }
}
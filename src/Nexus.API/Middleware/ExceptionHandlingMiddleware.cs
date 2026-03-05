using System;
using System.Net;
using System.Text.Json;
using Nexus.API.Models;

namespace Nexus.API.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;
    private readonly IHostEnvironment _env;

    public ExceptionHandlingMiddleware(
        RequestDelegate next,
        ILogger<ExceptionHandlingMiddleware> logger,
        IHostEnvironment env
    )
    {
        _next = next;
        _logger = logger;
        _env = env;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, "Erro não tratado: {Message}", ex.Message);
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        var response = exception switch
        {
            KeyNotFoundException => CreateErrorResponse(
                HttpStatusCode.NotFound,
                "RECURSO_NAO_ENCONTRADO",
                exception.Message
            ),

            UnauthorizedAccessException => CreateErrorResponse(
                HttpStatusCode.Forbidden,
                "ACESSO_NEGADO",
                "Vocẽ não tem permissão para acessar este recurso"
            ),

            ArgumentException => CreateErrorResponse(
                HttpStatusCode.BadRequest,
                "DADOS_INVALIDOS",
                exception.Message
            ),

            InvalidOperationException => CreateErrorResponse(
                HttpStatusCode.BadRequest,
                "OPERACAO_INVALIDA",
                exception.Message),

            _ => CreateErrorResponse(
                HttpStatusCode.InternalServerError,
                "ERRO_INTERNO",
                _env.IsDevelopment() 
                    ? exception.Message 
                    : "Ocorreu um erro interno. Por favor, tente novamente mais tarde.")
        };

        context.Response.StatusCode = (int)response.StatusCode;

        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        var json = JsonSerializer.Serialize(response.Body, options);
        await context.Response.WriteAsync(json);
    }

    private (HttpStatusCode StatusCode, ApiResponse<object> Body) CreateErrorResponse(HttpStatusCode statusCode,string codigo,string mensagem,List<string>? detalhes = null)
    {
        var response = ApiResponse<object>.ComErro(codigo, mensagem, detalhes);
        return (statusCode, response);
    }

    
}

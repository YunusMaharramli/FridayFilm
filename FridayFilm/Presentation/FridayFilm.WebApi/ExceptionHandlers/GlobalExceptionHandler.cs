using FridayFilm.Application.Exceptions;

namespace FridayFilm.WebApi.ExceptionHandlers;

public class GlobalExceptionHandler
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(
        RequestDelegate next,
        ILogger<GlobalExceptionHandler> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (OperationCanceledException) when (context.RequestAborted.IsCancellationRequested)
        {
            // The client disconnected; there is no response left to write.
        }
        catch (Exception exception) when (!context.Response.HasStarted)
        {
            _logger.LogError(
                exception,
                "An error occurred while processing the request.");

            var statusCode = exception switch
            {
                ValidationException =>
                    StatusCodes.Status400BadRequest,

                BadRequestException =>
                    StatusCodes.Status400BadRequest,

                ArgumentException =>
                    StatusCodes.Status400BadRequest,
                Microsoft.AspNetCore.Http.BadHttpRequestException badRequest => badRequest.StatusCode,
                FluentValidation.ValidationException => StatusCodes.Status400BadRequest,
                Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException => StatusCodes.Status409Conflict,

                UnauthorizedException =>
                    StatusCodes.Status401Unauthorized,

                ForbiddenException =>
                    StatusCodes.Status403Forbidden,

                NotFoundException =>
                    StatusCodes.Status404NotFound,

                ConflictException =>
                    StatusCodes.Status409Conflict,
                Microsoft.EntityFrameworkCore.DbUpdateException { InnerException: Npgsql.PostgresException { SqlState: "23505" or "23503" } } =>
                    StatusCodes.Status409Conflict,

                _ =>
                    StatusCodes.Status500InternalServerError
            };

            var message =
                statusCode == StatusCodes.Status500InternalServerError
                    ? "An unexpected server error occurred."
                    : exception is Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException
                    ? "Məlumat başqa sorğu tərəfindən dəyişdirilib. Yenidən cəhd edin."
                    : exception is Microsoft.EntityFrameworkCore.DbUpdateException
                    ? "Məlumat artıq mövcuddur və ya başqa məlumatla əlaqəlidir."
                    : exception.Message;

            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json";

            await context.Response.WriteAsJsonAsync(new
            {
                StatusCode = statusCode,
                Message = message,
                Path = context.Request.Path.Value,
                TraceId = context.TraceIdentifier
            });
        }
    }
}

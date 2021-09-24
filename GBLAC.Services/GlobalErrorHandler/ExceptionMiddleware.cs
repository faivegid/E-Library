using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBLAC.Services.GlobalErrorHandler
{
    public class GlobalHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalHandlerMiddleware> _logger;


        public GlobalHandlerMiddleware(RequestDelegate next, ILogger<GlobalHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (BadHttpRequestException ex)
            {
                _logger.LogError(ex, $"The application encountered a BadRequest");
                await HanddleExceptionAsync(httpContext, ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"The application encountered an unhabdled error");
                await HanddleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HanddleExceptionAsync(HttpContext httpContext, Exception ex)
        {
            httpContext.Response.ContentType = "qpplication/json";
            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await httpContext.Response.WriteAsync(new ExceptionResponse
            {
                StatusCode = httpContext.Response.StatusCode,
                Message = ex.Message
            }.ToString());
        }

        private async Task HanddleExceptionAsync(HttpContext httpContext, BadHttpRequestException ex)
        {
            httpContext.Response.ContentType = "qpplication/json";
            httpContext.Response.StatusCode = ex.StatusCode;
            await httpContext.Response.WriteAsync(new ExceptionResponse
            {
                StatusCode = httpContext.Response.StatusCode,
                Message = ex.Message
            }.ToString());
        }
    }
}

using Microsoft.Extensions.Primitives;

namespace StudentRegistry.Api.Configuration
{
    /// <summary>
    /// Represents a class that handles API key authentication.
    /// </summary>
    public class ApiKeyMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;
        private const string ApiKeyHeader = "X-Api-Key";

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiKeyMiddleware"/> class.
        /// </summary>
        /// <param name="next">The next request delegate.</param>
        /// <param name="configuration">The configuration settings.</param>
        public ApiKeyMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
        }

        /// <summary>
        /// Invokes the middleware.
        /// </summary>
        /// <param name="context">The HTTP context.</param>
        public async Task InvokeAsync(HttpContext context)
        {
            string validApiKey = _configuration["ApiKey"];

            if (!context.Request.Headers.TryGetValue(ApiKeyHeader, out StringValues apiKey) || apiKey != validApiKey)
            {
                context.Response.StatusCode = 401; // Unauthorized
                await context.Response.WriteAsync("Invalid API key");
                return;
            }

            await _next(context);
        }

    }

    /// <summary>
    /// Extension methods for configuring API key middleware.
    /// </summary>
    public static class ApiKeyMiddlewareExtensions
    {
        /// <summary>
        /// Adds API key middleware to the application's request pipeline.
        /// </summary>
        /// <param name="builder">The <see cref="IApplicationBuilder"/> instance.</param>
        /// <param name="configuration">The configuration settings.</param>
        /// <returns>The <see cref="IApplicationBuilder"/> instance.</returns>
        public static IApplicationBuilder UseApiKeyMiddleware(this IApplicationBuilder builder, IConfiguration configuration)
        {
            return builder.UseMiddleware<ApiKeyMiddleware>(configuration);
        }
    }
}

using Serilog;
using Serilog.Formatting.Compact;
using StudentRegistry.Api.Configuration;
using StudentRegistry.Repositories.Data;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.File(
        new RenderedCompactJsonFormatter(),
        "logs-.txt",
        fileSizeLimitBytes: 10 * 1024 * 1024, // 10 MB
        rollOnFileSizeLimit: true,
        retainedFileCountLimit: null, // Retain all log files
        rollingInterval: RollingInterval.Day // Daily rollover
    )
    .CreateLogger();

// Add services to the container.

builder.Services
    .ConfigureApiServices(builder.Configuration)
    .AddBusinessServices()
    .AddDataServices(builder.Configuration);

var app = builder.Build();

// Middleware for logging each consumption of the backend services
app.Use(async (context, next) =>
{
    // Log the date-time and service URL
    Log.Information($"Service URL: {context.Request.Scheme}://{context.Request.Host}{context.Request.Path}");

    // Call the next middleware in the pipeline
    await next();
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Ensure database is created
using (var scope = app.Services.CreateScope())
{
    var rapidPayDbContext = scope.ServiceProvider.GetRequiredService<StudentRegistryDbContext>();
    rapidPayDbContext.Database.EnsureCreated();
}
app.UseHttpsRedirection();

app.UseApiKeyMiddleware(builder.Configuration);

app.UseAuthorization();

app.MapControllers();

app.Run();

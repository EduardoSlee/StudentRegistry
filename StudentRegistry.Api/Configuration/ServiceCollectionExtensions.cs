using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using StudentRegistry.Repositories.Data;
using StudentRegistry.Repositories.Students;
using StudentRegistry.Services.Students;
using StudentRegistry.Services.Students.Mappings;
using System.Reflection;

namespace StudentRegistry.Api.Configuration
{
    /// <summary>
    /// Static class used to add layers of services to the ASP.NET core service collection.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add configuration api services.
        /// </summary>
        /// <param name="services">ASP.NET core service collection.</param>
        /// /// <param name="configuration">Configuration settings.</param>
        /// <returns>ASP.NET core service collection.</returns>
        public static IServiceCollection ConfigureApiServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(config =>
            {
                config.SwaggerDoc("v1", new OpenApiInfo { Title = "Student Registry API", Version = "v1" });
                config.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
                {
                    Description = "API Key Authentication",
                    Name = "X-Api-Key",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });
                config.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "ApiKey"
                            }
                        },
                        new string[] { }
                    }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                config.IncludeXmlComments(xmlPath);
            });

            var corsOrigins = configuration.GetSection("Cors:Origins").Get<string[]>();
            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                    builder =>
                    {
                        builder.WithOrigins(corsOrigins)
                               .AllowAnyHeader()
                               .AllowAnyMethod();
                    });
            });

            return services;
        }

        /// <summary>
        /// Add business layer services.
        /// </summary>
        /// <param name="services">ASP.NET core service collection.</param>
        /// <returns>ASP.NET core service collection.</returns>
        public static IServiceCollection AddBusinessServices(this IServiceCollection services)
        {
            return services
                .AddAutoMapper(typeof(StudentsMappingProfile))
                .AddScoped<IStudentsService, StudentsService>();
        }

        /// <summary>
        /// Add data access services and initialize Db context.
        /// </summary>
        /// <param name="services">ASP.NET core service collection.</param>
        /// <param name="configuration">Configuration settings.</param>
        /// <returns>ASP.NET core service collection.</returns>
        public static IServiceCollection AddDataServices(this IServiceCollection services, IConfiguration configuration)
        {
            var studentRegistryConnectionString = configuration.GetConnectionString("StudentRegistryDb");

            services.AddDbContext<StudentRegistryDbContext>(
                options => options.UseSqlServer(studentRegistryConnectionString));

            return services
                .AddScoped<IStudentsRepository, StudentsRepository>();
        }
    }
}

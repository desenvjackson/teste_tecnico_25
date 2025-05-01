using Ambev.DeveloperEvaluation.Application;
using Ambev.DeveloperEvaluation.Common.HealthChecks;
using Ambev.DeveloperEvaluation.Common.Logging;
using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.IoC;
using Ambev.DeveloperEvaluation.Infrastructure.Context;
using Ambev.DeveloperEvaluation.WebApi.Middleware;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.WebApi.Mocks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Ambev.DeveloperEvaluation.ORM;
using Ambev.DeveloperEvaluation.Infrastructure.Repositories;
public class Program
{
    public static void Main(string[] args)
    {
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .MinimumLevel.Debug()
            .CreateLogger();

        try
        {
            Log.Information("Starting web application");

            var builder = WebApplication.CreateBuilder(args);

            Log.Information("ConfigureKestrel");
            builder.WebHost.ConfigureKestrel(options =>
            {
                options.ListenAnyIP(5109); // Porta HTTP
                options.ListenAnyIP(5188, listenOptions => listenOptions.UseHttps()); // Porta HTTPS
            });

            builder.Logging.ClearProviders();
            builder.Logging.AddConsole();
            Log.Information("Default logging configured.");

            Log.Information("Building the application...");
            builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
                    options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
                });

            builder.Services.AddEndpointsApiExplorer();

            builder.AddBasicHealthChecks();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseInMemoryDatabase("InMemoryDb"));

            builder.Services.AddScoped<ISaleRepository, SaleRepository>();
            builder.Services.AddScoped<IUserRepository, MockUserRepository>();

            builder.Services.AddJwtAuthentication(builder.Configuration);

            builder.RegisterDependencies();

            builder.Services.AddAutoMapper(typeof(Program).Assembly, typeof(ApplicationLayer).Assembly);

            builder.Services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblies(
                    typeof(ApplicationLayer).Assembly,
                    typeof(Program).Assembly
                );
            });

            builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowFrontend", policy =>
                {
                    policy.WithOrigins("http://localhost:3000")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

            var app = builder.Build();
            Log.Information("Application built successfully.");

            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                ApplicationDbContext.Seed(context);
            }

            app.UseMiddleware<ValidationExceptionMiddleware>();
            Log.Information("Middleware configured.");

            if (app.Environment.IsDevelopment())
            {
                Log.Information("Environment is Development. Enabling Swagger...");
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            Log.Information("HTTPS redirection enabled.");

            app.UseAuthentication();
            Log.Information("Authentication middleware configured.");

            app.UseCors("AllowFrontend");

            app.UseAuthorization();
            Log.Information("Authorization middleware configured.");

            app.UseBasicHealthChecks();
            Log.Information("Health checks middleware configured.");

            app.MapControllers();
            Log.Information("Controllers mapped.");

            Log.Information("Application is about to run...");
            app.Run();
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Application terminated unexpectedly");
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }
}
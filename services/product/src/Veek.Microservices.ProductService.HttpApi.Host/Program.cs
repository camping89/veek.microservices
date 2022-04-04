using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Veek.Microservices.ProductService.DbMigrations;
using Veek.Microservices.Shared.Hosting.AspNetCore;
using Serilog;

namespace Veek.Microservices.ProductService;

public class Program
{
    public async static Task<int> Main(string[] args)
    {
        var assemblyName = typeof(Program).Assembly.GetName().Name;

        SerilogConfigurationHelper.Configure(assemblyName);

        try
        {
            Log.Information($"Starting {assemblyName}.");

            var builder = WebApplication.CreateBuilder(args);
            builder.Host
                .AddAppSettingsSecretsJson()
                .UseAutofac()
                .UseSerilog();
            await builder.AddApplicationAsync<ProductServiceHttpApiHostModule>();
            var app = builder.Build();
            await app.InitializeApplicationAsync();

            using (var scope = app.Services.CreateScope())
            {
                await scope.ServiceProvider
                    .GetRequiredService<ProductServiceDatabaseMigrationChecker>()
                    .CheckAsync();
            }

            await app.RunAsync();
            return 0;
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, $"{assemblyName} terminated unexpectedly!");
            return 1;
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }
}

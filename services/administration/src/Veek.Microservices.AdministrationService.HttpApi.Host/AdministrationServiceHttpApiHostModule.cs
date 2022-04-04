using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Veek.Microservices.AdministrationService.DbMigrations;
using Veek.Microservices.AdministrationService.MongoDB;
using Veek.Microservices.IdentityService;
using Veek.Microservices.ProductService;
using Veek.Microservices.SaasService;
using Veek.Microservices.Shared.Hosting.Microservices;
using Prometheus;
using Volo.Abp;
using Volo.Abp.Account;
using Volo.Abp.AspNetCore.Mvc.UI.MultiTenancy;
using Volo.Abp.Http.Client.IdentityModel.Web;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;
using Volo.Abp.Threading;

namespace Veek.Microservices.AdministrationService;

[DependsOn(
    typeof(MicroservicesSharedLocalizationModule),
    typeof(AbpHttpClientIdentityModelWebModule),
    typeof(AbpAspNetCoreMvcUiMultiTenancyModule),
    typeof(AbpIdentityHttpApiClientModule),
    typeof(AbpAccountAdminApplicationContractsModule),
    typeof(AbpAccountPublicApplicationContractsModule),
    typeof(MicroservicesSharedHostingMicroservicesModule),
    typeof(ProductServiceApplicationContractsModule),
    typeof(SaasServiceApplicationContractsModule),
    typeof(IdentityServiceApplicationContractsModule),
    typeof(AdministrationServiceApplicationModule),
    typeof(AdministrationServiceMongoDbModule),
    typeof(AdministrationServiceHttpApiModule)
)]
public class AdministrationServiceHttpApiHostModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        // Enable if you need these
        // var configuration = context.Services.GetConfiguration();
        // var hostingEnvironment = context.Services.GetHostingEnvironment();

        JwtBearerConfigurationHelper.Configure(context, "AdministrationService");
        SwaggerConfigurationHelper.Configure(context, "Administration Service API");
    }

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        var app = context.GetApplicationBuilder();
        var env = context.GetEnvironment();

        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseCorrelationId();
        app.UseAbpRequestLocalization();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseHttpMetrics();
        app.UseAuthentication();
        app.UseAbpClaimsMap();
        app.UseMultiTenancy();
        app.UseAuthorization();
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "Administration Service API");
        });
        app.UseAbpSerilogEnrichers();
        app.UseAuditing();
        app.UseUnitOfWork();
        app.UseConfiguredEndpoints(endpoints =>
        {
            endpoints.MapMetrics();
        });
    }

    public async override Task OnPostApplicationInitializationAsync(ApplicationInitializationContext context)
    {
        using (var scope = context.ServiceProvider.CreateScope())
        {
            await scope.ServiceProvider
                .GetRequiredService<AdministrationServiceDatabaseMigrationChecker>()
                .CheckAsync();
        }
    }
}

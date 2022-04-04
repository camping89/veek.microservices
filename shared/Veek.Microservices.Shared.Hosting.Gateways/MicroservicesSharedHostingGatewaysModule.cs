using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Veek.Microservices.Shared.Hosting.AspNetCore;
using Ocelot.DependencyInjection;
using Ocelot.Provider.Polly;
using Volo.Abp.AspNetCore.Mvc.UI.MultiTenancy;
using Volo.Abp.Modularity;
using Volo.Abp.Swashbuckle;

namespace Veek.Microservices.Shared.Hosting.Gateways;

[DependsOn(
    typeof(MicroservicesSharedHostingAspNetCoreModule),
    typeof(AbpSwashbuckleModule),
    typeof(AbpAspNetCoreMvcUiMultiTenancyModule)
)]
public class MicroservicesSharedHostingGatewaysModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var configuration = context.Services.GetConfiguration();
        var env = context.Services.GetHostingEnvironment();

        var ocelotBuilder = context.Services.AddOcelot(configuration)
            .AddPolly();

        if (!env.IsProduction())
        {
            ocelotBuilder.AddDelegatingHandler<AbpRemoveCsrfCookieHandler>(true);
        }
    }
}

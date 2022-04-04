using Volo.Abp.AspNetCore.Serilog;
using Volo.Abp.Modularity;

namespace Veek.Microservices.Shared.Hosting.AspNetCore;

[DependsOn(
    typeof(MicroservicesSharedHostingModule),
    typeof(AbpAspNetCoreSerilogModule)
)]
public class MicroservicesSharedHostingAspNetCoreModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
    }
}

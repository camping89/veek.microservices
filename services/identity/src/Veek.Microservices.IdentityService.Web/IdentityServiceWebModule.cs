using Volo.Abp.Identity.Web;
using Volo.Abp.IdentityServer.Web;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace Veek.Microservices.IdentityService.Web;

[DependsOn(
    typeof(AbpIdentityWebModule),
    typeof(AbpIdentityServerWebModule),
    typeof(IdentityServiceApplicationContractsModule)
)]
public class IdentityServiceWebModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<IdentityServiceWebModule>();
        });
    }
}

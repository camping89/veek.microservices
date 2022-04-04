using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Account;
using Volo.Abp.AutoMapper;
using Volo.Abp.Identity;
using Volo.Abp.IdentityServer;
using Volo.Abp.Modularity;

namespace Veek.Microservices.IdentityService;

[DependsOn(
    typeof(AbpIdentityApplicationModule),
    typeof(AbpIdentityServerApplicationModule),
    typeof(IdentityServiceDomainModule),
    typeof(AbpAccountAdminApplicationModule),
    typeof(IdentityServiceApplicationContractsModule)
)]
public class IdentityServiceApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAutoMapperObjectMapper<IdentityServiceApplicationModule>();
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<IdentityServiceApplicationModule>(validate: true);
        });
    }
}

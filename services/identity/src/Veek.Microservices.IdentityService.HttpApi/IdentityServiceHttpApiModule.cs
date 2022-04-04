using Volo.Abp.Account;
using Volo.Abp.Identity;
using Volo.Abp.IdentityServer;
using Volo.Abp.Modularity;

namespace Veek.Microservices.IdentityService;

[DependsOn(
    typeof(IdentityServiceApplicationContractsModule),
    typeof(AbpIdentityHttpApiModule),
    typeof(AbpIdentityServerHttpApiModule),
    typeof(AbpAccountAdminHttpApiModule)
    )]
public class IdentityServiceHttpApiModule : AbpModule
{

}

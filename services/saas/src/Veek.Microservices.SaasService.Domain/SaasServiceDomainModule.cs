using Volo.Abp.Modularity;
using Volo.Saas;

namespace Veek.Microservices.SaasService;

[DependsOn(
    typeof(SaasServiceDomainSharedModule),
    typeof(SaasDomainModule)
)]
public class SaasServiceDomainModule : AbpModule
{
}

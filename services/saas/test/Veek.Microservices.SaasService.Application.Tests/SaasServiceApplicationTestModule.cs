using Veek.Microservices.SaasService.Application;
using Volo.Abp.Modularity;

namespace Veek.Microservices.SaasService;

[DependsOn(
    typeof(SaasServiceApplicationModule),
    typeof(SaasServiceDomainTestModule)
    )]
public class SaasServiceApplicationTestModule : AbpModule
{

}

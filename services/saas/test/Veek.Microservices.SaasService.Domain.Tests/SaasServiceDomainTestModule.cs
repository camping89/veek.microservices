using Veek.Microservices.MongoDB;
using Veek.Microservices.SaasService.MongoDB;
using Volo.Abp.Modularity;

namespace Veek.Microservices.SaasService;

/* Domain tests are configured to use the EF Core provider.
 * You can switch to MongoDB, however your domain tests should be
 * database independent anyway.
 */
[DependsOn(
    typeof(SaasMicroservicesMongoDbTestModule)
    )]
public class SaasServiceDomainTestModule : AbpModule
{

}

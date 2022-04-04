using Veek.Microservices.IdentityService.MongoDB;
using Volo.Abp.Modularity;

namespace Veek.Microservices.IdentityService;

/* Domain tests are configured to use the EF Core provider.
 * You can switch to MongoDB, however your domain tests should be
 * database independent anyway.
 */
[DependsOn(
    typeof(IdentityServiceMongoDbModule)
    )]
public class IdentityServiceDomainTestModule : AbpModule
{

}

using Veek.Microservices.AdministrationService.MongoDB;
using Veek.Microservices.MongoDB;
using Volo.Abp.Modularity;

namespace Veek.Microservices.AdministrationService;

/* Domain tests are configured to use the EF Core provider.
 * You can switch to MongoDB, however your domain tests should be
 * database independent anyway.
 */
[DependsOn(
    typeof(AdministrationMicroservicesMongoDbTestModule)
    )]
public class AdministrationServiceDomainTestModule : AbpModule
{

}

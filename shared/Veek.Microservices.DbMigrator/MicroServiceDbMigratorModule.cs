using Veek.Microservices.AdministrationService;
using Veek.Microservices.AdministrationService.MongoDB;
using Veek.Microservices.IdentityService;
using Veek.Microservices.IdentityService.MongoDB;
using Veek.Microservices.ProductService;
using Veek.Microservices.ProductService.MongoDB;
using Veek.Microservices.SaasService;
using Veek.Microservices.SaasService.MongoDB;
using Veek.Microservices.Shared.Hosting;
using Volo.Abp.Modularity;

namespace Veek.Microservices.DbMigrator;

[DependsOn(
    typeof(MicroservicesSharedHostingModule),
    typeof(IdentityServiceMongoDbModule),
    typeof(IdentityServiceApplicationContractsModule),
    typeof(SaasServiceMongoDbModule),
    typeof(SaasServiceApplicationContractsModule),
    typeof(AdministrationServiceMongoDbModule),
    typeof(AdministrationServiceApplicationContractsModule),
    typeof(ProductServiceApplicationContractsModule),
    typeof(ProductServiceMongoDbModule)
)]
public class MicroServiceDbMigratorModule : AbpModule
{

}

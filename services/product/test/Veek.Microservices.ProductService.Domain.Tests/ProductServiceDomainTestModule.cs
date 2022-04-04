using Veek.Microservices.MongoDB;
using Veek.Microservices.ProductService.MongoDB;
using Volo.Abp.Modularity;

namespace Veek.Microservices.ProductService;

/* Domain tests are configured to use the EF Core provider.
 * You can switch to MongoDB, however your domain tests should be
 * database independent anyway.
 */
[DependsOn(
    typeof(ProductMicroservicesMongoDbTestModule)
    )]
public class ProductServiceDomainTestModule : AbpModule
{

}

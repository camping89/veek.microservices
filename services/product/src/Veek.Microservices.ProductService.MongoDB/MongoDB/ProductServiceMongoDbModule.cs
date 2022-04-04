using Microsoft.Extensions.DependencyInjection;
using Veek.Microservices.ProductService.Products;
using Volo.Abp.Modularity;
using Volo.Abp.MongoDB;
using Volo.Abp.Uow;

namespace Veek.Microservices.ProductService.MongoDB;

[DependsOn(
    typeof(AbpMongoDbModule),
    typeof(ProductServiceDomainModule)
)]
public class ProductServiceMongoDbModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        ProductServiceMongoDBExtensionMappings.Configure();
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddMongoDbContext<ProductServiceDbContext>(options =>
        {
            options.AddDefaultRepositories(includeAllEntities: true);
            options.AddRepository<Product, MongoDbProductRepository>();
        });
        
        Configure<AbpUnitOfWorkDefaultOptions>(options =>
        {
            options.TransactionBehavior = UnitOfWorkTransactionBehavior.Disabled;
        });
    }
}
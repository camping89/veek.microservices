using System;
using Veek.Microservices.ProductService;
using Veek.Microservices.ProductService.MongoDB;
using Volo.Abp.Data;
using Volo.Abp.Modularity;
using Volo.Abp.Uow;

namespace Veek.Microservices.MongoDB;

[DependsOn(
    typeof(ProductServiceTestBaseModule),
    typeof(ProductServiceMongoDbModule)
)]
public class ProductMicroservicesMongoDbTestModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var stringArray = ProductMicroservicesMongoDbFixture.ConnectionString.Split('?');
        var connectionString = stringArray[0].EnsureEndsWith('/') +
                               "Db_" +
                               Guid.NewGuid().ToString("N") + "/?" + stringArray[1];

        Configure<AbpDbConnectionOptions>(options =>
        {
            options.ConnectionStrings.Default = connectionString;
        });
    }
}

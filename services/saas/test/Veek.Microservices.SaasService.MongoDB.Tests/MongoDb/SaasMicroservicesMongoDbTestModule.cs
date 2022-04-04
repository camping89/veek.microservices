using System;
using Veek.Microservices.SaasService;
using Veek.Microservices.SaasService.MongoDB;
using Volo.Abp.Data;
using Volo.Abp.Modularity;
using Volo.Abp.Uow;

namespace Veek.Microservices.MongoDB;

[DependsOn(
    typeof(SaasServiceTestBaseModule),
    typeof(SaasServiceMongoDbModule)
)]
public class SaasMicroservicesMongoDbTestModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var stringArray = SaasMicroservicesMongoDbFixture.ConnectionString.Split('?');
        var connectionString = stringArray[0].EnsureEndsWith('/') +
                               "Db_" +
                               Guid.NewGuid().ToString("N") + "/?" + stringArray[1];

        Configure<AbpDbConnectionOptions>(options =>
        {
            options.ConnectionStrings.Default = connectionString;
        });
    }
}

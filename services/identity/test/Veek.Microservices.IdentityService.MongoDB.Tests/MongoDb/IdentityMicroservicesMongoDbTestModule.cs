using System;
using Veek.Microservices.IdentityService;
using Veek.Microservices.IdentityService.MongoDB;
using Volo.Abp.Data;
using Volo.Abp.Modularity;
using Volo.Abp.Uow;

namespace Veek.Microservices.MongoDB;

[DependsOn(
    typeof(IdentityServiceTestBaseModule),
    typeof(IdentityServiceMongoDbModule)
)]
public class IdentityMicroservicesMongoDbTestModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var stringArray = IdentityMicroservicesMongoDbFixture.ConnectionString.Split('?');
        var connectionString = stringArray[0].EnsureEndsWith('/') +
                               "Db_" +
                               Guid.NewGuid().ToString("N") + "/?" + stringArray[1];

        Configure<AbpDbConnectionOptions>(options =>
        {
            options.ConnectionStrings.Default = connectionString;
        });
    }
}

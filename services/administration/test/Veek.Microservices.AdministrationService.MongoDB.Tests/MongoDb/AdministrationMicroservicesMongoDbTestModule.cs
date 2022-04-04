using System;
using Veek.Microservices.AdministrationService;
using Veek.Microservices.AdministrationService.MongoDB;
using Volo.Abp.Data;
using Volo.Abp.Modularity;
using Volo.Abp.Uow;

namespace Veek.Microservices.MongoDB;

[DependsOn(
    typeof(AdministrationServiceTestBaseModule),
    typeof(AdministrationServiceMongoDbModule)
)]
public class AdministrationMicroservicesMongoDbTestModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var stringArray = AdministrationMicroservicesMongoDbFixture.ConnectionString.Split('?');
        var connectionString = stringArray[0].EnsureEndsWith('/') +
                               "Db_" +
                               Guid.NewGuid().ToString("N") + "/?" + stringArray[1];

        Configure<AbpDbConnectionOptions>(options =>
        {
            options.ConnectionStrings.Default = connectionString;
        });
    }
}

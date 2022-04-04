using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;
using Volo.Abp.Uow;
using Volo.Payment.MongoDB;
using Volo.Saas.MongoDB;

namespace Veek.Microservices.SaasService.MongoDB;

[DependsOn(
    typeof(SaasServiceDomainModule),
    typeof(SaasMongoDbModule)
)]
public class SaasServiceMongoDbModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        SaasServiceMongoDbExtensionMappings.Configure();
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddMongoDbContext<SaasServiceDbContext>(options =>
        {
            options.ReplaceDbContext<ISaasMongoDbContext>();
            options.ReplaceDbContext<IPaymentMongoDbContext>();

            /* includeAllEntities: true allows to use IRepository<TEntity, TKey> also for non aggregate root entities */
            options.AddDefaultRepositories(includeAllEntities: true);
        });

        Configure<AbpUnitOfWorkDefaultOptions>(options =>
        {
            options.TransactionBehavior = UnitOfWorkTransactionBehavior.Disabled;
        });
    }
}
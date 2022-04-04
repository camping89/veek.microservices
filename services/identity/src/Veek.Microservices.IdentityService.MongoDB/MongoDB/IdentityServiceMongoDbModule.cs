using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.MongoDB;
using Volo.Abp.Identity.MongoDB;
using Volo.Abp.IdentityServer.MongoDB;
using Volo.Abp.Modularity;
using Volo.Abp.Uow;

namespace Veek.Microservices.IdentityService.MongoDB;

[DependsOn(
    typeof(IdentityServiceDomainModule),
    typeof(AbpIdentityProMongoDbModule),
    typeof(AbpIdentityServerMongoDbModule)
)]
public class IdentityServiceMongoDbModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        IdentityServiceEfCoreEntityExtensionMappings.Configure();
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddMongoDbContext<IdentityServiceDbContext>(options =>
        {
            options.ReplaceDbContext<IAbpIdentityMongoDbContext>();
            options.ReplaceDbContext<IAbpIdentityServerMongoDbContext>();

            /* includeAllEntities: true allows to use IRepository<TEntity, TKey> also for non aggregate root entities */
            options.AddDefaultRepositories(includeAllEntities: true);
        });
        
        Configure<AbpUnitOfWorkDefaultOptions>(options =>
        {
            options.TransactionBehavior = UnitOfWorkTransactionBehavior.Disabled;
        });
    }
}
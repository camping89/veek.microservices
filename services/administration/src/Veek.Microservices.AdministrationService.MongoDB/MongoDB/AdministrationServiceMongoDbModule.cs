using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AuditLogging.MongoDB;
using Volo.Abp.BlobStoring.Database.MongoDB;
using Volo.Abp.FeatureManagement.MongoDB;
using Volo.Abp.LanguageManagement.MongoDB;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement.MongoDB;
using Volo.Abp.SettingManagement.MongoDB;
using Volo.Abp.TextTemplateManagement.MongoDB;
using Volo.Abp.Uow;

namespace Veek.Microservices.AdministrationService.MongoDB;

[DependsOn(
    typeof(AdministrationServiceDomainModule),
    typeof(AbpPermissionManagementMongoDbModule),
    typeof(AbpFeatureManagementMongoDbModule),
    typeof(AbpSettingManagementMongoDbModule),
    typeof(AbpAuditLoggingMongoDbModule),
    typeof(BlobStoringDatabaseMongoDbModule),
    typeof(LanguageManagementMongoDbModule),
    typeof(TextTemplateManagementMongoDbModule)
)]
public class AdministrationServiceMongoDbModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        AdministrationServiceMongoDbExtensionMappings.Configure();
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddMongoDbContext<AdministrationServiceDbContext>(options =>
        {
            options.ReplaceDbContext<IPermissionManagementMongoDbContext>();
            options.ReplaceDbContext<ISettingManagementMongoDbContext>();
            options.ReplaceDbContext<IFeatureManagementMongoDbContext>();
            options.ReplaceDbContext<IAuditLoggingMongoDbContext>();
            options.ReplaceDbContext<ILanguageManagementMongoDbContext>();
            options.ReplaceDbContext<ITextTemplateManagementMongoDbContext>();
            options.ReplaceDbContext<IBlobStoringMongoDbContext>();

            /* includeAllEntities: true allows to use IRepository<TEntity, TKey> also for non aggregate root entities */
            options.AddDefaultRepositories(includeAllEntities: true);
        });
        
        Configure<AbpUnitOfWorkDefaultOptions>(options =>
        {
            options.TransactionBehavior = UnitOfWorkTransactionBehavior.Disabled;
        });
    }
}
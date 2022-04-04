using System;
using System.Threading.Tasks;
using Veek.Microservices.SaasService.MongoDB;
using Veek.Microservices.Shared.Hosting.Microservices.DbMigrations;
using Volo.Abp.Data;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Uow;
using Volo.Saas.Tenants;

namespace Veek.Microservices.SaasService.DbMigrations;

public class SaasServiceDatabaseMigrationEventHandler
    : DatabaseMigrationEventHandlerBase<SaasServiceDbContext>,
        IDistributedEventHandler<ApplyDatabaseMigrationsEto>
{
    public SaasServiceDatabaseMigrationEventHandler(
        ICurrentTenant currentTenant,
        IUnitOfWorkManager unitOfWorkManager,
        ITenantStore tenantStore,
        ITenantRepository tenantRepository,
        IDistributedEventBus distributedEventBus,
        IServiceProvider serviceProvider
    ) : base(
        currentTenant,
        unitOfWorkManager,
        tenantStore,
        tenantRepository,
        distributedEventBus,
        SaasServiceDbProperties.ConnectionStringName,
        serviceProvider)
    {
    }

    public async Task HandleEventAsync(ApplyDatabaseMigrationsEto eventData)
    {
        if (eventData.DatabaseName != DatabaseName)
        {
            return;
        }

        if (eventData.TenantId != null)
        {
            return;
        }

        try
        {
            await MigrateDatabaseSchemaAsync(null);
        }
        catch (Exception ex)
        {
            await HandleErrorOnApplyDatabaseMigrationAsync(eventData, ex);
        }
    }
}

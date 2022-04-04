using System;
using System.Linq;
using System.Threading.Tasks;
using Veek.Microservices.AdministrationService.MongoDB;
using Veek.Microservices.Shared.Hosting.Microservices.DbMigrations;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Data;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.MultiTenancy;
using Volo.Abp.PermissionManagement;
using Volo.Abp.Uow;
using Volo.Saas.Tenants;

namespace Veek.Microservices.AdministrationService.DbMigrations;

public class AdministrationServiceDatabaseMigrationEventHandler
    : DatabaseMigrationEventHandlerBase<AdministrationServiceDbContext>,
        IDistributedEventHandler<TenantCreatedEto>,
        IDistributedEventHandler<TenantConnectionStringUpdatedEto>,
        IDistributedEventHandler<ApplyDatabaseMigrationsEto>
{
    private readonly IPermissionDefinitionManager _permissionDefinitionManager;
    private readonly IPermissionDataSeeder _permissionDataSeeder;

    public AdministrationServiceDatabaseMigrationEventHandler(
        ICurrentTenant currentTenant,
        IUnitOfWorkManager unitOfWorkManager,
        ITenantStore tenantStore,
        IPermissionDefinitionManager permissionDefinitionManager,
        IPermissionDataSeeder permissionDataSeeder,
        ITenantRepository tenantRepository,
        IDistributedEventBus distributedEventBus,
        IServiceProvider serviceProvider
    ) : base(
        currentTenant,
        unitOfWorkManager,
        tenantStore,
        tenantRepository,
        distributedEventBus,
        AdministrationServiceDbProperties.ConnectionStringName,
        serviceProvider)
    {
        _permissionDefinitionManager = permissionDefinitionManager;
        _permissionDataSeeder = permissionDataSeeder;
    }

    public async Task HandleEventAsync(ApplyDatabaseMigrationsEto eventData)
    {
        if (eventData.DatabaseName != DatabaseName)
        {
            return;
        }

        try
        {
            var schemaMigrated = await MigrateDatabaseSchemaAsync(eventData.TenantId);
            await SeedDataAsync(eventData.TenantId);

            if (eventData.TenantId == null && schemaMigrated)
            {
                /* Migrate tenant databases after host migration */
                await QueueTenantMigrationsAsync();
            }
        }
        catch (Exception ex)
        {
            await HandleErrorOnApplyDatabaseMigrationAsync(eventData, ex);
        }
    }

    public async Task HandleEventAsync(TenantCreatedEto eventData)
    {
        try
        {
            await MigrateDatabaseSchemaAsync(eventData.Id);
            await SeedDataAsync(eventData.Id);
        }
        catch (Exception ex)
        {
            await HandleErrorTenantCreatedAsync(eventData, ex);
        }
    }

    public async Task HandleEventAsync(TenantConnectionStringUpdatedEto eventData)
    {
        if (eventData.ConnectionStringName != DatabaseName && eventData.ConnectionStringName != ConnectionStrings.DefaultConnectionStringName ||
            eventData.NewValue.IsNullOrWhiteSpace())
        {
            return;
        }

        try
        {
            await MigrateDatabaseSchemaAsync(eventData.Id);
            await SeedDataAsync(eventData.Id);

            /* You may want to move your data from the old database to the new database!
             * It is up to you. If you don't make it, new database will be empty. */
        }
        catch (Exception ex)
        {
            await HandleErrorTenantConnectionStringUpdatedAsync(eventData, ex);
        }
    }

    private async Task SeedDataAsync(Guid? tenantId)
    {
        using (CurrentTenant.Change(tenantId))
        {
            using (var uow = UnitOfWorkManager.Begin(requiresNew: true, isTransactional: true))
            {
                var multiTenancySide = tenantId == null
                    ? MultiTenancySides.Host
                    : MultiTenancySides.Tenant;

                var permissionNames = _permissionDefinitionManager
                    .GetPermissions()
                    .Where(p => p.MultiTenancySide.HasFlag(multiTenancySide))
                    .Where(p => !p.Providers.Any() || p.Providers.Contains(RolePermissionValueProvider.ProviderName))
                    .Select(p => p.Name)
                    .ToArray();

                await _permissionDataSeeder.SeedAsync(
                    RolePermissionValueProvider.ProviderName,
                    "admin",
                    permissionNames,
                    tenantId
                );

                await uow.CompleteAsync();
            }
        }
    }
}

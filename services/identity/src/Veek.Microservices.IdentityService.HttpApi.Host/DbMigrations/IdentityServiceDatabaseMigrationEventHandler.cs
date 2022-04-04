using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Veek.Microservices.IdentityService.MongoDB;
using Veek.Microservices.Shared.Hosting.Microservices.DbMigrations;
using Volo.Abp;
using Volo.Abp.Data;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.Identity;
using Volo.Abp.LanguageManagement.Data;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Uow;
using Volo.Saas.Tenants;

namespace Veek.Microservices.IdentityService.DbMigrations;

public class IdentityServiceDatabaseMigrationEventHandler
    : DatabaseMigrationEventHandlerBase<IdentityServiceDbContext>,
        IDistributedEventHandler<TenantCreatedEto>,
        IDistributedEventHandler<TenantConnectionStringUpdatedEto>,
        IDistributedEventHandler<ApplyDatabaseMigrationsEto>
{
    private readonly IIdentityDataSeeder _identityDataSeeder;
    private readonly IdentityServerDataSeeder _identityServerDataSeeder;
    private readonly LanguageManagementDataSeeder _languageManagementDataSeeder;

    public IdentityServiceDatabaseMigrationEventHandler(
        ICurrentTenant currentTenant,
        IUnitOfWorkManager unitOfWorkManager,
        ITenantStore tenantStore,
        IIdentityDataSeeder identityDataSeeder,
        IdentityServerDataSeeder identityServerDataSeeder,
        LanguageManagementDataSeeder languageManagementDataSeeder,
        ITenantRepository tenantRepository,
        IDistributedEventBus distributedEventBus,
        IServiceProvider serviceProvider
    ) : base(
        currentTenant,
        unitOfWorkManager,
        tenantStore,
        tenantRepository,
        distributedEventBus,
        IdentityServiceDbProperties.ConnectionStringName,
        serviceProvider)
    {
        _identityDataSeeder = identityDataSeeder;
        _identityServerDataSeeder = identityServerDataSeeder;
        _languageManagementDataSeeder = languageManagementDataSeeder;
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
            await SeedDataAsync(
                tenantId: eventData.TenantId,
                adminEmail: IdentityServiceDbProperties.DefaultAdminEmailAddress,
                adminPassword: IdentityServiceDbProperties.DefaultAdminPassword
            );

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
            await SeedDataAsync(
                tenantId: eventData.Id,
                adminEmail: eventData.Properties.GetOrDefault(IdentityDataSeedContributor.AdminEmailPropertyName) ?? IdentityServiceDbProperties.DefaultAdminEmailAddress,
                adminPassword: eventData.Properties.GetOrDefault(IdentityDataSeedContributor.AdminPasswordPropertyName) ?? IdentityServiceDbProperties.DefaultAdminPassword
            );
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
            await SeedDataAsync(
                tenantId: eventData.Id,
                adminEmail: IdentityServiceDbProperties.DefaultAdminEmailAddress,
                adminPassword: IdentityServiceDbProperties.DefaultAdminPassword
            );

            /* You may want to move your data from the old database to the new database!
             * It is up to you. If you don't make it, new database will be empty
             * (and tenant's admin password is reset to IdentityServiceDbProperties.DefaultAdminPassword). */
        }
        catch (Exception ex)
        {
            await HandleErrorTenantConnectionStringUpdatedAsync(eventData, ex);
        }
    }

    private async Task SeedDataAsync(Guid? tenantId, string adminEmail, string adminPassword)
    {
        using (CurrentTenant.Change(tenantId))
        {
            if (tenantId == null)
            {
                await _languageManagementDataSeeder.SeedAsync();
                await _identityServerDataSeeder.SeedAsync();
            }

            await _identityDataSeeder.SeedAsync(
                adminEmail,
                adminPassword,
                tenantId
            );
        }
    }
}

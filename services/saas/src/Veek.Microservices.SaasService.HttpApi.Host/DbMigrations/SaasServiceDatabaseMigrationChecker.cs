using System;
using Veek.Microservices.SaasService.MongoDB;
using Veek.Microservices.Shared.Hosting.Microservices.DbMigrations;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Uow;

namespace Veek.Microservices.SaasService.DbMigrations;

public class SaasServiceDatabaseMigrationChecker : PendingMigrationsCheckerBase<SaasServiceDbContext>
{
    public SaasServiceDatabaseMigrationChecker(
        IUnitOfWorkManager unitOfWorkManager,
        IServiceProvider serviceProvider,
        ICurrentTenant currentTenant,
        IDistributedEventBus distributedEventBus)
        : base(
            unitOfWorkManager,
            serviceProvider,
            currentTenant,
            distributedEventBus,
            SaasServiceDbProperties.ConnectionStringName)
    {

    }
}

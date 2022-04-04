using System;
using Veek.Microservices.ProductService.MongoDB;
using Veek.Microservices.Shared.Hosting.Microservices.DbMigrations;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Uow;

namespace Veek.Microservices.ProductService.DbMigrations;

public class ProductServiceDatabaseMigrationChecker : PendingMigrationsCheckerBase<ProductServiceDbContext>
{
    public ProductServiceDatabaseMigrationChecker(
        IUnitOfWorkManager unitOfWorkManager,
        IServiceProvider serviceProvider,
        ICurrentTenant currentTenant,
        IDistributedEventBus distributedEventBus)
        : base(
            unitOfWorkManager,
            serviceProvider,
            currentTenant,
            distributedEventBus,
            ProductServiceDbProperties.ConnectionStringName)
    {

    }
}

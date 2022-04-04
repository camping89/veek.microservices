using MongoDB.Driver;
using Volo.Abp.Data;
using Volo.Abp.MongoDB;
using Volo.Payment.MongoDB;
using Volo.Payment.Plans;
using Volo.Payment.Requests;
using Volo.Saas.Editions;
using Volo.Saas.MongoDB;
using Volo.Saas.Tenants;

namespace Veek.Microservices.SaasService.MongoDB;

[ConnectionStringName(SaasServiceDbProperties.ConnectionStringName)]
public interface ISaasServiceDbContext : IAbpMongoDbContext, ISaasMongoDbContext, IPaymentMongoDbContext
{
    IMongoCollection<TenantConnectionString> TenantConnectionStrings { get; }
    IMongoCollection<GatewayPlan> GatewayPlans { get; }
}

[ConnectionStringName(SaasServiceDbProperties.ConnectionStringName)]
public class SaasServiceDbContext : AbpMongoDbContext, ISaasServiceDbContext
{
    public IMongoCollection<Tenant> Tenants => Collection<Tenant>();
    public IMongoCollection<Edition> Editions => Collection<Edition>();
    public IMongoCollection<TenantConnectionString> TenantConnectionStrings => Collection<TenantConnectionString>();

    public IMongoCollection<PaymentRequest> PaymentRequests => Collection<PaymentRequest>();
    public IMongoCollection<Plan> Plans => Collection<Plan>();
    public IMongoCollection<GatewayPlan> GatewayPlans => Collection<GatewayPlan>();
}

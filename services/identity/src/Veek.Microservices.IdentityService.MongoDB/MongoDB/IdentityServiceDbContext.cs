using MongoDB.Driver;
using Volo.Abp.Data;
using Volo.Abp.Identity;
using Volo.Abp.Identity.MongoDB;
using Volo.Abp.IdentityServer.ApiResources;
using Volo.Abp.IdentityServer.ApiScopes;
using Volo.Abp.IdentityServer.Clients;
using Volo.Abp.IdentityServer.Devices;
using Volo.Abp.IdentityServer.Grants;
using Volo.Abp.IdentityServer.IdentityResources;
using Volo.Abp.IdentityServer.MongoDB;
using Volo.Abp.MongoDB;

namespace Veek.Microservices.IdentityService.MongoDB;

[ConnectionStringName(IdentityServiceDbProperties.ConnectionStringName)]
public class IdentityServiceDbContext : AbpMongoDbContext, IAbpIdentityMongoDbContext, IAbpIdentityServerMongoDbContext
{
    public IMongoCollection<IdentityUser> Users => Collection<IdentityUser>();
    public IMongoCollection<IdentityRole> Roles => Collection<IdentityRole>();
    public IMongoCollection<IdentityClaimType> ClaimTypes => Collection<IdentityClaimType>();
    public IMongoCollection<OrganizationUnit> OrganizationUnits => Collection<OrganizationUnit>();
    public IMongoCollection<IdentitySecurityLog> SecurityLogs => Collection<IdentitySecurityLog>();
    public IMongoCollection<IdentityLinkUser> LinkUsers => Collection<IdentityLinkUser>();
    public IMongoCollection<ApiResource> ApiResources => Collection<ApiResource>();
    public IMongoCollection<ApiResourceSecret> ApiResourceSecrets => Collection<ApiResourceSecret>();
    public IMongoCollection<ApiResourceClaim> ApiResourceClaims => Collection<ApiResourceClaim>();
    public IMongoCollection<ApiResourceScope> ApiResourceScopes => Collection<ApiResourceScope>();
    public IMongoCollection<ApiResourceProperty> ApiResourceProperties => Collection<ApiResourceProperty>();
    public IMongoCollection<ApiScope> ApiScopes => Collection<ApiScope>();
    public IMongoCollection<ApiScopeClaim> ApiScopeClaims => Collection<ApiScopeClaim>();
    public IMongoCollection<ApiScopeProperty> ApiScopeProperties => Collection<ApiScopeProperty>();
    public IMongoCollection<IdentityResource> IdentityResources => Collection<IdentityResource>();
    public IMongoCollection<IdentityResourceClaim> IdentityClaims => Collection<IdentityResourceClaim>();

    public IMongoCollection<IdentityResourceProperty> IdentityResourceProperties =>
        Collection<IdentityResourceProperty>();

    public IMongoCollection<Client> Clients => Collection<Client>();
    public IMongoCollection<ClientGrantType> ClientGrantTypes => Collection<ClientGrantType>();
    public IMongoCollection<ClientRedirectUri> ClientRedirectUris => Collection<ClientRedirectUri>();

    public IMongoCollection<ClientPostLogoutRedirectUri> ClientPostLogoutRedirectUris =>
        Collection<ClientPostLogoutRedirectUri>();

    public IMongoCollection<ClientScope> ClientScopes => Collection<ClientScope>();
    public IMongoCollection<ClientSecret> ClientSecrets => Collection<ClientSecret>();
    public IMongoCollection<ClientClaim> ClientClaims => Collection<ClientClaim>();
    public IMongoCollection<ClientIdPRestriction> ClientIdPRestrictions => Collection<ClientIdPRestriction>();
    public IMongoCollection<ClientCorsOrigin> ClientCorsOrigins => Collection<ClientCorsOrigin>();
    public IMongoCollection<ClientProperty> ClientProperties => Collection<ClientProperty>();
    public IMongoCollection<PersistedGrant> PersistedGrants => Collection<PersistedGrant>();
    public IMongoCollection<DeviceFlowCodes> DeviceFlowCodes => Collection<DeviceFlowCodes>();
}
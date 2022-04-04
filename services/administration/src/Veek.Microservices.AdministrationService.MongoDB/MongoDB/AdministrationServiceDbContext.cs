using MongoDB.Driver;
using Veek.Microservices.AdministrationService;
using Volo.Abp.AuditLogging;
using Volo.Abp.AuditLogging.MongoDB;
using Volo.Abp.BlobStoring.Database;
using Volo.Abp.BlobStoring.Database.MongoDB;
using Volo.Abp.Data;
using Volo.Abp.MongoDB;
using Volo.Abp.FeatureManagement;
using Volo.Abp.FeatureManagement.MongoDB;
using Volo.Abp.LanguageManagement;
using Volo.Abp.LanguageManagement.MongoDB;
using Volo.Abp.PermissionManagement;
using Volo.Abp.PermissionManagement.MongoDB;
using Volo.Abp.SettingManagement;
using Volo.Abp.SettingManagement.MongoDB;
using Volo.Abp.TextTemplateManagement.MongoDB;
using Volo.Abp.TextTemplateManagement.TextTemplates;

namespace Veek.Microservices.AdministrationService.MongoDB;

[ConnectionStringName(AdministrationServiceDbProperties.ConnectionStringName)]
public class AdministrationServiceDbContext : AbpMongoDbContext,
    IPermissionManagementMongoDbContext,
    ISettingManagementMongoDbContext,
    IFeatureManagementMongoDbContext,
    IAuditLoggingMongoDbContext,
    ILanguageManagementMongoDbContext,
    ITextTemplateManagementMongoDbContext,
    IBlobStoringMongoDbContext
{
    public IMongoCollection<PermissionGrant> PermissionGrants => Collection<PermissionGrant>();
    public IMongoCollection<Setting> Settings => Collection<Setting>();
    public IMongoCollection<FeatureValue> FeatureValues => Collection<FeatureValue>();
    public IMongoCollection<AuditLog> AuditLogs => Collection<AuditLog>();
    public IMongoCollection<Language> Languages => Collection<Language>();
    public IMongoCollection<LanguageText> LanguageTexts => Collection<LanguageText>();
    public IMongoCollection<TextTemplateContent> TextTemplates => Collection<TextTemplateContent>();
    public IMongoCollection<DatabaseBlobContainer> BlobContainers => Collection<DatabaseBlobContainer>();
    public IMongoCollection<DatabaseBlob> Blobs => Collection<DatabaseBlob>();
}

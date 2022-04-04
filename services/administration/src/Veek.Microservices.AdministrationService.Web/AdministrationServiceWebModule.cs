using Volo.Abp.Account.Admin.Web;
using Volo.Abp.AuditLogging.Web;
using Volo.Abp.LanguageManagement;
using Volo.Abp.LeptonTheme.Management;
using Volo.Abp.Modularity;
using Volo.Abp.TextTemplateManagement.Web;
using Volo.Abp.VirtualFileSystem;

namespace Veek.Microservices.AdministrationService.Web;

[DependsOn(
    typeof(LeptonThemeManagementWebModule),
    typeof(AbpAuditLoggingWebModule),
    typeof(LanguageManagementWebModule),
    typeof(TextTemplateManagementWebModule),
    typeof(AbpAccountAdminWebModule),
    typeof(AdministrationServiceApplicationContractsModule)
    )]
public class AdministrationServiceWebModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<AdministrationServiceWebModule>();
        });
    }
}

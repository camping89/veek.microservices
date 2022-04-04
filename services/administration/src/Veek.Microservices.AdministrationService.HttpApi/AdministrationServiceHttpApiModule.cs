using Volo.Abp.AuditLogging;
using Volo.Abp.FeatureManagement;
using Volo.Abp.LanguageManagement;
using Volo.Abp.LeptonTheme;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement.HttpApi;
using Volo.Abp.SettingManagement;
using Volo.Abp.TextTemplateManagement;

namespace Veek.Microservices.AdministrationService;

[DependsOn(
    typeof(AdministrationServiceApplicationContractsModule),
    typeof(AbpPermissionManagementHttpApiModule),
    typeof(AbpFeatureManagementHttpApiModule),
    typeof(AbpSettingManagementHttpApiModule),
    typeof(AbpAuditLoggingHttpApiModule),
    typeof(LeptonThemeManagementHttpApiModule),
    typeof(LanguageManagementHttpApiModule),
    typeof(TextTemplateManagementHttpApiModule)
)]
public class AdministrationServiceHttpApiModule : AbpModule
{
}

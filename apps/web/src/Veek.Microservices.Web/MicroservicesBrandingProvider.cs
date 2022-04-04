using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace Veek.Microservices.Web;

[Dependency(ReplaceServices = true)]
public class MicroservicesBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "Microservices";
}

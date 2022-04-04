using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace Veek.Microservices.PublicWeb;

[Dependency(ReplaceServices = true)]
public class MicroservicesBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "Microservices";
}

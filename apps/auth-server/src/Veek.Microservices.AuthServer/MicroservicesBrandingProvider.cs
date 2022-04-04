using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace Veek.Microservices.AuthServer;

[Dependency(ReplaceServices = true)]
public class MicroservicesBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "Microservices";
}

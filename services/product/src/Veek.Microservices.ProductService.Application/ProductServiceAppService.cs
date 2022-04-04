using Veek.Microservices.ProductService.Localization;
using Volo.Abp.Application.Services;

namespace Veek.Microservices.ProductService;

public abstract class ProductServiceAppService : ApplicationService
{
    protected ProductServiceAppService()
    {
        LocalizationResource = typeof(ProductServiceResource);
        ObjectMapperContext = typeof(ProductServiceApplicationModule);
    }
}

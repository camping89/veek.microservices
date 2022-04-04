using Veek.Microservices.ProductService.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Veek.Microservices.ProductService;

public abstract class ProductServiceController : AbpController
{
    protected ProductServiceController()
    {
        LocalizationResource = typeof(ProductServiceResource);
    }
}

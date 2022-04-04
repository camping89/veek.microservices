using AutoMapper;
using Veek.Microservices.ProductService.Products;

namespace Veek.Microservices.ProductService.Web;

public class ProductServiceWebAutoMapperProfile : Profile
{
    public ProductServiceWebAutoMapperProfile()
    {
        CreateMap<ProductDto, ProductUpdateDto>();
    }
}

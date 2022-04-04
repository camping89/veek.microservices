using AutoMapper;
using Veek.Microservices.ProductService.Products;

namespace Veek.Microservices.ProductService;

public class ProductServiceApplicationAutoMapperProfile : Profile
{
    public ProductServiceApplicationAutoMapperProfile()
    {
        CreateMap<Product, ProductDto>();
    }
}

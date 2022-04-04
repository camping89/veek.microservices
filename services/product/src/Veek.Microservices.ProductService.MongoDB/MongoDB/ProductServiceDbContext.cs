using Veek.Microservices.ProductService.Products;
using MongoDB.Driver;
using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace Veek.Microservices.ProductService.MongoDB;

[ConnectionStringName(ProductServiceDbProperties.ConnectionStringName)]
public interface IProductServiceDbContext : IAbpMongoDbContext
{
    IMongoCollection<Product> Products { get; }
}


[ConnectionStringName(ProductServiceDbProperties.ConnectionStringName)]
public class ProductServiceDbContext : AbpMongoDbContext, IProductServiceDbContext
{
    public IMongoCollection<Product> Products => Collection<Product>();
}


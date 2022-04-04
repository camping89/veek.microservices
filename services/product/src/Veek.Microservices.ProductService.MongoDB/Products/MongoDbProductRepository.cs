using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Veek.Microservices.ProductService.MongoDB;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Volo.Abp.Domain.Repositories.MongoDB;
using Volo.Abp.MongoDB;

namespace Veek.Microservices.ProductService.Products;

public class MongoDbProductRepository : MongoDbRepository<ProductServiceDbContext, Product, Guid>, IProductRepository
{
    public MongoDbProductRepository(IMongoDbContextProvider<ProductServiceDbContext> dbContextProvider)
        : base(dbContextProvider)
    {

    }

    public async Task<List<Product>> GetListAsync(
        string filterText = null,
        string name = null,
        float? priceMin = null,
        float? priceMax = null,
        string sorting = null,
        int maxResultCount = int.MaxValue,
        int skipCount = 0,
        CancellationToken cancellationToken = default)
    {
        var query = ApplyFilter(await GetMongoQueryableAsync(cancellationToken), filterText, name, priceMin, priceMax);
        query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? ProductConsts.GetDefaultSorting(false) : sorting);
        return await query.As<IMongoQueryable<Product>>().PageBy<Product, IMongoQueryable<Product>>(skipCount, maxResultCount).ToListAsync(cancellationToken);
    }

    public async Task<long> GetCountAsync(
        string filterText = null,
        string name = null,
        float? priceMin = null,
        float? priceMax = null,
        CancellationToken cancellationToken = default)
    {
        var query = ApplyFilter(await GetMongoQueryableAsync(cancellationToken), filterText, name, priceMin, priceMax);
        return await query.As<IMongoQueryable<Product>>().LongCountAsync(GetCancellationToken(cancellationToken));
    }

    protected virtual IQueryable<Product> ApplyFilter(
        IQueryable<Product> query,
        string filterText,
        string name = null,
        float? priceMin = null,
        float? priceMax = null)
    {
        return query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Name.Contains(filterText))
                .WhereIf(!string.IsNullOrWhiteSpace(name), e => e.Name.Contains(name))
                .WhereIf(priceMin.HasValue, e => e.Price >= priceMin.Value)
                .WhereIf(priceMax.HasValue, e => e.Price <= priceMax.Value);
    }
}

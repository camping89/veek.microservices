using Veek.Microservices.ProductService;
using Xunit;

namespace Veek.Microservices.MongoDB;

[CollectionDefinition(ProductMicroservicesTestConsts.CollectionDefinitionName)]
public class ProductMicroservicesMongoCollection : ProductMicroservicesMongoDbCollectionFixtureBase
{

}

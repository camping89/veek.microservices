using System;
using System.Threading.Tasks;
using Shouldly;
using Veek.Microservices.ProductService;
using Veek.Microservices.ProductService.Products;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace Veek.Microservices.MongoDB.Samples;

/* This is just an example test class.
 * Normally, you don't test ABP framework code
 * Only test your custom repository methods.
 */
[Collection(ProductMicroservicesTestConsts.CollectionDefinitionName)]
public class AdministrationSampleRepositoryTests : ProductServiceTestBase
{
    private readonly IRepository<Product, Guid> _appUserRepository;
    
    public AdministrationSampleRepositoryTests()
    {
        _appUserRepository = GetRequiredService<IRepository<Product, Guid>>();
    }
    
    [Fact]
    public async Task Should_Query_AppUser()
    {
        /* Need to manually start Unit Of Work because
         * FirstOrDefaultAsync should be executed while db connection / context is available.
         */
        await WithUnitOfWorkAsync(async () =>
        {
                //Act
                var adminUser = await _appUserRepository
                .FirstOrDefaultAsync(u => u.Name == "test");
    
                //Assert
                adminUser.ShouldNotBeNull();
        });
    }
}

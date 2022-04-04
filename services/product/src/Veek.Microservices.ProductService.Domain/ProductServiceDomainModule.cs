﻿using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace Veek.Microservices.ProductService;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(ProductServiceDomainSharedModule)
)]
public class ProductServiceDomainModule : AbpModule
{

}

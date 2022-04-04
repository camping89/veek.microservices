using System.ComponentModel.DataAnnotations;

namespace Veek.Microservices.ProductService.Products;

public class ProductCreateDto
{
    [Required]
    [StringLength(ProductConsts.NameMaxLength)]
    public string Name { get; set; }

    [Required]
    public float Price { get; set; }
}

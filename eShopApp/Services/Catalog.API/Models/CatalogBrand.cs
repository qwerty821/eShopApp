using System.ComponentModel.DataAnnotations;

namespace eShopApp.Models;

public class CatalogBrand
{
    public int Id { get; set; }

    [Required]
    public string Brand { get; set; }
}

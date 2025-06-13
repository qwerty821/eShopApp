using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eShopApp.Models;

public class CatalogBrand
{
    [Column("CategoryID")]
    public int Id { get; set; }

    [Column("Name")]
    [Required]
    public string Brand { get; set; }
    
    [Column("Description")]
    public string Description { get; set; }


}

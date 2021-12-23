using System.ComponentModel.DataAnnotations;

namespace ToyShopAPI.Models
{
    public class ProductModel
    {
        [Key]
        [Required]
        public int ID { get; set; }

        public string? Name { get; set; }    
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string? Manufacturer { get; set; }

    }
}

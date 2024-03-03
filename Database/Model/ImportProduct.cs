using ShopMVC.Database.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopMVC.Database.Model
{
    public class ImportProduct : BaseModel
    {
        [Required]
        public int ProductId { get; set; }
        [Required]
        public double Price { get; set; }
        public string Color { get; set; }
        [Required]
        public int Quantity { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }

    }
}

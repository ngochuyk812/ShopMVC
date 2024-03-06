using ShopMVC.Database.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopMVC.Database.Model
{
    public class Product :BaseModel
    {
        [Required]
        [MaxLength(300)]
        public string Title { get; set; }
        [Required]
        [MaxLength(300)]
        public string SubTitle { get; set; }
        [Required]
        public double Price { get; set; }
        public int? Discount { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }
        public virtual IEnumerable<Review> Reviews { get; set; }
        public virtual IEnumerable<ImageProduct> Images { get; set;}
        public virtual IEnumerable<ImportProduct> Imports { get; set; }

    }
}

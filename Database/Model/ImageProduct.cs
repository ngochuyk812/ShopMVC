using ShopMVC.Database.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopMVC.Database.Model
{
    public class ImageProduct : BaseModel
    {
        [Required]
        public int ProductId { get; set; }
        [Required]
        public string Src { get; set; }
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
         
    }
}

using ShopMVC.Database.Model;
using System.ComponentModel.DataAnnotations;

namespace ShopMVC.Database.Model
{
    public class Category : BaseModel
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }   
    }
}

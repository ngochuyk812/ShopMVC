using ShopMVC.Database.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopMVC.DTO
{
    public class ProductDTO : BaseModel
    {
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public double Price { get; set; }
        public int? Discount { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public virtual IEnumerable<ReviewDTO> Reviews { get; set; }
        public virtual IEnumerable<ImageProductDTO> Images { get; set;}
        public virtual IEnumerable<ImportProductDTO> Imports { get; set; }

    }
}

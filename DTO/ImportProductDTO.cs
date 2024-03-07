using ShopMVC.Database.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopMVC.DTO
{
    public class ImportProductDTO : BaseModel
    {
        public int ProductId { get; set; }
        public string Color { get; set; }
        public int Quantity { get; set; }
    }
}

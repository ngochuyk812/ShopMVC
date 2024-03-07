using ShopMVC.Database.Model;

namespace ShopMVC.DTO
{
    public class CartDTO:BaseModel
    {
        public int ProductId { get; set; }
        public int ImportId { get; set; }
        public int UserId { get; set; }
        public int Quantity { get; set; }
        public virtual ImportProductDTO Import { get; set; }
        public virtual ProductDTO Product { get; set; }
        public virtual UserDTO User { get; set; }

    }
}

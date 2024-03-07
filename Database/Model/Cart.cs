using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography.X509Certificates;

namespace ShopMVC.Database.Model
{
    public class Cart : BaseModel
    {
        public int ImportId { get; set; }
        public int ProductId { get; set; }
        public int UserId { get; set; }
        public int Quantity { get; set; }

        [ForeignKey("ImportId")]
        public virtual ImportProduct Import { get; set; }
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }

    }
}

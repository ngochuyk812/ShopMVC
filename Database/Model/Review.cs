using System.ComponentModel.DataAnnotations.Schema;

namespace ShopMVC.Database.Model
{
    public class Review : BaseModel
    {
        
        public int IdUser { get; set; }
        public int IdProduct { get; set; }

        public string Content { get; set; }
       
        [ForeignKey("IdUser")]
        public virtual User User { get; set; }
        [ForeignKey("IdProduct")]
        public virtual Product Product { get; set; }
        public virtual List<MediaReview> Medias { get; set; }
    }
}

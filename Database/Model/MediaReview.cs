using ShopMVC.Database.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopMVC.Database.Model
{
    public class MediaReview : BaseModel
    {
        public enum TypeEnum
        {
            IMAGE,
            VIDEO
        }
        public int ReviewId { get; set; }
        [Required]
        public string Src { get; set; }
        [Required]
        public TypeEnum Type { get; set; }
        [ForeignKey("ReviewId")]
        public virtual Review Review { get; set; }
         
    }
}

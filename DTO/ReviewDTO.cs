using ShopMVC.Database.Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopMVC.DTO
{
    public class ReviewDTO 
    {
        public int Id { get; set; }
        public int IdUser { get; set; }
        public string Content { get; set; }
        public UserDTO User { get; set; }
        public List<MediaReviewDTO> Medias { get; set; }
        public DateTime CreatedDate { get; set; } 


    }
}

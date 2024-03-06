using System.ComponentModel.DataAnnotations.Schema;

namespace ShopMVC.Database.Model
{
    public class UserRole:BaseModel
    {
        public int IdUser { get; set; }
        public int IdRole { get; set; }
        [ForeignKey("IdRole")]
        public virtual Role Role { get; set; }
        [ForeignKey("IdUser")]
        public virtual User User { get; set; }
        
    }
}

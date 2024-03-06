using System.ComponentModel.DataAnnotations.Schema;

namespace ShopMVC.Database.Model
{
    public class Address:BaseModel
    {
        public string City { get; set; }
        public string District { get; set; }
        public string Commune { get; set; }
        public string Detail { get; set; }
        public string Phone { get; set; }
        public int IdUser { get; set; }
        [ForeignKey("IdUser")]
        public virtual User User { get; set; }
    }
}

using ShopMVC.Database.Model;

namespace ShopMVC.ViewModel
{
    public class HomeViewModel
    {
        public IEnumerable<Product> NewClock { get; set; } = new List<Product>();
        public IEnumerable<Product> NewJewelry { get; set; } = new List<Product>();
    }
}

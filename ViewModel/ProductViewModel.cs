using ShopMVC.Database.Model;
using ShopMVC.Extentions;

namespace ShopMVC.ViewModel
{
    public class ProductViewModel
    {
        public enum SortEnum
        {
            NORMAL, // Corrected typo
            AZ,
            ZA
        }

        public string Title { get; set; } = string.Empty;
        public double MinPrice { get; set; } = 0;
        public double MaxPrice { get; set; } = -1;
        public int Category { get; set; } = -1;
        public SortEnum Sort { get; set; } = SortEnum.NORMAL;
        public Pagination<Product> Data;
        public int Page { get; set; } = 1;
    }
}

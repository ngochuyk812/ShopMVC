namespace ShopMVC.ViewModel
{
    public class ReviewViewModel
    {
        public string? Content { get; set; }
        public int IdProduct { get; set; }
        public IEnumerable<IFormFile>? Files { get; set; } = new List<IFormFile>();
    }
}

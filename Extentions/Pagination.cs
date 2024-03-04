namespace ShopMVC.Extentions
{
    public class Pagination<T> where T : class
    {
        public long Total { get; set; }
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public int TotalPage { get; set; }
        public IEnumerable<T> Data { get; set; }
        public bool HasPreviousPage => PageIndex > 1;
        public bool HasNextPage => PageIndex < TotalPage;
        public Pagination(long Total, int PageIndex, int PageSize, IEnumerable<T> Data)
        {
            this.Total = Total;
            this.PageIndex = PageIndex;
            this.PageSize = PageSize;
            this.Data = Data;
            TotalPage = (int)Math.Ceiling(Total / (double)PageSize);
        }
    }
}

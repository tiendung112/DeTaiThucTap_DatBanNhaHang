namespace DatBanNhaHang.Pagination
{
    public class Pagintation
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }

        public int TotalCount { get; set; }
        public int TotalPage
        {
            get
            {
                if (PageSize == 0) return 0;
                var total = TotalCount / PageSize;
                if (TotalCount % PageSize != 0)
                    total++;
                return total;
            }
        }
        public Pagintation()
        {
            PageSize = -1;
            PageNumber = 1;
        }
    }
}

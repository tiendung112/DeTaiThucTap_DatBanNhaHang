namespace DatBanNhaHang.Handler.Pagination
{
    public class Pagintation
    {
        public static PageResult<T> GetPagedData<T>(IQueryable<T> data, int pageSize, int pageNumber)
        {
            int totalItems = data.Count();
            int totalPages = pageSize == 0 ? 1 : (int)Math.Ceiling((decimal)totalItems / pageSize);

            var pagedData = pageSize == 0 ? data : data.Skip((pageNumber - 1) * pageSize).Take(pageSize);

            return new PageResult<T>(pagedData, totalItems, totalPages, pageNumber, pageSize);
        }
    }
}

namespace PostApi.Infrastructure.Pagination
{
    public class PagedList<T>
    {
        public PagedList(IEnumerable<T> items, int page, int pageSize, int total)
        {
            Items = items;
            Page = page;
            PageSize = pageSize;
            Total = total;
        }
        public IEnumerable<T> Items { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int Total { get; set; }
        public bool HasNext => Page * PageSize < Total;
        public bool HasPrevious => Page > 1;

        public static PagedList<T> Paginate(IQueryable<T> query, int page, int pageSize)
        {
            var total = query.Count();
            var items = query.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            return new PagedList<T>(items, page, pageSize, total);
        }

        public override string ToString()
        {
            return $"\npage: {Page} \npagesize: {PageSize} \ntotal: {Total} \nHasNext: {HasNext} \nHasPrevious: {HasPrevious} \nitem: {Items} \n";
        }
    }
}
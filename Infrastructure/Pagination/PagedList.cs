using Microsoft.EntityFrameworkCore;

namespace PostApi.Infrastructure.Pagination
{
    public class PagedList<T>
    {
        public PagedList(ICollection<T> items, int page, int pageSize, int totalItems, int totalPages )
        {
            Items = items;
            Page = page;
            PageSize = pageSize;
            TotalItems = totalItems;
            TotalPages = totalPages;
        }
        public ICollection<T> Items { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
        public bool HasNext => Page * PageSize < TotalItems;
        public bool HasPrevious => Page > 1;

        public static async Task<PagedList<T>> Paginate(IQueryable<T> query, int page, int pageSize)
        {
            int totalItems = query.Count();
            int totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
            ICollection<T> items = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PagedList<T>(items, page, pageSize, totalItems, totalPages);
        }

        public override string ToString()
        {
            return $"\npage: {Page} \npagesize: {PageSize} \ntotal items: {TotalItems} \nHasNext: {HasNext} \nHasPrevious: {HasPrevious} \nitem: {Items} \n";
        }
    }
}
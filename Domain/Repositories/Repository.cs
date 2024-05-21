using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PostApi.Infrastructure.Pagination;
using Topy_like_asp_webapi.Domain.DBContexts;
using Topy_like_asp_webapi.Domain.Entities.Base;
using Topy_like_asp_webapi.Domain.Repositories.Interfaces;

namespace Topy_like_asp_webapi.Domain.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly DBContext _context;
        public DbSet<T> entity { get; set; }

        public Repository(DBContext context)
        {
            _context = context;
            entity = context.Set<T>();
        }

        public PagedList<T> All(int page, int pageSize)
        {
            var result = entity.OrderBy(model => model.Id);
            return PagedList<T>.Paginate(result, page, pageSize);
        }

        public void Create(T model)
        {
            entity.Add(model);
            _context.SaveChanges();
        }

        public void Delete(T model)
        {
            entity.Remove(model);
            _context.SaveChanges();
        }

        public T Get(Guid id)
        {
            return entity.FirstOrDefault(model => model.Id == id);
        }

        public IEnumerable<T> Index()
        {
            return entity.ToList();
        }

        public void Update(T model)
        {
            _context.Entry(model).State = EntityState.Modified;
            model.UpdatedAt = DateTime.UtcNow;
            _context.SaveChanges();
        }
    }
}
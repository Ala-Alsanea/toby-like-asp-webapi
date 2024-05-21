using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PostApi.Infrastructure.Pagination;
using Topt_like_asp_webapi.Domain.DBContexts;
using Topt_like_asp_webapi.Domain.Entities.Base;
using Topt_like_asp_webapi.Domain.Repositories.Interfaces;

namespace Topt_like_asp_webapi.Domain.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly DBContext _context;
        private DbSet<T> entities;

        public Repository(DBContext context)
        {
            _context = context;
            entities = context.Set<T>();
        }

        public PagedList<T> All(int page, int pageSize)
        {
            var result = entities.OrderBy(model => model.Id);
            return PagedList<T>.Paginate(result, page, pageSize);
        }

        public void Create(T model)
        {
            entities.Add(model);
            _context.SaveChanges();
        }

        public void Delete(T model)
        {
            entities.Remove(model);
            _context.SaveChanges();
        }

        public T Get(Guid id)
        {
            return entities.FirstOrDefault(model => model.Id == id);
        }

        public IEnumerable<T> Index()
        {
            return entities.ToList();
        }

        public void Update(T model)
        {
            _context.Entry(model).State = EntityState.Modified;
            _context.SaveChanges();
        }

        // public DbSet<T> Custom  ()
        // {
        //     _context.Entry(model).State = EntityState.Modified;
        //     _context.SaveChanges();
        // }
    }
}
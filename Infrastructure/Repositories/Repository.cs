using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using PostApi.Infrastructure.Pagination;
using Topy_like_asp_webapi.Domain.DBContexts;
using Topy_like_asp_webapi.Infrastructure.Entities;
using Topy_like_asp_webapi.Infrastructure.Repositories.Interfaces;

namespace Topy_like_asp_webapi.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly DBContext _context;
        private readonly ILogger<Repository<T>> _logger;

        public DbSet<T> entity { get; set; }

        public Repository(DBContext context, ILogger<Repository<T>> logger)
        {
            _context = context;
            _logger = logger;
            entity = context.Set<T>();
        }

        public PagedList<T> Paged(int page, int pageSize)
        {

            try
            {
                var result = entity.OrderBy(model => model.Id);
                return PagedList<T>.Paginate(result, page, pageSize);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while fetching the {nameof(T)}"); // Log error with exception
                throw;
            }

        }

        async public Task<T> GetAsync(Guid id)
        {

            try
            {
                return await entity.FirstOrDefaultAsync(model => model.Id == id);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while fetching the {nameof(T)}"); // Log error with exception
                throw;
            }

        }

        async public Task<IEnumerable<T>> AllAsync()
        {

            try
            {
                return await entity.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while fetching the {nameof(T)}"); // Log error with exception
                throw;
            }
        }

        async public Task<bool> CreateAsync(T model)
        {
            try
            {
                await entity.AddAsync(model);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while creating the {nameof(T)} "); // Log error with exception
                throw;
            }
        }

        async public Task<bool> DeleteAsync(T model)
        {

            try
            {
                entity.Remove(model);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while deleting the {nameof(T)}"); // Log error with exception
                throw;
            }
        }

        async public Task<bool> UpdateAsync(T model)
        {
            try
            {
                _context.Entry(model).State = EntityState.Modified;
                model.UpdatedAt = DateTime.UtcNow;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while fetching the {nameof(T)}"); // Log error with exception
                throw;
            }
        }


    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.IdentityModel.Tokens;
using PostApi.Infrastructure.Pagination;
using Topy_like_asp_webapi.Domain.DBContexts;
using Topy_like_asp_webapi.Infrastructure.Entities;
using Topy_like_asp_webapi.Infrastructure.ErrorHandling;
using Topy_like_asp_webapi.Infrastructure.Repositories.Interfaces;

namespace Topy_like_asp_webapi.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly DBContext _context;
        private readonly ILogger<Repository<T>> _logger;

        private const string _errorMessage = "An error occurred in repository:\n ";

        public DbSet<T> entity { get; set; }



        public Repository(DBContext context, ILogger<Repository<T>> logger)
        {
            _context = context;
            _logger = logger;
            entity = context.Set<T>();
        }

        async public Task<ResultReturn<PagedList<T>>> Paged(int page, int pageSize, params Expression<Func<T, object>>[] includes)
        {

            try
            {
                IQueryable<T> query = entity.OrderBy(model => model.Id);


                if (includes is not null)
                {
                    foreach (Expression<Func<T, object>>? include in includes)
                    {
                        query = query.Include(include);
                    }
                }

                PagedList<T> result = await PagedList<T>.Paginate(query, page, pageSize);
                return result.Items is null ? ResultReturn.Fail<PagedList<T>>("no item found") : ResultReturn.Ok<PagedList<T>>(result);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{_errorMessage}  \n {ex.Message}");
                throw new Exception(_errorMessage + ex.Message);
                // return ResultReturn.Fail<PagedList<T>>(_errorMessage + ex.Message);
            }

        }

        async public Task<ResultReturn<T>> GetAsync(Guid id)
        {

            try
            {
                T? result = await entity.FirstOrDefaultAsync(model => model.Id == id);
                return result is null ? ResultReturn.Fail<T>("no item found") : ResultReturn.Ok<T>(result);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{_errorMessage}  \n {ex.Message}"); // Log error with exception
                throw new Exception(_errorMessage + ex.Message);
                // return ResultReturn.Fail<T>(_errorMessage + ex.Message);
            }

        }

        async public Task<ResultReturn<ICollection<T>>> AllAsync(params Expression<Func<T, object>>[] includes)
        {
            try
            {
                IQueryable<T> query = entity;

                var len = includes.Length;

                if (includes is not null)
                {
                    foreach (var include in includes)
                    {
                        query = query.Include(include);
                    }
                }

                List<T> result = await query.ToListAsync();

                return result is null ? ResultReturn.Fail<ICollection<T>>("no item found") : ResultReturn.Ok<ICollection<T>>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{_errorMessage}  \n {ex.Message}"); // Log error with exception
                throw new Exception(_errorMessage + ex.Message);
                // return ResultReturn.Fail<ICollection<T>>(_errorMessage + ex.Message);
            }
        }

        async public Task<ResultReturn<int>> CreateAsync(T model)
        {
            try
            {
                await entity.AddAsync(model);
                int result = await _context.SaveChangesAsync();
                return result == 0 ? ResultReturn.Fail<int>("item not saved") : ResultReturn.Ok<int>(result);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{_errorMessage}  \n {ex.Message}"); // Log error with exception
                throw new Exception(_errorMessage + ex.Message);
                // return ResultReturn.Fail<int>(_errorMessage + ex.Message);
            }
        }

        async public Task<ResultReturn<int>> DeleteAsync(T model)
        {

            try
            {
                entity.Remove(model);
                int result = await _context.SaveChangesAsync();
                return result == 0 ? ResultReturn.Fail<int>("item not deleted") : ResultReturn.Ok<int>(result);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{_errorMessage}  \n {ex.Message}"); // Log error with exception
                throw new Exception(_errorMessage + ex.Message);
                // return ResultReturn.Fail<int>(_errorMessage + ex.Message);
            }
        }

        async public Task<ResultReturn<int>> UpdateAsync(T model)
        {
            try
            {
                entity.Entry(model).State = EntityState.Modified;
                model.UpdatedAt = DateTime.UtcNow;
                int result = await _context.SaveChangesAsync();
                return result == 0 ? ResultReturn.Fail<int>("item not updated") : ResultReturn.Ok<int>(result);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{_errorMessage}  \n {ex.Message}"); // Log error with exception
                throw new Exception(_errorMessage + ex.Message);
                // return ResultReturn.Fail<int>(_errorMessage + ex.Message);
            }
        }


    }
}
using GBLAC.Data;
using GBLAC.Models.DTOs;
//using GBLAC.Repository.APIRepository.interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using X.PagedList;

namespace GBLAC.Repository.APIRepository.Implementations
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly GBlacContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(GBlacContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<bool> DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IList<T>> GetAllAsync(Expression<Func<T, bool>> expression = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderby = null, List<string> Includes = null)
        {
            var query = _dbSet.AsNoTracking();
            if (Includes != null) Includes.ForEach(x => query.Include(x));
            if (expression != null) query = query.Where(expression);
            if (orderby != null) query = orderby(query);

            return await query.ToListAsync();
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> expression, List<string> Includes = null)
        {
            var query = _dbSet;
            if (Includes != null) Includes.ForEach(x => query.Include(x));
            return await _dbSet.AsNoTracking().Where(expression).FirstOrDefaultAsync(expression);
        }

        public async Task<IPagedList<T>> GetPageList(
            PagingDTO pager,
            Expression<Func<T, bool>> expression = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderby = null,
            List<string> Includes = null)
        {
            IList<T> listQuery = await GetAllAsync(expression, orderby, Includes);
            var pageList = await listQuery.ToPagedListAsync(pager.PageNumber, pager.PageSize);
            return pageList;
        }

        public async Task<bool> InsertAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}

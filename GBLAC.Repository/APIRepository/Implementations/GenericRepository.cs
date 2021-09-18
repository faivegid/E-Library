using GBLAC.Data;
using GBLAC.Repository.APIRepository.interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

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
        public async Task<bool> Create(T model)
        {
            await _dbSet.AddAsync(model);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> Delete(T model)
        {
            _dbSet.Remove(model);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<T> Get(string Id)
        {
            return await _dbSet.FindAsync(Id);
        }

        public IQueryable<T> GetAll()
        {
            return _dbSet.AsNoTracking();
        }

        public async Task<bool> Update(T model)
        {
            _dbSet.Update(model);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}

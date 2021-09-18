using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBLAC.Repository.APIRepository
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> Get(string Id);
        IQueryable<T> GetAll();
        Task<bool> Create(T model);
        Task<bool> Update(T model);
        Task<bool> Delete(T model);
    }
}

using GBLAC.Models;
using GBLAC.Repository.APIRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBLAC.Repository.APIUnitOfWork.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Book> Books { get; }
        IGenericRepository<Category> Categories {  get; }
    }
}

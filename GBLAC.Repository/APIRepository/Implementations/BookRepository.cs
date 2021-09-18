using GBLAC.Data;
using GBLAC.Models;
using GBLAC.Repository.APIRepository.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBLAC.Repository.APIRepository.Implementations
{
    public class BookRepository : GenericRepository<Book> 
    {
        public BookRepository(GBlacContext context):base(context)
        {
        }
    }
}

using GBLAC.Models;
using GBLAC.Models.DTOs;
using System.Collections;
using System.Threading.Tasks;

namespace GBLAC.Services.APIServices.Interfaces
{
    public interface IBookServices
    {
        Task<bool> AddBookAsync(BookDTO bookDTO);
        Task<bool> DeleteBookAsync(Book book);
        Task<IEnumerable> GetAllBooksByCategory(PagingDTO pager, string categoryName);
        Task<IEnumerable> GetAllBooksByType(PagingDTO pager, string bookTypeName);
        Task<Book> GetBookAsync(string bookName);
        Task<bool> UpdateBook(Book book);
    }
}
using GBLAC.Models;
using GBLAC.Models.DTOs;
using GBLAC.Services.APIServices.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Collections;
using System.Threading.Tasks;

namespace GBLAC.Services.APIServices.Implementations
{
    public interface IBookServices
    {
        Task<bool> AddBookAsync(BookDTO bookDTO);
        Task<bool> DeleteBookAsync(Book book);
        Task<IEnumerable> GetAllBooksByCategory(PagingDTO pager, string categoryName);
        Task<IEnumerable> GetAllBooksByType(PagingDTO pager, string bookTypeName);
        Task<Book> GetBookAsync(string bookName);
        Task<bool> UpdateBook(Book book);
        Task<bool> UplodeBookFile(Book book, IFormFile bookFile);
    }
}
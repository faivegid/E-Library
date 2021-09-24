using AutoMapper;
using GBLAC.Models;
using GBLAC.Models.DTOs;
using GBLAC.Repository.APIUnitOfWork.Interfaces;
using GBLAC.Services.APIServices.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using X.PagedList;

namespace GBLAC.Services.APIServices.Implementations
{
    public class BookServices : IBookServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BookServices(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Book> GetBookAsync(string bookName)
        {
            var result = await _unitOfWork.Books.GetAsync(b => b.BookName == bookName, null);
            if (result != null)
            {
                return result;
            }
            throw new BadHttpRequestException("Book does not exist", StatusCodes.Status400BadRequest);
        }
        public async Task<IEnumerable> GetAllBooksByCategory(PagingDTO pager, string categoryName)
        {
            var categoryList = await _unitOfWork.Categories.GetAllAsync(b => b.Name == categoryName, null, new List<string>() { "Books" });
            var books = new List<Book>();
            foreach (var category in categoryList)
            {
                books.AddRange(category.Books);
            }
            return await books.ToPagedListAsync(pager.PageNumber, pager.PageSize);
        }
        public async Task<IEnumerable> GetAllBooksByType(PagingDTO pager, string bookTypeName)
        {
            var bookTypeList = await _unitOfWork.BookTypes.GetAllAsync(b => b.Name == bookTypeName, null, new List<string>() { "Books" });
            var books = new List<Book>();
            foreach (var bookType in bookTypeList)
            {
                books.AddRange(bookType.Books);
            }
            return await books.ToPagedListAsync(pager.PageNumber, pager.PageSize);
        }

        public async Task<bool> AddBookAsync(BookDTO bookDTO)
        {
            var book = _mapper.Map<Book>(bookDTO);
            var result = await _unitOfWork.Books.InsertAsync(book);
            if (result)
            {
                return result;
            }
            throw new BadHttpRequestException("Error Creating Book", StatusCodes.Status400BadRequest);
        }

        public async Task<bool> DeleteBookAsync(Book book)
        {
            var result = await _unitOfWork.Books.DeleteAsync(book);
            if (result)
            {
                return result;
            }
            throw new BadHttpRequestException("Error Deleting Book", StatusCodes.Status400BadRequest);
        }

        public async Task<bool> UpdateBook(Book book)
        {
            var result = await _unitOfWork.Books.UpdateAsync(book);
            if (result)
            {
                return result;
            }
            throw new BadHttpRequestException("Book was not updated", StatusCodes.Status400BadRequest);
        }

        public async Task<bool> UplodeBookFile(Book book, IFormFile bookFile)
        {
            var result = await _unitOfWork.Books.GetAsync(b => b.BookName == book.BookName, null);
            if (result != null)
            {
                using (var target = new MemoryStream())
                {
                    bookFile.CopyTo(target);
                    result.BookFileObject.BookFile = target.ToArray();
                }
            }
            else
            {
                throw new BadHttpRequestException("Book does not exist", StatusCodes.Status400BadRequest);
            }

            var result2 = await _unitOfWork.Books.UpdateAsync(result);
            if (result2)
            {
                return result2;
            }
            throw new BadHttpRequestException("Book was not updated", StatusCodes.Status400BadRequest);
        }

        public async Task<FileContentResult> GetBookFile(Book book )
        {
            var bookObject = await _unitOfWork.Books.GetAsync(b => b.BookName == book.BookName, null);
            if (bookObject != null)
            {
                if (bookObject.BookFileObject.BookFile == null)
                {
                    throw new BadHttpRequestException("Book does not contain a file", StatusCodes.Status400BadRequest);
                }
                else
                {
                    byte[] BookFile = bookObject.BookFileObject.BookFile;
                    string mimeType = "application/pdf";
                    return new FileContentResult(BookFile, mimeType);
                    
                }
            }
            else
            {
                throw new BadHttpRequestException("Book does not exist", StatusCodes.Status400BadRequest);
            }
        }

    }
}

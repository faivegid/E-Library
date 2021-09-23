using GBLAC.Data;
using GBLAC.Models;
using GBLAC.Models.Enums;
using GBLAC.Repository.APIRepository;
using GBLAC.Repository.APIRepository.Implementations;
using GBLAC.Repository.APIUnitOfWork.Interfaces;
using System;

namespace GBLAC.Repository.APIUnitOfWork.Implementaiton
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly GBlacContext _context;
        private IGenericRepository<Book> _books;
        private IGenericRepository<Category> _categories;
        private IGenericRepository<BookType> _bookTypes;

        public UnitOfWork(GBlacContext context)
        {
            _context = context;
        }
        public IGenericRepository<Book> Books => _books ??= new GenericRepository<Book>(_context);
        public IGenericRepository<Category> Categories => _categories ??= new GenericRepository<Category>(_context);
        public IGenericRepository<BookType> BookTypes => _bookTypes ??= new GenericRepository<BookType>(_context);

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}

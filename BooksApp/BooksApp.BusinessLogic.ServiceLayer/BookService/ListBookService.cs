using BooksApp.Infrastructure.Data;
using BooksApp.Infrastructure.Entities;

namespace BooksApp.BusinessLogic.ServiceLayer.BookService
{
    public class ListBookService
    {
        private readonly BooksAppDbContext booksAppDbContext;

        public ListBookService(BooksAppDbContext booksAppDbContext)
        {
            this.booksAppDbContext = booksAppDbContext;
        }

        public IQueryable<Book> GetBookList()
        {
            return booksAppDbContext.Books.Page(3, pageSize: 4);
        }
    }
}

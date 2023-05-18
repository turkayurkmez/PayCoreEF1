using BooksApp.Infrastructure.Data;
using BooksApp.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace BooksApp.BusinessDbAccess.Orders
{
    public interface IPlaceOrderDbAccess
    {
        IDictionary<int, Book> FindBooksByIdsWithPriceOffers(IEnumerable<int> bookIds);
        void Add(Order order);

    }
    /*
     * 4. Veritabanı erişim kodları ayrı bir projede yer almalı.
       5. İş mantığı doğrudan SaveChanges metodunu çağırmamalı. 
     */
    public class PlaceOrderDbAccess : IPlaceOrderDbAccess
    {
        private readonly BooksAppDbContext _context;

        public PlaceOrderDbAccess(BooksAppDbContext context)
        {
            _context = context;
        }

        public void Add(Order order)
        {
            _context.Orders.Add(order);
        }

        public IDictionary<int, Book> FindBooksByIdsWithPriceOffers(IEnumerable<int> bookIds)
        {
            return _context.Books.Where(book => bookIds.Contains(book.BookId))
                                 .Include(book => book.Promotion)
                                 .ToDictionary(key => key.BookId);

            /*
             * var query = from person in context.Persons
               where persons.Any(p => p.Name == person.Name && p.Age == person.Age)
               select person;

               
             */
        }
    }
}

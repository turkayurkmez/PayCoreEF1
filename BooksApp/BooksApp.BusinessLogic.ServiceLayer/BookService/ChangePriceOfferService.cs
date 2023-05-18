using BooksApp.Infrastructure.Data;
using BooksApp.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BooksApp.BusinessLogic.ServiceLayer.BookService
{
    public class ChangePriceOfferService
    {
        private readonly BooksAppDbContext _context;

        public ChangePriceOfferService(BooksAppDbContext context)
        {
            _context = context;
        }

        public Book OriginalBook { get; set; }
        public PriceOffer GetOriginalPrice(int id)
        {
            OriginalBook = _context.Books.Include(book => book.Promotion)
                                         .Single(book => book.BookId == id);

            return OriginalBook.Promotion ?? new PriceOffer { BookId = id, NewPrice = OriginalBook.Price };
        }

        public ValidationResult AddOrRemovePriceOffer(PriceOffer promotion)
        {
            var book = _context.Books.Include(book => book.Promotion)
                                     .Single(book => book.BookId == promotion.BookId);


            //promotion parametresi içinde belirtilen kitabın daha önceden bir promosyonu olabilir ya da olmayabilir....

            //eğer önceden bir promosyonu varsa


            if (string.IsNullOrEmpty(promotion.PromotionalText))
            {
                return new ValidationResult("Promosyon mesajı belirtilmelidir.", new[] { nameof(PriceOffer.PromotionalText) });

            }

            if (book.Promotion != null)
            {
                //öncekini kaldır.
                _context.Remove(book.Promotion);
                _context.SaveChanges();
            }

            //yeni promosyonu ekle:
            book.Promotion = promotion;
            _context.SaveChanges();

            return null;
        }

    }
}

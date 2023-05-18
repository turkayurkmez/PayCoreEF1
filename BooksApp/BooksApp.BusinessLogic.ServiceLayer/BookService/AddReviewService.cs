using BooksApp.Infrastructure.Data;
using BooksApp.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using StatusGeneric;

namespace BooksApp.BusinessLogic.ServiceLayer.BookService
{
    public class AddReviewService : IAddReviewService
    {
        private readonly BooksAppDbContext booksAppDbContext;

        public AddReviewService(BooksAppDbContext booksAppDbContext)
        {
            this.booksAppDbContext = booksAppDbContext;

        }

        public string BookTitle { get; private set; }

        public Book AddReviewToBook(Review review)
        {
            var book = booksAppDbContext.Books.Where(book => book.BookId == review.BookId)
                                              .Single(book => book.BookId == review.BookId);

            book.Reviews.Add(review);
            booksAppDbContext.SaveChanges();

            return book;

        }

        public Review GetBlankReview(int id)
        {
            BookTitle = booksAppDbContext.Books.Where(book => book.BookId == id)
                                               .Select(book => book.Title)
                                               .Single();

            return new Review { BookId = id };
        }

        public IStatusGeneric AddReviewWithControls(Review review)
        {
            var status = new StatusGenericHandler();
            if (review.Stars < 0 || review.Stars > 5)
            {
                status.AddError("Puan 0 ile 5 arasında olmalı", nameof(Review.Stars));
            }

            if (string.IsNullOrWhiteSpace(review.Comment))
            {
                status.AddError("Lütfen yorum yazınız....");
            }

            if (!status.IsValid)
            {
                return status;
            }

            var book = booksAppDbContext.Books.Include(book => book.Reviews)
                                              .First(book => book.BookId == review.BookId);

            book.Reviews.Add(review);
            booksAppDbContext.SaveChanges();

            return status;
        }


    }
}

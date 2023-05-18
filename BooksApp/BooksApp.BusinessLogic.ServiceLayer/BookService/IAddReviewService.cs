using BooksApp.Infrastructure.Entities;

namespace BooksApp.BusinessLogic.ServiceLayer.BookService
{
    public interface IAddReviewService
    {
        public string BookTitle { get; }
        Review GetBlankReview(int id);
        Book AddReviewToBook(Review review);

    }
}

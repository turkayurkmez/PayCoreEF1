using BooksApp.Infrastructure.Data;
using BooksApp.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace BooksApp.Application.Services
{
    public class QueryingService
    {


        private readonly BooksAppDbContext dbContext;

        public QueryingService(BooksAppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Book GetFirstBook()
        {
            //1. Eager Loading: İlişkileri, entity sınıfları (navigation property) üzerinden yükler:
            var firstBook = dbContext.Books.AsNoTracking()
                                           .Include(book => book.AuthorsLink)
                                                .ThenInclude(bookAuthor => bookAuthor.Author)
                                           .Include(book => book.Tags)
                                           .Include(book => book.Promotion)
                                           .Include(book => book.Reviews)
                                           .FirstOrDefault();

            firstBook.Price = 35;
            return firstBook;



        }

        public List<Book> GetBooks()
        {
            //1. Eager Loading: İlişkileri, entity sınıfları (navigation property) üzerinden yükler:
            var books = dbContext.Books.AsNoTracking()
                                           .Include(book => book.AuthorsLink)
                                                .ThenInclude(bookAuthor => bookAuthor.Author)
                                           .Include(book => book.Tags.Where(t => t.TagId == "Bilim-Kurgu"))
                                           .Include(book => book.Promotion)
                                           .Include(book => book.Reviews)
                                           .OrderBy(book => book.Title)
                                           .ThenBy(book => book.PublishedOn)
                                           .ToList();


            return books;



        }


        public Book GetFirstBookWithExplicit()
        {
            //2. Explicit Loading:
            var firstBook = dbContext.Books.First();
            //Bir nesnesinin belirli bir alanı ile çalıştıktıktan sonra; Direkt ilişkili alanları yüklemek için tercih edilebilir.
            dbContext.Entry(firstBook).Collection(book => book.AuthorsLink).Load();
            foreach (var authorLink in firstBook.AuthorsLink)
            {
                dbContext.Entry(authorLink).Reference(author => author.Author).Load();
            }

            dbContext.Entry(firstBook).Collection(book => book.Tags).Load();
            return firstBook;

        }

        public object GetBooksWithSelectLoading()
        {

            //Select Loading
            object books = dbContext.Books.Include(p => p.AuthorsLink)
                                          .ThenInclude(ba => ba.Author)
                                          .Select(book => new
                                          {
                                              book.Title,
                                              book.Price,
                                              AuthorName = book.AuthorsLink.ToList()[0].Author.Name

                                          }).ToList();

            return books;
        }

        public object GetBook()
        {
            var authors = dbContext.Books.Include(b => b.AuthorsLink)
                                         .ThenInclude(ba => ba.Author)
                                         .Select(a =>
                                         string.Join(",", a.AuthorsLink
                                                           .OrderBy(q => q.Order)
                                                           .Select(x => x.Author.Name)))
                                         .First();


            var tags = dbContext.Books.Select(b => b.Tags.ToArray()).First();

            return new { Authors = authors, Tags = tags };
        }


        public void AddTag(string tag)
        {
            var tagEntity = new Tag { TagId = tag };
            dbContext.Tags.Add(tagEntity);

            //var sample = new Tag { TagId = "Araştırma" };
            //dbContext.Tags.Add(sample);

            //var author = dbContext.Authors.FirstOrDefault(a => a.AuthorId == 1);
            //author.Name = "Jule Verne";


            //dbContext.Authors.Update(author);

            dbContext.SaveChanges();




        }

        public void AddReviewToBook(int id, string comment)
        {
            var review = new Review { Stars = 5, Comment = comment, VoterName = "türkay" };

            var book = new Book
            {
                Title = "Test Book",
                Description = "Test Desc",
                Price = 10,
                Reviews = new List<Review> { new Review { Comment = "TestComment", Stars = 5, VoterName = "abc" } }

            };

            dbContext.Books.Add(book);
            dbContext.Books.FirstOrDefault(b => b.BookId == id)?.Reviews.Add(review);
            dbContext.SaveChanges();



        }

        public void Update(Book book)
        {

            //1. book.BookId YOKSA Ekler VARSA günceller
            //2. Yalnızca bir satır üzerinde çalışılır.
            dbContext.Books.Update(book);
            dbContext.SaveChanges();
        }

        public void DeleteReview(int id)
        {
            var first = dbContext.Books.First();
            var review = first.Reviews.FirstOrDefault(p => p.ReviewId == 1);
            first.Reviews.Remove(review);
            dbContext.SaveChanges();


        }


    }
}

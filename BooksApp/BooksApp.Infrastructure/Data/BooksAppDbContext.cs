using BooksApp.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace BooksApp.Infrastructure.Data
{
    public class BooksAppDbContext : DbContext
    {
        public BooksAppDbContext(DbContextOptions<BooksAppDbContext> options) : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<PriceOffer> PriceOffers { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /* Fluent API */
            modelBuilder.Entity<BookAuthor>()
                        .HasKey(ba => new { ba.AuthorId, ba.BookId });

            modelBuilder.Entity<Book>()
                        .HasMany(b => b.AuthorsLink)
                        .WithOne(al => al.Book)
                        .HasForeignKey(b => b.BookId);

            modelBuilder.Entity<Author>()
                        .HasMany(a => a.BooksLink)
                        .WithOne(bl => bl.Author)
                        .HasForeignKey(a => a.AuthorId);


            modelBuilder.Entity<LineItem>()
                        .HasOne(l => l.ChoosenBook)
                        .WithMany()
                        .OnDelete(DeleteBehavior.Restrict);


            var julesVerne = new Author
            {
                Name = "Jules Verne",
                AuthorId = 1

            };
            var alperCaniguz = new Author
            {
                Name = "Alper Canıgüz",
                AuthorId = 2

            };

            modelBuilder.Entity<Author>().HasData(julesVerne, alperCaniguz);

            var books = new List<Book>()
            {
                new(){
                    BookId =1,
                    Title = "Esrarlı Ada",
                    //AuthorsLink = new List<BookAuthor>(){ new BookAuthor { BookId = 1, AuthorId =1} },
                    Description="6 Kişi Issız bir adaya düşer...",
                    PublishedOn= DateTime.Now
                } ,
                new(){
                    BookId = 2,
                    Title = "Kip Kardeşler",
                    // AuthorsLink = new List<BookAuthor>(){ new BookAuthor { BookId = 2, AuthorId =1} },
                    Description="İki dedektif kardeşin maceraları",
                    PublishedOn= DateTime.Now
                },
                new(){
                    BookId =3,
                    Title = "Karpatlar Şatosu",
                    //AuthorsLink = new List<BookAuthor>(){ new BookAuthor { BookId = 3, AuthorId =1} },
                    Description="Televizyonu öngören.....",
                    PublishedOn= DateTime.Now
                },
                new(){
                    BookId = 4,
                    Title = "Oğullar ve Rencide Ruhlar",
                    //AuthorsLink = new List<BookAuthor>(){ new BookAuthor { BookId = 4, AuthorId =1} },
                    Description="5 Yaşındaki Alper Kamu'nun başınan geçenler :)",
                    PublishedOn= DateTime.Now
                }

            };

            modelBuilder.Entity<Book>().HasData(books);
        }

    }
}

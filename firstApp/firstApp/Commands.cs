using firstApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace firstApp
{
    public static class Commands
    {
        public static bool CreateDbAndSeedData(bool onlyIfNoDatabase)
        {
            using var db = new AppDbContext();
            if (onlyIfNoDatabase && (db.GetService<IDatabaseCreator>() as RelationalDatabaseCreator).Exists())
            {
                return false;
            }

            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();

            if (!db.Books.Any())
            {
                SeedData(db);
                Console.WriteLine("Database içerisine veri ekleniyor....");

            }
            return true;

        }

        private static void SeedData(AppDbContext db)
        {
            var julesVerne = new Author
            {
                Name = "Jules Verne",
                WebUrl = "http://www.julesverne.com"
            };

            var books = new List<Book>()
            {
                new(){
                    Title = "Esrarlı Ada",
                    Author=julesVerne,
                    Description="6 Kişi Issız bir adaya düşer...",
                    PublishedOn= DateTime.Now
                } ,
                new(){
                    Title = "Kip Kardeşler",
                    Author=julesVerne,
                    Description="İki dedektif kardeşin maceraları",
                    PublishedOn= DateTime.Now
                },
                new(){
                    Title = "Karpatlar Şatosu",
                    Author=julesVerne,
                    Description="Televizyonu öngören.....",
                    PublishedOn= DateTime.Now
                },
                new(){
                    Title = "Oğullar ve Rencide Ruhlar",
                    Author=new Author{ Name="Alper Canıgüz"},
                    Description="5 Yaşındaki Alper Kamu'nun başınan geçenler :)",
                    PublishedOn= DateTime.Now
                }

            };

            db.Books.AddRange(books);
            db.SaveChanges();
        }

        public static void ListAll()
        {
            using var db = new AppDbContext();
            foreach (var book in db.Books.AsNoTracking().Include(book => book.Author))
            {
                var webUrl = book.Author.WebUrl == null ? "- web adresi yok -" : book.Author.WebUrl;
                Console.WriteLine($"{book.Title}: {book.Author.Name} tarafından yazılmıştır. Yazartın adresi: {webUrl} ");

            }
        }

        public static void ChangeWebUrl()
        {
            Console.Write("Alper Canıgüz'ün web sitesi:");
            var newWebUrl = Console.ReadLine();

            using var db = new AppDbContext();
            var singleBook = db.Books.Include(b => b.Author).Single(book => book.Title == "Oğullar ve Rencide Ruhlar");
            singleBook.Author.WebUrl = newWebUrl;
            db.SaveChanges();
            ListAll();



        }
    }
}


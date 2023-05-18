using BooksApp.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BooksApp.Infrastructure.Data.Configurations
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.Property(book => book.ImageUrl).HasMaxLength(5000);
            builder.Property(book => book.PublishedOn).HasColumnType("date");
            builder.Property(b => b.Price).HasPrecision(9, 2);
            builder.Property(b => b.PublishedOn).ValueGeneratedOnAddOrUpdate();
            builder.HasIndex(b => b.PublishedOn);
        }
    }
}

using System.ComponentModel.DataAnnotations;

namespace BooksApp.Infrastructure.Entities
{
    public class Tag
    {
        [Key]
        [MaxLength(40)]
        public string TagId { get; set; }
        public ICollection<Book> Books { get; set; }
    }
}
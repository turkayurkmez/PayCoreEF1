using System.ComponentModel.DataAnnotations;

namespace BooksApp.Infrastructure.Entities
{
    public class LineItem : IValidatableObject
    {
        public int LineItemId { get; set; }
        [Range(1, 5, ErrorMessage = "Sadece 5 kitap ile sınırlısınız")]
        public byte LineNumber { get; set; }

        public decimal BookPrice { get; set; }
        public short NumberOfBooks { get; set; }

        public int OrderId { get; set; }
        public Book ChoosenBook { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            //Specification Pattern:
            // Ayrtıntı ve örnek : https://en.wikipedia.org/wiki/Specification_pattern
            //

            if (ChoosenBook.Price <= 0)
            {
                yield return new ValidationResult($"Üzgünüz ancak {ChoosenBook.Title} kitabı satılık değil!");
            }

            if (NumberOfBooks > 100)
            {
                yield return new ValidationResult($"100'den fazla sipariş vermek için eposta adresimiz...");
            }
        }
    }
}
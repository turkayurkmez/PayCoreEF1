namespace BooksApp.Domains.Books.DomainEvents
{

    public interface IEntiyEvent { }
    public class ChangeBookTitleEvent : IEntiyEvent
    {
        public Book Book { get; set; }
        public string NewTitle { get; set; }

    }
}

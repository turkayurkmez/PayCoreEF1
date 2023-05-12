namespace firstApp.Models
{
    //Convention Over Configuration
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public DateTime? PublishedOn { get; set; }
        public int AuthorId { get; set; }
        //Navigation Property:
        public Author Author { get; set; }




    }
}

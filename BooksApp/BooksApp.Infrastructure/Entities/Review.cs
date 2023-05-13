namespace BooksApp.Infrastructure.Entities
{
    public class Review
    {
        public int ReviewId { get; set; }
        public string VoterName { get; set; }
        public int Stars { get; set; }
        public string Comment { get; set; }

        public int BookId { get; set; }


    }


}
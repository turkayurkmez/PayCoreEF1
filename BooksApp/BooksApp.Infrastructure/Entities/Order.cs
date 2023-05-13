namespace BooksApp.Infrastructure.Entities
{
    public class Order
    {
        public int OrderId { get; set; }
        public DateTime OrderedDate { get; set; }
        public Guid CustomerId { get; set; }

        public ICollection<LineItem> LineItems { get; set; }

        public string OrderNumber => $"BO{OrderId}";
        public Order()
        {
            OrderedDate = DateTime.UtcNow;
        }
    }
}

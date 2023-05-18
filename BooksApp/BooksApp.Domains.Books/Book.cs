using BooksApp.Domains.Books.DomainEvents;

namespace BooksApp.Domains.Books
{
    public class Book
    {
        //Read-only özellikler:
        public int BookId { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public DateTime? PublishedOn { get; private set; }

        public void SetBookDetails(string title, string description, DateTime? publishedOn)
        {
            /*
             * Bir varlık (Entity), başla bir nesneyi barındırabilir. Fakat bu nesne tek başına var olamıyorsa Value Object'dir (Employee.Address, Customer.Address) 
             * 
             * Aggregate: Başka entity'leri yöneten entity Aggregate'dir.
             * Sipariş olması için müşteri olmalı, satın alınan ürün olmalı. 
             * 
             * customer.CreateOrder(new Product());
             * order.LineItems.Add(product);
             */

        }

        public void ChangeTitle(string title)
        {
            Title = title;
            AddEvent(new ChangeBookTitleEvent { Book = this, NewTitle = title });
        }

        private void AddEvent(ChangeBookTitleEvent changeBookTitleEvent)
        {
            events.Add(changeBookTitleEvent);
        }

        private List<IEntiyEvent> events = new List<IEntiyEvent>();
    }
}
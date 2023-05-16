using BooksApp.BusinessDbAccess.Orders;
using BooksApp.BusinessLogic.GenericInterfaces;
using BooksApp.BusinessLogic.Orders;
using BooksApp.Infrastructure.Entities;

namespace BooksApp.BusinessLogic.Concrete
{
    public class PlaceOrderAction : BusinessActionErrors, IBusinessAction<PlaceOrderRequestDto, Order>
    {
        /*
         * 2. İş mantığı hiçbir dikkat dağıtıcıya sahip olmamalıdır. Sadece Entity'ler ile birlikte çalışır. 
           3. İş mantığı, bellek üzerindeki data ile çalışıyormuş gibi gözükmeli.  
         */
        private readonly IPlaceOrderDbAccess _dbAccess;

        public PlaceOrderAction(IPlaceOrderDbAccess dbAccess)
        {
            _dbAccess = dbAccess;
        }

        public Order? Action(PlaceOrderRequestDto dto)
        {
            if (!dto.AcceptTermsAndConditions)
            {
                AddError("Satış sözleşmesini imzalamadan sipariş gerçekleşemez...");
                return null;
            }

            if (!dto.LineItems.Any())
            {
                AddError("Sipariş edeceğiniz kitap listesi yok!");
                return null;
            }

            //Kitaba eriş....
            //sipariş nesnesini oluştur....
            //ve nesneyi döndür.

            var bookDictionary = _dbAccess.FindBooksByIdsWithPriceOffers(dto.LineItems.Select(b => b.BookId));

            var order = new Order
            {
                CustomerId = dto.UserId,
                LineItems = FormLineItemsWithErrorChecking(dto.LineItems, bookDictionary)

            };
            if (!HasErrors)
            {
                _dbAccess.Add(order);
            }
            return HasErrors ? null : order;
        }

        private List<LineItem> FormLineItemsWithErrorChecking(IEnumerable<OrderLineItem> lineItems, IDictionary<int, Book> booksDictionary)
        {
            var result = new List<LineItem>();
            int orderIndex = 1;
            foreach (var lineItem in lineItems)
            {
                if (!booksDictionary.ContainsKey(lineItem.BookId))
                {
                    throw new InvalidOperationException($"Sipariş hatalı. Çünkü {lineItem.BookId} değeri yok.");
                }

                var book = booksDictionary[lineItem.BookId];
                var bookPrice = book.Promotion?.NewPrice ?? book.Price;

                if (bookPrice <= 0)
                {
                    AddError($"{book.Title} kitabının fiyatı uyumlu değil");
                }
                else
                {
                    result.Add(new LineItem { BookPrice = bookPrice, ChoosenBook = book, LineNumber = (byte)(orderIndex++), NumberOfBooks = lineItem.NumberOfBooks });
                }


            }

            return result;


        }
    }
}

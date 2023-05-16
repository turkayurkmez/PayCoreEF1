using BooksApp.BusinessDbAccess.Orders;
using BooksApp.BusinessLogic.Concrete;
using BooksApp.BusinessLogic.Orders;
using BooksApp.Infrastructure.Data;
using BooksApp.Infrastructure.Entities;
using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;

namespace BooksApp.BusinessLogic.ServiceLayer.OrderServices
{
    public class PlaceOrderService
    {
        private readonly RunnerWriteDb<PlaceOrderRequestDto, Order> _runner;
        public IImmutableList<ValidationResult> Errors => _runner.Errors;

        public PlaceOrderService(BooksAppDbContext dbContext)
        {
            var dbAccess = new PlaceOrderDbAccess(dbContext);
            var orderAction = new PlaceOrderAction(dbAccess);

            _runner = new RunnerWriteDb<PlaceOrderRequestDto, Order>(orderAction, dbContext);
        }


        public int PlaceOrder(bool acceptTermsAndConditions)
        {

            //TODO 2: Hangi kulanıcı (Guid) hangi order line'ları satın alıyor?
            var mockOrderLines = new List<OrderLineItem>()
            {
                 new(){ BookId=1, NumberOfBooks=3},
                 new(){ BookId=2, NumberOfBooks=1},
            }.ToImmutableList();
            var userId = Guid.NewGuid();

            var order = _runner.RunAction(new PlaceOrderRequestDto(acceptTermsAndConditions, userId, mockOrderLines));
            if (_runner.HasErrors)
            {
                return 0;
            }

            return order.OrderId;


        }
    }
}

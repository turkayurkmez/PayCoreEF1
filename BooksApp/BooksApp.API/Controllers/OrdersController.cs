using BooksApp.BusinessLogic.ServiceLayer.OrderServices;
using Microsoft.AspNetCore.Mvc;

namespace BooksApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly PlaceOrderService placeOrderService;

        public OrdersController(PlaceOrderService placeOrderService)
        {
            this.placeOrderService = placeOrderService;
        }

        [HttpPost]
        public IActionResult SaveOrder(bool isAccept)
        {
            var responseOrderId = placeOrderService.PlaceOrder(true);
            return Ok(responseOrderId);
        }
    }
}

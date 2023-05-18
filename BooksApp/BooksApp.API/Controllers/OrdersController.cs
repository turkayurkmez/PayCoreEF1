using BooksApp.BusinessLogic.ServiceLayer.BookService;
using BooksApp.BusinessLogic.ServiceLayer.OrderServices;
using BooksApp.Infrastructure.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BooksApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly PlaceOrderService placeOrderService;
        private readonly ChangePriceOfferService changePriceOfferService;
        private readonly AddReviewService addReviewService;

        public OrdersController(PlaceOrderService placeOrderService, ChangePriceOfferService changePriceOfferService, AddReviewService addReviewService)
        {
            this.placeOrderService = placeOrderService;
            this.changePriceOfferService = changePriceOfferService;
            this.addReviewService = addReviewService;
        }

        [HttpPost]
        public IActionResult SaveOrder(bool isAccept)
        {
            var responseOrderId = placeOrderService.PlaceOrder(true);
            return Ok(responseOrderId);
        }

        [HttpPost("[action]")]
        public IActionResult ChangePromotion()
        {
            PriceOffer priceOffer = new PriceOffer { BookId = 1, NewPrice = 8, PromotionalText = "Jules Verne kitaplarında indirim!" };
            changePriceOfferService.AddOrRemovePriceOffer(priceOffer);
            return Ok($"Fiyat Güncellendi: {priceOffer.NewPrice} TL");
        }

        [HttpPost("[action]/{bookId}")]
        public IActionResult AddReviewForBook(int bookId, string comment)
        {
            var review = new Review { BookId = bookId, Comment = comment, VoterName = "Türkay", Stars = 6 };
            var status = addReviewService.AddReviewWithControls(review);
            if (status.IsValid)
            {
                return Ok(review);
            }
            var errors = status.Errors.ToList();
            var results = errors.Select(e => e.ErrorResult.ErrorMessage);
            return BadRequest(new { message = status.Message, errorMessages = string.Join(',', results) });
        }
    }
}

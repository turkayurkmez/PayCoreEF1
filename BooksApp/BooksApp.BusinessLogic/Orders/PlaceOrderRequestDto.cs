using System.Collections.Immutable;

namespace BooksApp.BusinessLogic.Orders
{
    public class PlaceOrderRequestDto
    {
        public PlaceOrderRequestDto(bool acceptTermsAndConditions, Guid userId, IImmutableList<OrderLineItem> orderLines)
        {
            AcceptTermsAndConditions = acceptTermsAndConditions;
            UserId = userId;
            LineItems = orderLines;

        }

        public bool AcceptTermsAndConditions { get; set; }
        public Guid UserId { get; set; }
        public IImmutableList<OrderLineItem> LineItems { get; set; }
    }
}

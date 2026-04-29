

using Basket.API.Data;

namespace Basket.API.Features.Basket.Query.GetBasket
{
    public record GetBasketQuery(string UserName) : IQuery<GetBasketResult>;
    public record GetBasketResult(ShoppingCart Cart);

    public class GetBasketHandler(IBasketRepository repository) : IQueryHandler<GetBasketQuery, GetBasketResult>
    {
        public async Task<GetBasketResult> Handle(GetBasketQuery query, CancellationToken cancellationToken)
        {
            // Get Basket from database
            var basket = await repository.GetBasket(query.UserName);

            return new GetBasketResult(basket);
        }
    }
}

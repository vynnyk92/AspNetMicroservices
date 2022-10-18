using Basket.Api.Entities;

namespace Basket.Api.Repositories
{
    public interface IBasketRepository
    {
        Task<ShoppingCart?> GetCart(string userName);
        Task<ShoppingCart> UpdateCart(ShoppingCart cart);
        Task DeleteCart(string userName);
    }
}

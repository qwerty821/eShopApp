using Basket.API.Models;

namespace Basket.API.Abstractions
{
    public interface IBasketRepository
    {
        public Task<BasketItem> Get(string id);
        public Task<List<BasketItem>> GetAll();
        public void Add();

    }
}

using Codecool.CodecoolShop.Models;

namespace Codecool.CodecoolShop.Repository.IRepository
{
    public interface ICartRepository : IRepository<Cart>
    {
        void Update(Cart obj);
    }
}

using Codecool.CodecoolShop.Data;
using Codecool.CodecoolShop.Models;
using Codecool.CodecoolShop.Repository.IRepository;

namespace Codecool.CodecoolShop.Repository
{
    public class CartRepository : Repository<Cart>, ICartRepository
    {
        private readonly CodeCoolDBContext _db;

        public CartRepository(CodeCoolDBContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Cart obj)
        {
            _db.Update(obj);
        }
    }
}

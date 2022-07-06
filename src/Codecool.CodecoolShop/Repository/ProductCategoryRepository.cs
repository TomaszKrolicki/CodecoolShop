using Codecool.CodecoolShop.Data;
using Codecool.CodecoolShop.Models;
using Codecool.CodecoolShop.Repository.IRepository;

namespace Codecool.CodecoolShop.Repository
{
    public class ProductCategoryRepository : Repository<ProductCategory>, IProductCategoryRepository
    {
        private readonly CodeCoolDBContext _db;

        public ProductCategoryRepository(CodeCoolDBContext db) : base(db)
        {
            _db = db;
        }

        public void Update(ProductCategory obj)
        {
            _db.Update(obj);
        }
    }
}

using Codecool.CodecoolShop.Models;

namespace Codecool.CodecoolShop.Repository.IRepository
{
    public interface IProductCategoryRepository : IRepository<ProductCategory>
    {
        void Update(ProductCategory obj);
    }
    
    }

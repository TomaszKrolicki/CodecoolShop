using System.Collections.Generic;
using Codecool.CodecoolShop.Models;

namespace Codecool.CodecoolShop.Repository.IRepository
{
    public interface IProductRepository : IRepository<Product>
    {
        void Update(Product obj);
        IEnumerable<Product> GetProductsForCategory(int categoryId);
        IEnumerable<Product> GetProductsForSupplier(int supplierId);
    }
}

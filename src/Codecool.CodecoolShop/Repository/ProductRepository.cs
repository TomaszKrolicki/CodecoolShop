using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Codecool.CodecoolShop.Data;
using Codecool.CodecoolShop.Models;
using Codecool.CodecoolShop.Repository.IRepository;

namespace Codecool.CodecoolShop.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly CodeCoolDBContext _db;

        public ProductRepository(CodeCoolDBContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Product obj)
        {
            _db.Update(obj);
        }

        public IEnumerable<Product> GetProductsForCategory(int categoryId)
        {
            return _db.Products.Where(e => e.ProductCategory.Id == categoryId);
        }
        public IEnumerable<Product> GetProductsForSupplier(int supplierId)
        {
            return _db.Products.Where(e => e.Supplier.Id == supplierId);
        }
    }
}

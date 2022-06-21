using System.Collections.Generic;
using System.Linq;
using Codecool.CodecoolShop.Daos;
using Codecool.CodecoolShop.Models;

namespace Codecool.CodecoolShop.Services
{
    public class ProductService
    {
        private readonly IProductDao productDao;
        private readonly IProductCategoryDao productCategoryDao;
        private readonly ISupplierDao productSupplierDao;

        public ProductService(IProductDao productDao, IProductCategoryDao productCategoryDao, ISupplierDao productSupplierDao)
        {
            this.productDao = productDao;
            this.productCategoryDao = productCategoryDao;
            this.productSupplierDao = productSupplierDao;
        }

        public ProductCategory GetProductCategory(int categoryId)
        {
            return this.productCategoryDao.Get(categoryId);
        }

        public IEnumerable<Product> GetProductsForCategory(int categoryId)
        {
            ProductCategory category = this.productCategoryDao.Get(categoryId);
            return this.productDao.GetBy(category);
        }

        public IEnumerable<Product> GetProductsForSupplier(int supplierId)
        {
            Supplier supplier = this.productSupplierDao.Get(supplierId);
            return this.productDao.GetBy(supplier);
        }
        public IEnumerable<Product> GetProductsForSupplierAndCategory(int supplierId, int categoryId)
        {
            var productsForCategory = GetProductsForCategory(categoryId);
            var productsForSupplier = GetProductsForSupplier(supplierId);
            return productsForSupplier.Intersect(productsForCategory);
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return this.productDao.GetAll();
        }
    }
}

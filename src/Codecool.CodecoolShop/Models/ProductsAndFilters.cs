using System.Collections.Generic;

namespace Codecool.CodecoolShop.Models
{
    public class ProductsAndFilters
    {
        public List<Product> AllProducts { get; set; }
        public IEnumerable<Supplier> AllSuppliers { get; set; }
        public IEnumerable<ProductCategory> AllProductCategories { get; set; }
    }
}

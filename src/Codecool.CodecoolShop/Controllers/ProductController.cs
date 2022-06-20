using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Codecool.CodecoolShop.Daos;
using Codecool.CodecoolShop.Daos.Implementations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Codecool.CodecoolShop.Models;
using Codecool.CodecoolShop.Services;

namespace Codecool.CodecoolShop.Controllers
{
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;
        public ProductService ProductService { get; set; }

        public ProductController(ILogger<ProductController> logger)
        {
            _logger = logger;
            ProductService = new ProductService(
                ProductDaoMemory.GetInstance(),
                ProductCategoryDaoMemory.GetInstance());
        }

        public IActionResult Index(int categoryId, int supplierId)
        {
            if (categoryId == 0 && supplierId == 0)
            {
                var allProducts = ProductService.GetAllProducts();
                return View(allProducts.ToList());
            }
            else if (categoryId != 0 && supplierId == 0)
            {
                var productsForCategory = ProductService.GetProductsForCategory(categoryId);
                return View(productsForCategory.ToList());
            }
            else if (categoryId == 0 && supplierId != 0)
            {
                var productsForSupplier = ProductService.GetProductsForSupplier(supplierId);
                return View(productsForSupplier.ToList());
            }
            var products = ProductService.GetProductsForSupplierAndCategory(supplierId, categoryId);
            return View(products.ToList());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

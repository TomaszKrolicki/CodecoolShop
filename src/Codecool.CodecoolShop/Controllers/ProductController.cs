using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
        public UserService UserService { get; set; }
        public OrderService OrderService { get; set; }

        private ProductsAndFilters _productsAndFilters;


        public ProductController(ILogger<ProductController> logger)
        {
            _logger = logger;
            ProductService = new ProductService(
                ProductDaoMemory.GetInstance(),
                ProductCategoryDaoMemory.GetInstance(),
                SupplierDaoMemory.GetInstance());
            UserService = new UserService(UserDaoMemory.GetInstance());
            OrderService = new OrderService(OrderDaoMemory.GetInstance());
        }

        private IEnumerable<Product> GetFilteredProducts(int categoryId, int supplierId)
        {
            if (categoryId == 0 && supplierId == 0)
            {
                return ProductService.GetAllProducts();
            }
            else if (categoryId != 0 && supplierId == 0)
            {
                return ProductService.GetProductsForCategory(categoryId);
            }
            else if (categoryId == 0 && supplierId != 0)
            {
                return ProductService.GetProductsForSupplier(supplierId);
            } 
            return ProductService.GetProductsForSupplierAndCategory(supplierId, categoryId);
                
        }

        public IActionResult Index(int categoryId, int supplierId, int orderedProductId = -1, bool ordered = false)
        {
            var user = UserService.GetUser(1);
            if (ordered)
            {
                user.ShoppingCart = new List<Product>();
                user.ShoppingCartValue = 0;
            }
            if (orderedProductId != -1)
            {
                var allProducts = GetFilteredProducts(0, 0);
                var orderedProduct = allProducts.First(e => e.Id == orderedProductId);
                if (user.ShoppingCart.Any(p=>p.Id == orderedProductId))
                {
                    var product = user.ShoppingCart.First(e => e.Id == orderedProductId);
                    product.Quantity++;
                    if (product.Quantity > product.MaxInStock)
                    {
                        product.Quantity = product.MaxInStock;
                    }
                }
                else
                {
                    user.ShoppingCart.Add(new Product { Id = orderedProduct.Id, Name = orderedProduct.Name, DefaultPrice = orderedProduct.DefaultPrice,
                        Currency = orderedProduct.Currency, Quantity = 1, MaxInStock = orderedProduct.MaxInStock, 
                        Description = orderedProduct.Description, ProductCategory = orderedProduct.ProductCategory, 
                        Supplier = orderedProduct.Supplier });
                }

                user.ShoppingCartValue += orderedProduct.DefaultPrice;

            }
            _productsAndFilters = new ProductsAndFilters
            {
                AllProducts = GetFilteredProducts(categoryId, supplierId).ToList(),
                AllProductCategories = ProductCategoryDaoMemory.GetInstance().GetAll(),
                AllSuppliers = SupplierDaoMemory.GetInstance().GetAll()
            };
            return View(_productsAndFilters);
        }

        public IActionResult OrderDetails()
        {
            var newestOrder = OrderService.GetNewestOrder();
            var allProducts = GetFilteredProducts(0, 0);
            foreach (Product product in newestOrder.User.ShoppingCart)
            {
                allProducts.First(e => e.Id == product.Id).Quantity -= product.Quantity;
            }
            var user = UserService.GetUser(1);
            return View(newestOrder);
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult ShoppingCart()
        {
            var user = UserService.GetUser(1);
            return View(user);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public IActionResult Checkout()
        {
            return View();
        }
        
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Checkout([Bind("FirstName,LastName,Email,PhoneNumber," +
                                                        "BillingCountry, BillingCity, BillingZip, BillingAddress, " +
                                                        "ShippingCountry, ShippingCity, ShippingZip, ShippingAddress", "IsPayedNow")] UserDataToCheck userData)
        {
            if (ModelState.IsValid)
            {
                User currentUser = UserService.GetUser(1);
                Order newOrder = new Order(currentUser, userData);
                IAllOrdersDao ordersDataStore = OrderDaoMemory.GetInstance();
                ordersDataStore.Add(newOrder);
                if (newOrder.UserPersonalInformation.IsPayedNow)
                {
                    //return RedirectToAction(nameof(Payment));
                }
                return RedirectToAction(nameof(OrderDetails));
            }
            return View(userData);
        }
    }
}

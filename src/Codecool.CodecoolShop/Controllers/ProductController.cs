using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Codecool.CodecoolShop.Daos;
using Codecool.CodecoolShop.Daos.Implementations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Codecool.CodecoolShop.Models;
using Codecool.CodecoolShop.Repository.IRepository;
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

        //Stworzyæ obiekt koszyk i do niego dodawaæ shoppigcartvalue i po zakupach go clearowaæ 
        // dodaæ coœ co obs³uguje to repository/unitofwork 

        // _unitOfWork.Order.Get(0);  -> powinno dzia³aæ dla order id 0 

        private readonly IUnitOfWork _unitOfWork;
        



        public ProductController(ILogger<ProductController> logger, IUnitOfWork unitOfWork) //
        {
            _logger = logger;
            ProductService = new ProductService(
                ProductDaoMemory.GetInstance(),
                ProductCategoryDaoMemory.GetInstance(),
                SupplierDaoMemory.GetInstance());
            UserService = new UserService(UserDaoMemory.GetInstance());
            OrderService = new OrderService(OrderDaoMemory.GetInstance());
            _unitOfWork = unitOfWork;
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

        public IActionResult Index(int categoryId, int supplierId, int orderedProductId = -1)
        {
            
            var user = UserService.GetUserByName("Janusz");
            //int cos = 1;
            //if (User.Identity.IsAuthenticated)
            //{
            //    cos = 3;
            //}
            if (orderedProductId != -1)
            {
                var allProducts = GetFilteredProducts(0, 0);
                var orderedProduct = allProducts.First(e => e.Id == orderedProductId);
                if (user.ShoppingCart.Any(p=>p.Id == orderedProductId))
                {
                    var product = user.ShoppingCart.First(e => e.Id == orderedProductId);
                    product.Quantity++;
                    user.ShoppingCartValue += orderedProduct.DefaultPrice;
                    if (product.Quantity > product.MaxInStock)
                    {
                        var numberToDelete = product.Quantity - product.MaxInStock;
                        user.ShoppingCartValue -= numberToDelete * orderedProduct.DefaultPrice;
                        product.Quantity = product.MaxInStock;
                    }
                }
                else
                {
                    user.ShoppingCart.Add(new Product { Id = orderedProduct.Id, Name = orderedProduct.Name, DefaultPrice = orderedProduct.DefaultPrice,
                        Currency = orderedProduct.Currency, Quantity = 1, MaxInStock = orderedProduct.MaxInStock, 
                        Description = orderedProduct.Description, ProductCategory = orderedProduct.ProductCategory, 
                        Supplier = orderedProduct.Supplier });
                    user.ShoppingCartValue += orderedProduct.DefaultPrice;
                }

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
            //var newestOrder = OrderService.GetNewestOrder();
            var newestOrder = _unitOfWork.Order.Get(0);
            var allProducts = GetFilteredProducts(0, 0);
            foreach (Product product in newestOrder.User.ShoppingCart)
            {
                allProducts.First(e => e.Id == product.Id).Quantity -= product.Quantity;
                allProducts.First(e => e.Id == product.Id).MaxInStock -= product.Quantity;
            }

            var userData = newestOrder.User;
            var userAddress = newestOrder.User;
            OrderForDelete orderCopy = new OrderForDelete()
            {
                Name = userData.Name, UserId = userData.Id,
                ShoppingCart = userData.ShoppingCart, ShoppingCartValue = userData.ShoppingCartValue,
                FirstName = userAddress.FirstName, LastName = userAddress.LastName, Email = userAddress.Email,
                PhoneNumber = userAddress.PhoneNumber, BillingAddress = userAddress.BillingAddress, BillingCity = userAddress.BillingCity,
                BillingCountry = userAddress.BillingCountry, BillingZip = userAddress.BillingZip,
                ShippingAddress = userAddress.ShippingAddress, ShippingCity = userAddress.ShippingCity,
                ShippingCountry = userAddress.ShippingCountry, ShippingZip = userAddress.ShippingZip, OrderId = newestOrder.Id,
                OrderDateTime = newestOrder.OrderDateTime, IsPayed = newestOrder.IsPayedNow
            };
            var mail = new MailSenderService();
            
            mail.MailSender(orderCopy);
            newestOrder.User.ShoppingCart = new List<Product>();
            newestOrder.User.ShoppingCartValue = 0;
            return View(orderCopy);
        }

        public IActionResult Privacy()
        {
            return View();
        }
        [HttpGet]
        public IActionResult ShoppingCart()
        {
            var user = UserService.GetUser(1);
            return View(user);
        }

        //public IActionResult ShoppingCartCheckout(User user)
        //{
        //    return RedirectToAction(nameof(Checkout));
        //}

        public IActionResult PlusQuantity(int id)
        {
            var user = UserService.GetUserByName("Janusz");
            var product = user.ShoppingCart.FirstOrDefault(e => e.Id == id);
            if (product.Quantity == product.MaxInStock)
            {
                product.Quantity = product.MaxInStock;
            }
            else
            {
                product.Quantity++;
                user.ShoppingCartValue += product.DefaultPrice;
            }
                
            return RedirectToAction(nameof(ShoppingCart));
        }

        public IActionResult MinusQuantity(int id)
        {
            var user = UserService.GetUserByName("Janusz");
            var product = user.ShoppingCart.FirstOrDefault(e => e.Id == id);
            product.Quantity--;
            user.ShoppingCartValue -= product.DefaultPrice;
            if (product.Quantity == 0)
            {
                user.ShoppingCart.Remove(product);
            }
            return RedirectToAction(nameof(ShoppingCart));
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
            User currentUser = UserService.GetUserByName("Janusz");
            Order newOrder = new Order();
            newOrder.IsPayedNow = userData.IsPayedNow;
            newOrder.OrderDateTime = DateTime.Now;
            
            IAllOrdersDao ordersDataStore = OrderDaoMemory.GetInstance();
            ordersDataStore.Add(newOrder);
            currentUser.FirstName = userData.FirstName;
            currentUser.LastName = userData.LastName;
            currentUser.Email = userData.Email;
            currentUser.PhoneNumber = userData.PhoneNumber;
            currentUser.BillingAddress = userData.BillingAddress;
            currentUser.BillingCity = userData.BillingCity;
            currentUser.BillingCountry = userData.BillingCountry;
            currentUser.BillingZip = userData.BillingZip;
            currentUser.ShippingAddress = userData.ShippingAddress;
            currentUser.ShippingCity = userData.ShippingCity;
            currentUser.ShippingCountry = userData.ShippingCountry;
            currentUser.ShippingZip = userData.ShippingZip;
            var userDat = currentUser;
            newOrder.User = currentUser;
            OrderForDelete orderCopy = new OrderForDelete()
            {
                Name = userDat.Name,
                UserId = userDat.Id,
                ShoppingCart = userDat.ShoppingCart,
                ShoppingCartValue = userDat.ShoppingCartValue,
                FirstName = userData.FirstName,
                LastName = userData.LastName,
                Email = userData.Email,
                PhoneNumber = userData.PhoneNumber,
                BillingAddress = userData.BillingAddress,
                BillingCity = userData.BillingCity,
                BillingCountry = userData.BillingCountry,
                BillingZip = userData.BillingZip,
                ShippingAddress = userData.ShippingAddress,
                ShippingCity = userData.ShippingCity,
                ShippingCountry = userData.ShippingCountry,
                ShippingZip = userData.ShippingZip,
                OrderId = newOrder.Id,
                OrderDateTime = newOrder.OrderDateTime,
                IsPayed = newOrder.IsPayedNow,
                IsSuccessFull = newOrder.IsSuccessFull
            };
            if (ModelState.IsValid)
            {
                newOrder.IsSuccessFull = true;
                orderCopy.IsSuccessFull = newOrder.IsSuccessFull;
                
                string jsonOrderSuccessFull = orderCopy.SaveToJson();
                string filename = $"{orderCopy.OrderId}-{newOrder.OrderDateTime.Day}-{newOrder.OrderDateTime.Month}-{newOrder.OrderDateTime.Hour}-{newOrder.OrderDateTime.Minute}";
                System.IO.File.WriteAllText($@".\AdminLog\{filename}.json", jsonOrderSuccessFull);
                if (newOrder.IsPayedNow)
                {
                    return RedirectToAction(nameof(Payment));
                }
                return RedirectToAction(nameof(OrderDetails));
            }
            newOrder.IsSuccessFull = false;
            orderCopy.IsSuccessFull = newOrder.IsSuccessFull;
            string jsonOrderFailed = orderCopy.SaveToJson();
            string filename2 = $"{orderCopy.OrderId}-{newOrder.OrderDateTime.Day}-{newOrder.OrderDateTime.Month}-{newOrder.OrderDateTime.Hour}-{newOrder.OrderDateTime.Minute}";
            System.IO.File.WriteAllText($@".\AdminLog\{filename2}.json", jsonOrderFailed);
            return View(userData);
        }

        

        [HttpGet]
        public IActionResult Payment()
        {
            return View();
        }
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Payment([Bind("FirstName,LastName,CardNumber,ExpiryDate,CvvCode")] PaymentDataToCheck userData)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction(nameof(OrderDetails));
            }
            return View(userData);
        }
    }
}

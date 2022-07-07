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
                return _unitOfWork.Product.GetAll();
            }
            else if (categoryId != 0 && supplierId == 0)
            {
                return _unitOfWork.Product.GetProductsForCategory(categoryId);
            }
            else if (categoryId == 0 && supplierId != 0)
            {
                return _unitOfWork.Product.GetProductsForSupplier(supplierId);
            } 
            return ProductService.GetProductsForSupplierAndCategory(supplierId, categoryId);
                
        }

        public IActionResult Index(int categoryId, int supplierId, int orderedProductId = -1)
        {
            //Dodawanie do bazy supliera oraz kilku produktów do bazy

            //Supplier lenovo = new Supplier { Name = "Lenovo", Description = "Computers" };
            //_unitOfWork.Supplier.Add(lenovo);
            //ProductCategory computer = new ProductCategory { Name = "Computer", Department = "PC", Description = "A tablet computer, commonly shortened to tablet, is a thin, flat mobile computer with a touchscreen display." };
            //_unitOfWork.ProductCategory.Add(computer);
            //_unitOfWork.Product.Add(new Product { Name = "Lenovo IdeaPad Miix 700", DefaultPrice = 479.0m, Currency = "USD", MaxInStock = 1, Description = "Keyboard cover is included. Fanless Core m5 processor. Full-size USB ports. Adjustable kickstand.", ProductCategory = computer, Supplier = lenovo });
            //_unitOfWork.Product.Add(new Product { Name = "Amazon Fire HD 8", DefaultPrice = 89.0m, Currency = "USD", MaxInStock = 5, Description = "Amazon's latest Fire HD 8 tablet is a great value for media consumption.", ProductCategory = computer, Supplier = lenovo });
            //_unitOfWork.Product.Add(new Product { Name = "PC1", DefaultPrice = 49.9m, Currency = "USD", MaxInStock = 0, Description = "Fantastic price. Large content ecosystem. Good parental controls. Helpful technical support.", ProductCategory = computer, Supplier = lenovo });
            //_unitOfWork.Product.Add(new Product { Name = "PC2", DefaultPrice = 479.0m, Currency = "USD", MaxInStock = 7, Description = "Keyboard cover is included. Fanless Core m5 processor. Full-size USB ports. Adjustable kickstand.", ProductCategory = computer, Supplier = lenovo });

            //_unitOfWork.Save();


            // Dodanie kolejnego przedmiotu do bazy 

            //Supplier lenovo = new Supplier { Name = "Amazon", Description = "Tablets" };
            //_unitOfWork.Supplier.Add(lenovo);

            //ProductCategory computer = new ProductCategory { Name = "Tablet", Department = "Tablet", Description = "A tablet computer, commonly shortened to tablet, is a thin, flat mobile computer with a touchscreen display." };
            //_unitOfWork.ProductCategory.Add(computer);

            //_unitOfWork.Product.Add(new Product { Name = "Amazon Fire", DefaultPrice = 49.9m, Currency = "USD", MaxInStock = 10, Description = "Fantastic price. Large content ecosystem. Good parental controls. Helpful technical support.", ProductCategory = computer, Supplier = lenovo });

            //_unitOfWork.Save();

            var user = UserService.GetUserByName("Janusz");
            if (orderedProductId != -1)
            {
                var allProducts = GetFilteredProducts(0, 0);
                var orderedProduct = allProducts.First(e => e.Id == orderedProductId);
                if (user.Cart.Details.Any(p=>p.Product.Id == orderedProductId))
                {
                    var detail = user.Cart.Details.First(e => e.Product.Id == orderedProductId);
                    detail.Quantity++;
                    user.ShoppingCartValue += orderedProduct.DefaultPrice;
                    if (detail.Quantity > detail.Product.MaxInStock)
                    {
                        var numberToDelete = detail.Quantity - detail.Product.MaxInStock;
                        user.ShoppingCartValue -= numberToDelete * orderedProduct.DefaultPrice;
                        detail.Quantity = detail.Product.MaxInStock;
                    }
                }
                else
                {
                    var newDetail = new CartDetail();
                    newDetail.Product = orderedProduct;
                    newDetail.Quantity = 1;
                    user.Cart.Details.Add(newDetail);
                    user.ShoppingCartValue += orderedProduct.DefaultPrice;

                }

            }
            _productsAndFilters = new ProductsAndFilters
            {
                AllProducts = GetFilteredProducts(categoryId, supplierId).ToList(),
                AllProductCategories = _unitOfWork.ProductCategory.GetAll(),
                AllSuppliers = _unitOfWork.Supplier.GetAll()
            };
            return View(_productsAndFilters);
        }

        public IActionResult OrderDetails()
        {
            var newestOrder = OrderService.GetNewestOrder();
            //var newestOrder = _unitOfWork.Order.Get(0);
            var allProducts = GetFilteredProducts(0, 0);
            foreach (CartDetail detail in newestOrder.User.Cart.Details)
            {
                //allProducts.First(e => e.Id == detail.Product.Id).Quantity -= product.Quantity;
                allProducts.First(e => e.Id == detail.Product.Id).MaxInStock -= detail.Quantity;
            }

            var userData = newestOrder.User;
            var userAddress = newestOrder.User;
            OrderForDelete orderCopy = new OrderForDelete()
            {
                Name = userData.Name, UserId = userData.Id,
                CartDetails = userData.Cart.Details, ShoppingCartValue = userData.ShoppingCartValue,
                FirstName = userAddress.FirstName, LastName = userAddress.LastName, Email = userAddress.Email,
                PhoneNumber = userAddress.PhoneNumber, BillingAddress = userAddress.BillingAddress, BillingCity = userAddress.BillingCity,
                BillingCountry = userAddress.BillingCountry, BillingZip = userAddress.BillingZip,
                ShippingAddress = userAddress.ShippingAddress, ShippingCity = userAddress.ShippingCity,
                ShippingCountry = userAddress.ShippingCountry, ShippingZip = userAddress.ShippingZip, OrderId = newestOrder.Id,
                OrderDateTime = newestOrder.OrderDateTime, IsPayed = newestOrder.IsPayedNow
            };
            var mail = new MailSenderService();
            
            mail.MailSender(orderCopy);
            newestOrder.User.Cart = new Cart();
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
            var product = user.Cart.Details.FirstOrDefault(e => e.Product.Id == id);
            if (product.Quantity == product.Product.MaxInStock)
            {
                product.Quantity = product.Product.MaxInStock;
            }
            else
            {
                product.Quantity++;
                user.ShoppingCartValue += product.Product.DefaultPrice;
            }
                
            return RedirectToAction(nameof(ShoppingCart));
        }

        public IActionResult MinusQuantity(int id)
        {
            var user = UserService.GetUserByName("Janusz");
            var product = user.Cart.Details.FirstOrDefault(e => e.Product.Id == id);
            product.Quantity--;
            user.ShoppingCartValue -= product.Product.DefaultPrice;
            if (product.Quantity == 0)
            {
                user.Cart.Details.Remove(product);
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
                CartDetails = userDat.Cart.Details,
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
                
                //string jsonOrderSuccessFull = orderCopy.SaveToJson();
                string filename = $"{orderCopy.OrderId}-{newOrder.OrderDateTime.Day}-{newOrder.OrderDateTime.Month}-{newOrder.OrderDateTime.Hour}-{newOrder.OrderDateTime.Minute}";
                //System.IO.File.WriteAllText($@".\AdminLog\{filename}.json", jsonOrderSuccessFull);
                if (newOrder.IsPayedNow)
                {
                    return RedirectToAction(nameof(Payment));
                }
                return RedirectToAction(nameof(OrderDetails));
            }
            newOrder.IsSuccessFull = false;
            orderCopy.IsSuccessFull = newOrder.IsSuccessFull;
            //string jsonOrderFailed = orderCopy.SaveToJson();
            string filename2 = $"{orderCopy.OrderId}-{newOrder.OrderDateTime.Day}-{newOrder.OrderDateTime.Month}-{newOrder.OrderDateTime.Hour}-{newOrder.OrderDateTime.Minute}";
            //System.IO.File.WriteAllText($@".\AdminLog\{filename2}.json", jsonOrderFailed);
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

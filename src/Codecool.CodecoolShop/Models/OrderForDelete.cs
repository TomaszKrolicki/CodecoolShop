using System;
using System.Collections.Generic;
using Codecool.CodecoolShop.Services;

namespace Codecool.CodecoolShop.Models
{
    public class OrderForDelete
    {
        public string Name { get; set; }
        public int UserId { get; set; }
        public List<Product> ShoppingCart { get; set; }
        public decimal ShoppingCartValue { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string BillingCountry { get; set; }
        public string BillingCity { get; set; }
        public string BillingZip { get; set; }
        public string BillingAddress { get; set; }
        public string ShippingCountry { get; set; }
        public string ShippingCity { get; set; }
        public string ShippingZip { get; set; }
        public string ShippingAddress { get; set; }

        public DateTime OrderDateTime { get; set; }
        public int OrderId { get; set; }
    }
}
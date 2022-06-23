using System;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading;
using System.ComponentModel;
using System.Linq;
using Codecool.CodecoolShop.Models;

namespace Codecool.CodecoolShop.Services
{
    public class MailSenderService

    {

        public void MailSender (OrderForDelete order)
        {


            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential("codecoolshop3@gmail.com", "czsmvtjyjurjoffl"),
                EnableSsl = true
            };

            string message = CreateMessage(order);

            client.Send("codecoolshop3@gmail.com", order.Email, $"CodeCoolShop - order number {order.OrderId}", message);

        }

        private string CreateMessage(OrderForDelete order)
        {
            string products = "";
            for (int i = 0; i < order.ShoppingCart.Count; i++)
            {
                products += $"Product {i + 1}: " + order.ShoppingCart[i].Name + "\n";
                products += "Supplier: " + order.ShoppingCart[i].Supplier.Name + "\n";
                products += "Price for 1: " + order.ShoppingCart[i].DefaultPrice + "$\n";
                products += "Quantity: " + order.ShoppingCart[i].Quantity + "\n\n";
            }

            string isPayed = "";
            if (order.IsPayed)
            {
                isPayed = "You already payed for products!\n\n";
            }
            else
            {
                isPayed = $"Remember - You will have to pay {order.ShoppingCartValue}$ when the package will arrive!\n\n";
            }
            string userData =
                $"Your personal data: {order.FirstName} {order.LastName}\nEmail: {order.Email}\nPhone: {order.PhoneNumber}\n" +
                $"Your billing address: {order.BillingZip} {order.BillingCity}, {order.BillingCountry}\n{order.BillingAddress}\n" +
                $"Your shipping address: {order.ShippingZip} {order.ShippingCity}, {order.ShippingCountry}\n{order.ShippingAddress}\n\n";
            return $"ORDER NUMBER {order.OrderId}\n\n\n" +
                   "Your shop list:\n" + products + userData + isPayed + "CodeCoolShop";
        }
    }
}

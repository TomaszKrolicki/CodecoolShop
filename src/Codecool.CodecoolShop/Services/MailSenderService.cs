using System;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading;
using System.ComponentModel;
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
            return $"{order.OrderId} {order.FirstName}";
        }
    }
}

using System;
using Codecool.CodecoolShop.Services;

namespace Codecool.CodecoolShop.Models
{
    public class Order
    {
        public User User { get; set; }
        public UserDataToCheck UserPersonalInformation { get; set; }
        public DateTime OrderDateTime { get; set; }
        public int OrderId { get; set; }
        public Order(User user, UserDataToCheck userPersonalInformation)
        {
            OrderDateTime = DateTime.Now;
            User = user;
            UserPersonalInformation = userPersonalInformation;
        }
    }
}

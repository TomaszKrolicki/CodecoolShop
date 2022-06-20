using System.Collections.Generic;

namespace Codecool.CodecoolShop.Models
{
    public class User
    {
        public User(string name, int userId, List<Product> shoppingCart)
        {
            Name = name;
            UserId = userId;
            ShoppingCart = shoppingCart;

        }
        public string Name { get; set; }
        public int UserId { get; set; }
        public List<Product> ShoppingCart { get; set; }

    }
}

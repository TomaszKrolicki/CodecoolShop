using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Codecool.CodecoolShop.Models;

namespace Codecool.CodecoolShop.Daos.Implementations
{
    public class UserDaoMemory : IUserDao
    {
        private List<User> data = new List<User>();
        private static UserDaoMemory instance = null;
        private UserDaoMemory()
        {
        }

        public static UserDaoMemory GetInstance()
        {
            if (instance == null)
            {
                instance = new UserDaoMemory();
            }

            return instance;
        }

        public void Add(User item)
        {
            if (data.Count == 0)
            {
                item.UserId = 1;
            }
            else
            {
                item.UserId = data.Max(e => e.UserId) + 1;
            }
            data.Add(item);
        }

        public void Remove(int id)
        {
            data.Remove(this.Get(id));
        }
        public User Get(int id)
        {
            return data.Find(x => x.UserId == id);
        }
        public IEnumerable<User> GetAll()
        {
            return data;
        }

        //public IEnumerable<Product> GetBy(Supplier supplier)
        //{
        //    return data.Where(x => x.Supplier.Id == supplier.Id);
        //}

        //public IEnumerable<Product> GetBy(ProductCategory productCategory)
        //{
        //    return data.Where(x => x.ProductCategory.Id == productCategory.Id);
        //}
    }
}

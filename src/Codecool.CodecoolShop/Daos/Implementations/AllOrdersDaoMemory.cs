using System.Collections.Generic;
using System.Linq;
using Codecool.CodecoolShop.Models;

namespace Codecool.CodecoolShop.Daos.Implementations
{
    public class OrderDaoMemory : IAllOrdersDao
    {
        private List<Order> data = new List<Order>();
        private static OrderDaoMemory instance = null;

        private OrderDaoMemory()
        {
        }

        public static OrderDaoMemory GetInstance()
        {
            if (instance == null)
            {
                instance = new OrderDaoMemory();
            }

            return instance;
        }

        public void Add(Order item)
        {
            if (data.Count == 0)
            {
                item.Id = 1;
            }
            else
            {
                item.Id = data.Max(e => e.Id) + 1;
            }
            data.Add(item);
        }

        public void Remove(int id)
        {
            data.Remove(this.Get(id));
        }

        public Order Get(int id)
        {
            return data.Find(x => x.Id == id);
        }
        public Order GetByName(string name)
        {
            return data.Find(x => x.Name == name);
        }

        public IEnumerable<Order> GetAll()
        {
            return data;
        }
    }
}

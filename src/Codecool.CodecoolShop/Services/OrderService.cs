using Codecool.CodecoolShop.Daos;
using Codecool.CodecoolShop.Models;

namespace Codecool.CodecoolShop.Services
{
    public class OrderService
    {
        private readonly IAllOrdersDao orderDao;
        public OrderService(IAllOrdersDao orderDao)
        {
            this.orderDao = orderDao;
        }

        public Order GetOrder(int orderId)
        {
            return this.orderDao.Get(orderId);
        }
    }
}

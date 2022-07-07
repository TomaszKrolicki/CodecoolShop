using Codecool.CodecoolShop.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Codecool.CodecoolShop.Repository.IRepository
{
    public interface IOrderRepository : IRepository<Order>
    {
        void Update(Order obj);
        Order GetWithDetailsNewest();
    }
}
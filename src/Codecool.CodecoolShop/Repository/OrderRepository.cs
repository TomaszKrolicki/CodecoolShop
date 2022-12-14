
using Codecool.CodecoolShop.Data;
using Codecool.CodecoolShop.Models;
using Codecool.CodecoolShop.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Codecool.CodecoolShop.Repository
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        private readonly CodeCoolDBContext _db;

        public OrderRepository(CodeCoolDBContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Order obj)
        {
            _db.Update(obj);
        }

        public Order GetWithDetailsNewest()
        {
            return _db.Orders.Include(u => u.User).ThenInclude(e => e.Cart).ThenInclude(e=>e.Details).ThenInclude(e=>e.Product).ThenInclude(e=>e.Supplier).OrderBy(i=>i.Id).LastOrDefault();
            // .theninclude()
        }

        //public void Add(Order obj)
        //{
        //    _db.Add(obj);
        //}
    }
}
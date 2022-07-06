
using Codecool.CodecoolShop.Data;
using Codecool.CodecoolShop.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Codecool.CodecoolShop.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CodeCoolDBContext _db;

        public UnitOfWork(CodeCoolDBContext db)
        {
            _db = db;
            Order = new OrderRepository(_db);
        }


        public IOrderRepository Order { get; private set; }


        public void Dispose()
        {
            _db.Dispose();
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
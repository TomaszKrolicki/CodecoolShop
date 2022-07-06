
using Codecool.CodecoolShop.Data;
using Codecool.CodecoolShop.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Text;
using Codecool.CodecoolShop.Models;

namespace Codecool.CodecoolShop.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CodeCoolDBContext _db;

        public UnitOfWork(CodeCoolDBContext db)
        {
            _db = db;
            Order = new OrderRepository(_db);
            User = new UserRepository(_db);
            Product = new ProductRepository(_db);
            Supplier = new SupplierRepository(_db);
            ProductCategory = new ProductCategoryRepository(_db);
        }


        public IOrderRepository Order { get; private set; }
        public IUserRepository User { get; private set; }
        public IProductRepository Product { get; private set; }
        public ISupplierRepository Supplier { get; private set; }
        public IProductCategoryRepository ProductCategory { get; private set; }


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
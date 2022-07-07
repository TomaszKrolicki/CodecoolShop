
using System;
using System.Collections.Generic;
using System.Text;

namespace Codecool.CodecoolShop.Repository.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        IOrderRepository Order { get; }
        IUserRepository User { get; }
        IProductRepository Product { get; }
        ISupplierRepository Supplier { get; }
        IProductCategoryRepository ProductCategory { get; }
        ICartRepository Cart { get; }

        void Save();
    }
}

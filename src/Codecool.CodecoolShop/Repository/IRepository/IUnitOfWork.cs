
using System;
using System.Collections.Generic;
using System.Text;

namespace Codecool.CodecoolShop.Repository.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        IOrderRepository Order { get; }
        IUserRepository User { get; }

        void Save();
    }
}

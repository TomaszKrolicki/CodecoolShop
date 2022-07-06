
using System;
using System.Collections.Generic;
using System.Text;

namespace Codecool.CodecoolShop.Repository.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        IOrderRepository Order { get; }

        void Save();
    }
}

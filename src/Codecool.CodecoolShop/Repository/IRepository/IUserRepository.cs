using Codecool.CodecoolShop.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Codecool.CodecoolShop.Repository.IRepository
{
    public interface IUserRepository : IRepository<User>
    {
        void Update(User obj);
    }
}

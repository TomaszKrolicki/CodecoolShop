
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
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly CodeCoolDBContext _db;

        public UserRepository(CodeCoolDBContext db) : base(db)
        {
            _db = db;
        }

        public void Update(User obj)
        {
            _db.Update(obj);
        }

        public User GetLast()
        {
            return _db.Users.Include(u=>u.Cart).ThenInclude(i=>i.Details).ThenInclude(p=>p.Product).OrderBy(e => e.Id).LastOrDefault();
        }

    }
}

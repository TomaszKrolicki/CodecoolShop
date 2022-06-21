using Codecool.CodecoolShop.Daos;
using Codecool.CodecoolShop.Models;

namespace Codecool.CodecoolShop.Services
{
    public class UserService
    {
        private readonly IUserDao userDao;

        public UserService(IUserDao userDao)
        {
            this.userDao = userDao;
        }

        public User GetUser(int userId)
        {
            return this.userDao.Get(userId);
        }
    }
}

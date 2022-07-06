using Codecool.CodecoolShop.Data;
using Codecool.CodecoolShop.Models;
using Codecool.CodecoolShop.Repository.IRepository;

namespace Codecool.CodecoolShop.Repository
{
    public class SupplierRepository : Repository<Supplier>, ISupplierRepository
    {
        private readonly CodeCoolDBContext _db;

        public SupplierRepository(CodeCoolDBContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Supplier obj)
        {
            _db.Update(obj);
        }
    }
}

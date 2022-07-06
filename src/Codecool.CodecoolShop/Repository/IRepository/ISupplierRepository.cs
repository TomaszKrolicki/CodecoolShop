using Codecool.CodecoolShop.Models;

namespace Codecool.CodecoolShop.Repository.IRepository
{
    public interface ISupplierRepository : IRepository<Supplier>
    {
        void Update(Supplier obj);
    }
}

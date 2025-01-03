using nSkinShop.Models;

namespace nSkinShop.DataAccess.Repository;

public interface IOrderHeaderRepository: IRepository<OrderHeader>
{
    void Update(OrderHeader orderHeader);
}
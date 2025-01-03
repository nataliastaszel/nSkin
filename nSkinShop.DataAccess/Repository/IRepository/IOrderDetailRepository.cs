using nSkinShop.Models;

namespace nSkinShop.DataAccess.Repository;

public interface IOrderDetailRepository: IRepository<OrderDetail>
{
    void Update(OrderDetail orderDetail);
}
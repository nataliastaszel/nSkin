using nSkinShop.Models;

namespace nSkinShop.DataAccess.Repository;

public interface IShoppingCartRepository: IRepository<ShoppingCart>
{
    void Update(ShoppingCart shoppingCart);
}
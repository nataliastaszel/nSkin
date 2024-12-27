using nSkinShop.Models;

namespace nSkinShop.DataAccess.Repository;

public interface IProductRepository: IRepository<Product>
{
    void Update(Product product);
}
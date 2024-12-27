using nSkinShop.Models;

namespace nSkinShop.DataAccess.Repository;

public interface ICategoryRepository: IRepository<Category>
{
    void Update(Category category);
}
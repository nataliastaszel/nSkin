using nSkinShop.Models;

namespace nSkinShop.DataAccess.Repository;

public interface ICompanyRepository: IRepository<Company>
{
    void Update(Company company);
}
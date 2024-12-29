namespace nSkinShop.DataAccess.Repository;

public interface IUnitOfWork
{
    ICategoryRepository Category { get; }
    IProductRepository Product { get; }

    ICompanyRepository Company { get; }
    
    IPromoCodeRepository PromoCode { get; }

    void Save();
}
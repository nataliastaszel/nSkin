namespace nSkinShop.DataAccess.Repository;

public interface IUnitOfWork
{
    ICategoryRepository Category { get; }
    IProductRepository Product { get; }

    ICompanyRepository Company { get; }
    
    IPromoCodeRepository PromoCode { get; }
    IShoppingCartRepository ShoppingCart { get; }
    IApplicationUserRepository ApplicationUser { get; }
    IOrderHeaderRepository OrderHeader { get; }
    IOrderDetailRepository OrderDetail { get; }

    void Save();
}
using nSkinShop.Models;

namespace nSkinShop.DataAccess.Repository;

public interface IPromoCodeRepository: IRepository<PromoCode>
{
    void Update(PromoCode promoCode);
}
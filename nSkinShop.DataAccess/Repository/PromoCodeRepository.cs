using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using nSkinShop.Data;
using nSkinShop.Models;

namespace nSkinShop.DataAccess.Repository;

public class PromoCodeRepository : Repository<PromoCode>, IPromoCodeRepository
{
    private readonly ApplicationDbContext _db;

    public PromoCodeRepository(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }

    public void Update(PromoCode promoCode)
    {
        _db.PromoCodes.Update(promoCode);
    }
}
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using nSkinShop.Data;
using nSkinShop.Models;

namespace nSkinShop.DataAccess.Repository;

public class ShoppingCartRepository : Repository<ShoppingCart>, IShoppingCartRepository
{
    private readonly ApplicationDbContext _db;

    public ShoppingCartRepository(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }

    public void Update(ShoppingCart shoppingCart)
    {
        _db.ShoppingCarts.Update(shoppingCart);
    }
}
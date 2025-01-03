using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using nSkinShop.Data;
using nSkinShop.Models;

namespace nSkinShop.DataAccess.Repository;

public class OrderHeaderRepository : Repository<OrderHeader>, IOrderHeaderRepository
{
    private readonly ApplicationDbContext _db;

    public OrderHeaderRepository(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }

    public void Update(OrderHeader orderHeader)
    {
        _db.OrderHeaders.Update(orderHeader);
    }
    
}
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using nSkinShop.Data;
using nSkinShop.Models;

namespace nSkinShop.DataAccess.Repository;

public class OrderDetailRepository : Repository<OrderDetail>, IOrderDetailRepository
{
    private readonly ApplicationDbContext _db;

    public OrderDetailRepository(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }

    public void Update(OrderDetail orderDetail)
    {
        _db.OrderDetails.Update(orderDetail);
    }
    
}
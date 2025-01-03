using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using nSkinShop.Data;
using nSkinShop.Models;

namespace nSkinShop.DataAccess.Repository;

public class ApplicationUserRepository : Repository<ApplicationUser>, IApplicationUserRepository
{
    private readonly ApplicationDbContext _db;

    public ApplicationUserRepository(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }
    
    
}
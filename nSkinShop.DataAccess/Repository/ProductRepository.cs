using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using nSkinShop.Data;
using nSkinShop.Models;

namespace nSkinShop.DataAccess.Repository;

public class ProductRepository : Repository<Product>, IProductRepository
{
    private readonly ApplicationDbContext _db;

    public ProductRepository(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }

    public void Update(Product product)
    {
        var objFromDb = _db.Products.FirstOrDefault(p => p.Id == product.Id);
        if (objFromDb != null)
        {
            objFromDb.Id = product.Id;
            objFromDb.Title = product.Title;
            objFromDb.Description = product.Description;
            objFromDb.Price = product.Price;
            objFromDb.Brand = product.Brand;
            objFromDb.Category = product.Category;
            objFromDb.CategoryId = product.CategoryId;
            objFromDb.Price100ml = product.Price100ml;
            if (product.ImageUrl != null)
            {
                objFromDb.ImageUrl = product.ImageUrl;
            }
        }
    }
}
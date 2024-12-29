namespace nSkinShop.Models;

public class PromoCode
{
    public int Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public double DiscountAmount { get; set; }
    public DateTime ExpiryDate { get; set; }
    public bool IsActive { get; set; }
}

namespace EssentialTools.Models
{
    public class DefaultDiscountHelper : EssentialTools.Models.IDiscountHelper
    {
        public decimal DiscountSize { get; set; }

        public decimal ApplyDiscount(decimal total)
        {
            return (total - (DiscountSize / 100m * total));
        }
    }
}
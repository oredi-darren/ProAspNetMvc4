using System;
namespace EssentialTools.Models
{
    public class MinimumDiscountHelper : IDiscountHelper
    {
        public decimal ApplyDiscount(decimal total)
        {
            if (total < 0)
                throw new ArgumentOutOfRangeException();

            if(total > 100)
                return 0.9M * total;

            if (total >= 10 && total <= 100)
                return total - 5.0M;
            
            return total;
        }
    }
}
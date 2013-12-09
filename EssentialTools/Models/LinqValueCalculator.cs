using System.Collections.Generic;
using System.Linq;

namespace EssentialTools.Models
{
    public class LinqValueCalculator : IValueCalculator
    {
        private IDiscountHelper _discounter;

        public LinqValueCalculator(IDiscountHelper discounter)
        {
            _discounter = discounter;
        }
        public decimal ValueProducts(IEnumerable<Product> products)
        {
            return _discounter.ApplyDiscount(products.Sum(e => e.Price));
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;

namespace LanguageFeatures.Models
{
    public static class MyExtensionsMethods
    {
        public static decimal TotalPrices(this ShoppingCart cart)
        {
            return cart.Products.Sum(e => e.Price);
        }

        public static decimal TotalPrices(this IEnumerable<Product> products)
        {
            return products.Sum(e => e.Price);
        }

        public static IEnumerable<Product> FilterByCategory(this IEnumerable<Product> products, string category)
        {
            foreach(var product in products)
                if(product.Category == category)
                yield return product;
        }

        public static IEnumerable<Product> Filter(this IEnumerable<Product> products, Func<Product, bool> selector)
        {
            foreach (var product in products)
                if (selector(product))
                    yield return product;
        }
    }
}
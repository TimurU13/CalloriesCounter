using CallorieCounter.Models;
using System.Collections.Generic;
using System.Linq;
namespace CallorieCounter
{
    using CallorieCounter.Models;
    using System.Collections.Generic;
    using System.Linq;

    public class ProductService : IProductService
    {
        private static List<Product> products = new List<Product>
    {
        new Product { Id = 1, Name = "Яблоко", CaloriesPer100g = 52 },
        new Product { Id = 2, Name = "Банан", CaloriesPer100g = 89 }
    };

        public IEnumerable<Product> GetProducts()
        {
            return products.ToList();
        }

        public Product GetProductById(int id)
        {
            return products.FirstOrDefault(p => p.Id == id);
        }

        public Product CreateProduct(Product newProduct)
        {
            newProduct.Id = products.Count + 1;
            products.Add(newProduct);
            return newProduct;
        }

        public void UpdateProduct(int id, Product updatedProduct)
        {
            var product = products.FirstOrDefault(p => p.Id == id);
            if (product != null)
            {
                product.Name = updatedProduct.Name;
                product.CaloriesPer100g = updatedProduct.CaloriesPer100g;
            }
        }

        public void DeleteProduct(int id)
        {
            var product = products.FirstOrDefault(p => p.Id == id);
            if (product != null)
            {
                products.Remove(product);
            }
        }
    }
}

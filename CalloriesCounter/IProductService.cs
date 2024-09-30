using CallorieCounter.Models;
using System.Collections.Generic;
namespace CallorieCounter;

public interface IProductService
{
    IEnumerable<Product> GetProducts();
    Product GetProductById(int id);
    Product CreateProduct(Product newProduct);
    void UpdateProduct(int id, Product updatedProduct);
    void DeleteProduct(int id);
}

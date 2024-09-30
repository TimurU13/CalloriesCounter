using Microsoft.AspNetCore.Mvc;
using CallorieCounter.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
namespace CallorieCounter.Controllers;
[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public IActionResult GetProducts()
    {
        var products = _productService.GetProducts();
        var response = products.Select(product => new
        {
            product.Id,
            product.Name,
            product.CaloriesPer100g,
            links = new List<object>
            {
                new { rel = "self", href = Url.Action(nameof(GetProduct), new { id = product.Id }) },
                new { rel = "edit", href = Url.Action(nameof(UpdateProduct), new { id = product.Id }) },
                new { rel = "delete", href = Url.Action(nameof(DeleteProduct), new { id = product.Id }) }
            }
        });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public IActionResult GetProduct(int id)
    {
        var product = _productService.GetProductById(id);
        if (product == null)
        {
            return NotFound();
        }

        var response = new
        {
            product.Id,
            product.Name,
            product.CaloriesPer100g,
            links = new List<object>
            {
                new { rel = "self", href = Url.Action(nameof(GetProduct), new { id = product.Id }) },
                new { rel = "edit", href = Url.Action(nameof(UpdateProduct), new { id = product.Id }) },
                new { rel = "delete", href = Url.Action(nameof(DeleteProduct), new { id = product.Id }) }
            }
        };

        return Ok(response);
    }

    [HttpPost]
    public IActionResult CreateProduct([FromBody] Product newProduct)
    {
        var createdProduct = _productService.CreateProduct(newProduct);

        var response = new
        {
            createdProduct.Id,
            createdProduct.Name,
            links = new List<object>
            {
                new { rel = "self", href = Url.Action(nameof(GetProduct), new { id = createdProduct.Id }) },
                new { rel = "edit", href = Url.Action(nameof(UpdateProduct), new { id = createdProduct.Id }) },
                new { rel = "delete", href = Url.Action(nameof(DeleteProduct), new { id = createdProduct.Id }) }
            }
        };

        return CreatedAtAction(nameof(GetProduct), new { id = createdProduct.Id }, response);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateProduct(int id, [FromBody] Product updatedProduct)
    {
        _productService.UpdateProduct(id, updatedProduct);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteProduct(int id)
    {
        _productService.DeleteProduct(id);
        return NoContent();
    }
}

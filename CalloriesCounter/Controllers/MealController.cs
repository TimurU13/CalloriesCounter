using Microsoft.AspNetCore.Mvc;
using CallorieCounter.Models;
namespace CallorieCounter.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MealController : ControllerBase
{
    private readonly IMealService _mealService;
    private readonly IProductService _productService;

    public MealController(IMealService mealService, IProductService productService)
    {
        _mealService = mealService;
        _productService = productService;
    }

    [HttpGet("{userId}")]
    public IActionResult GetMealsForUser(int userId)
    {
        var meals = _mealService.GetMealsForUser(userId);
        var response = meals.Select(meal =>
        {
            var product = _productService.GetProductById(meal.ProductId);
            var calories = _mealService.CalculateMealCalories(meal, product);

            return new
            {
                meal.Id,
                meal.UserId,
                meal.ProductId,
                meal.Amount,
                meal.Date,
                Calories = calories,
                links = new List<object>
                {
                    new { rel = "self", href = Url.Action(nameof(GetMeal), new { id = meal.Id }) },
                }
            };
        });

        return Ok(response);
    }

    [HttpGet("meal/{id}")]
    public IActionResult GetMeal(int id)
    {
        var meal = _mealService.GetMealById(id);
        if (meal == null)
        {
            return NotFound();
        }

        var product = _productService.GetProductById(meal.ProductId);
        var calories = _mealService.CalculateMealCalories(meal, product);

        var response = new
        {
            meal.Id,
            meal.UserId,
            meal.ProductId,
            meal.Amount,
            meal.Date,
            Calories = calories,  
            links = new List<object>
            {
                new { rel = "self", href = Url.Action(nameof(GetMeal), new { id = meal.Id }) },
            }
        };

        return Ok(response);
    }
}
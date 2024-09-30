using CallorieCounter.Models;
using System.Collections.Generic;
using System.Linq;
namespace CallorieCounter;
using CallorieCounter.Models;
using System.Collections.Generic;
using System.Linq;

public class MealService : IMealService
{
    private static List<Meal> meals = new List<Meal>
    {
        new Meal { Id = 1, UserId = 1, ProductId = 1, Amount = 150, Date = DateTime.Now },
        new Meal { Id = 2, UserId = 2, ProductId = 2, Amount = 200, Date = DateTime.Now }
    };

    public IEnumerable<Meal> GetMealsForUser(int userId)
    {
        return meals.Where(m => m.UserId == userId).ToList();
    }

    public double CalculateMealCalories(Meal meal, Product product)
    {

        return (meal.Amount / 100) * product.CaloriesPer100g;
    }

    public Meal GetMealById(int id)
    {
        var meal = meals.FirstOrDefault(m => m.Id == id);
        if (meal == null)
        {
            throw new InvalidOperationException("Meal not found.");
        }
        return meal;
    }

    public Meal CreateMeal(Meal newMeal)
    {
        newMeal.Id = meals.Count + 1;
        meals.Add(newMeal);
        return newMeal;
    }

    public void UpdateMeal(int id, Meal updatedMeal)
    {
        var meal = meals.FirstOrDefault(m => m.Id == id);
        if (meal != null)
        {
            meal.UserId = updatedMeal.UserId;
            meal.ProductId = updatedMeal.ProductId;
            meal.Amount = updatedMeal.Amount;
            meal.Date = updatedMeal.Date;
        }
    }

    public void DeleteMeal(int id)
    {
        var meal = meals.FirstOrDefault(m => m.Id == id);
        if (meal != null)
        {
            meals.Remove(meal);
        }
    }
}

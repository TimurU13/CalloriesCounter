using CallorieCounter.Models;
using System.Collections.Generic;
namespace CallorieCounter;
public interface IMealService
{
    IEnumerable<Meal> GetMealsForUser(int userId);
    Meal GetMealById(int id);
    Meal CreateMeal(Meal newMeal);
    void UpdateMeal(int id, Meal updatedMeal);
    void DeleteMeal(int id);
    double CalculateMealCalories(Meal meal, Product product);
}

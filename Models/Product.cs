namespace CallorieCounter.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public double CaloriesPer100g { get; set; }
    }
}

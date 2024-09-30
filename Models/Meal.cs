namespace CallorieCounter.Models
{
    public class Meal
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public double Amount { get; set; }
        public DateTime Date { get; set; }
    }
}

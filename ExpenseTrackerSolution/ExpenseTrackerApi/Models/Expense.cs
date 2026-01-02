namespace ExpenseTrackerApi.Models;

public class Expense{
    public Guid Id { get; set; } = Guid.NewGuid();
    public required string Title { get; set; }
    public decimal Amount { get; set; }
    public required string Category { get; set; }
    public DateTime Date { get; set; }
}
using ExpenseTrackerAPI.Models;

public class Expense
{
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public string Description { get; set; }
    public DateTime Date { get; set; }
    public ExpenseCategory Category { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
}

public enum ExpenseCategory
{
    Groceries,
    Leisure,
    Electronics,
    Utilities,
    Clothing,
    Health,
    Others
}

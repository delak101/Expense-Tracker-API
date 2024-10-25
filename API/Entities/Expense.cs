namespace API.Entities;

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
    Groceries,  // Daily shopping and groceries
    Leisure,    // Entertainment, outings, vacations, etc.
    Electronics,// Gadgets and electronics purchases
    Utilities,  // Bills like electricity, water, internet
    Clothing,   // Apparel purchases
    Health,     // Medical expenses
    Others      // Any other uncategorized expenses
}

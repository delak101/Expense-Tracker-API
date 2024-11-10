using System.ComponentModel.DataAnnotations;

namespace API.Entities;

public class Expense
{
    public int Id { get; set; }
    [Required]
    public decimal Amount { get; set; }
    [Required]
    [MaxLength(100)]
    public string Description { get; set; }
    [Required]
    public DateTime Date { get; set; } = DateTime.UtcNow;
    [Required]
    public ExpenseCategory Category { get; set; }
    /*
     * unfortunately this will take the enum index
     * 0 -> Groceries, 1 -> Leisure
     * can be solved by updating json parsing options or taking it as string then parsing it in the code
     * but i'll keep it like this for now
     */
    
    //user foreign key
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

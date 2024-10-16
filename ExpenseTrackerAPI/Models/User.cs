using System.ComponentModel.DataAnnotations;

namespace ExpenseTrackerAPI.Models;
public class User
{
    public int Id { get; set; }
    [Required]
    public string Username { get; set; }
    [Required]
    public string PasswordHash { get; set; } // Store hashed password
    public List<Expense> Expenses { get; set; } = new List<Expense>();
}

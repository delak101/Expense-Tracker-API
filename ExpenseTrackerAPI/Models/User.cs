public class User
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string PasswordHash { get; set; } // Store hashed password
    public List<Expense> Expenses { get; set; } = new List<Expense>();
}

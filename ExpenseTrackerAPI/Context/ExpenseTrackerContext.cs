using Microsoft.EntityFrameworkCore;
using ExpenseTrackerAPI.Models;

namespace ExpenseTrackerAPI.Context;
public class ExpenseTrackerContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Expense> Expenses { get; set; }

    public ExpenseTrackerContext(DbContextOptions<ExpenseTrackerContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasMany(u => u.Expenses).WithOne(e => e.User);
    }
}

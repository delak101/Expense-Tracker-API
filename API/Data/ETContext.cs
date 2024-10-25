using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class ETContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Expense> Expenses { get; set; }
}

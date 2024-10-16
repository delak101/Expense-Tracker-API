using ExpenseTrackerAPI.Context;
using ExpenseTrackerAPI.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ExpensesController : ControllerBase
{
    private readonly ExpenseTrackerContext _context;
    private readonly IUserService _userService;

    public ExpensesController(ExpenseTrackerContext context, IUserService userService)
    {
        _context = context;
        _userService = userService;
    }

    [HttpGet]
    public async Task<IActionResult> GetExpenses([FromQuery] DateTime? start, [FromQuery] DateTime? end)
    {
        var userId = int.Parse(User.Claims.First(c => c.Type == "id").Value);
        var expenses = _context.Expenses.Where(e => e.UserId == userId);

        if (start.HasValue && end.HasValue)
            expenses = expenses.Where(e => e.Date >= start && e.Date <= end);

        return Ok(await expenses.ToListAsync());
    }

    [HttpPost]
    public async Task<IActionResult> AddExpense([FromBody] Expense expense)
    {
        var userId = int.Parse(User.Claims.First(c => c.Type == "id").Value);
        expense.UserId = userId;
        _context.Expenses.Add(expense);
        await _context.SaveChangesAsync();
        return Ok(expense);
    }

    // Update, Delete Expense methods would be similar
}

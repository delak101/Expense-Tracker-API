//Handles CRUD operations for expenses

using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[Authorize]
[ApiController]
[Route("Finance/[controller]")] // Finance/Expenses 
public class ExpensesController(ETContext context) : ControllerBase
{
    [HttpPost("AddExpense")]
    public async Task<ActionResult<Expense>> CreateExpense(Expense expense)
    {
        var userId = int.Parse(User.FindFirst("UserId").Value); // Retrieve the user’s ID from the JWT claims
        expense.UserId = userId;
        context.Expenses.Add(expense);
        await context.SaveChangesAsync();

        return CreatedAtAction("GetExpense", new { id = expense.Id }, expense);
    }

    [HttpGet("GetExpense/{id}")]
    public async Task<ActionResult<Expense>> GetExpense(int id)
    {
        var userId = int.Parse(User.FindFirst("UserId").Value);
        var expense = await context.Expenses
            .Where(e => e.UserId == userId && e.Id == id)
            .FirstOrDefaultAsync();

        if (expense == null)
            return NotFound();

        return expense;
    }

    // GET: api/expenses
    [HttpGet("GetExpenses")]
    public async Task<ActionResult<IEnumerable<Expense>>> GetExpenses()
    {
        var userId = int.Parse(User.FindFirst("UserId").Value);
        return await context.Expenses
            .Where(e => e.UserId == userId)
            .ToListAsync();
    }

    // PUT: api/expenses/{id}
    [HttpPut("UpdateExpense/{id}")]
    public async Task<IActionResult> UpdateExpense(int id, Expense expense)
    {
        if (id != expense.Id) return BadRequest("Expense ID mismatch");

        var userId = int.Parse(User.FindFirst("UserId").Value);
        var existingExpense = await context.Expenses.FindAsync(id);

        if (existingExpense == null || existingExpense.UserId != userId)
            return NotFound("Expense not found or unauthorized");

        existingExpense.Amount = expense.Amount;
        existingExpense.Description = expense.Description;
        existingExpense.Category = expense.Category;
        existingExpense.Date = expense.Date;

        await context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/expenses/{id}
    [HttpDelete("DeleteExpense/{id}")]
    public async Task<IActionResult> DeleteExpense(int id)
    {
        var userId = int.Parse(User.FindFirst("UserId").Value);
        var expense = await context.Expenses.FindAsync(id);

        if (expense == null || expense.UserId != userId)
            return NotFound("Expense not found or unauthorized");

        context.Expenses.Remove(expense);
        await context.SaveChangesAsync();

        return NoContent();
    }
}

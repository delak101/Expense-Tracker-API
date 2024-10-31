//Handles CRUD operations for expenses

using API.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("Finance/[controller]")] // Finance/Expenses 
public class ExpensesController : ControllerBase
{
     [HttpGet]
     public ActionResult<IEnumerable<Expense>> GetExpenses()
     {
         return Ok();
     }

}

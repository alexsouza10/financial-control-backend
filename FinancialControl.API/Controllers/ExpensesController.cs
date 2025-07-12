using FinancialControl.Application.DTOs;
using FinancialControl.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FinancialControl.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ExpensesController : ControllerBase
{
    private readonly IExpenseService _expenseService;

    public ExpensesController(IExpenseService expenseService)
    {
        _expenseService = expenseService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ExpenseResponseDto>>> GetExpenses()
    {
        var expenses = await _expenseService.GetAllExpensesAsync();
        return Ok(expenses);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ExpenseResponseDto>> GetExpense(Guid id)
    {
        var expense = await _expenseService.GetExpenseByIdAsync(id);
        if (expense == null)
        {
            return NotFound();
        }
        return Ok(expense);
    }

    [HttpPost]
    public async Task<ActionResult<ExpenseResponseDto>> PostExpense([FromBody] ExpenseDto expenseDto)
    {
        var newExpense = await _expenseService.AddExpenseAsync(expenseDto);
        return CreatedAtAction(nameof(GetExpense), new { id = newExpense.Id }, newExpense);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutExpense(Guid id, [FromBody] ExpenseDto expenseDto)
    {
        var result = await _expenseService.UpdateExpenseAsync(id, expenseDto);
        if (!result)
        {
            return NotFound();
        }
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteExpense(Guid id)
    {
        var result = await _expenseService.DeleteExpenseAsync(id);
        if (!result)
        {
            return NotFound();
        }
        return NoContent();
    }
}
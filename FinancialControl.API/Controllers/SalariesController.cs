using FinancialControl.Application.DTOs;
using FinancialControl.Application.Interfaces;
using FinancialControl.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace FinancialControl.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SalariesController : ControllerBase
{
    private readonly ISalaryService _salaryService;

    public SalariesController(ISalaryService salaryService)
    {
        _salaryService = salaryService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<SalaryResponseDto>>> GetSalaries()
    {
        var salaries = await _salaryService.GetAllSalariesAsync();
        return Ok(salaries);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SalaryResponseDto>> GetSalary(Guid id)
    {
        var salary = await _salaryService.GetSalaryByIdAsync(id);
        if (salary == null)
        {
            return NotFound();
        }
        return Ok(salary);
    }

    [HttpPost]
    public async Task<ActionResult<SalaryResponseDto>> PostSalary([FromBody] SalaryDto salaryDto)
    {
        var newSalary = await _salaryService.AddSalaryAsync(salaryDto);
        return CreatedAtAction(nameof(GetSalary), new { id = newSalary.Id }, newSalary);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutSalary(Guid id, [FromBody] SalaryDto salaryDto)
    {
        var result = await _salaryService.UpdateSalaryAsync(id, salaryDto);
        if (!result)
        {
            return NotFound();
        }
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSalary(Guid id)
    {
        var result = await _salaryService.DeleteSalaryAsync(id);
        if (!result)
        {
            return NotFound();
        }
        return NoContent();
    }
}
using FinancialControl.Application.DTOs;

namespace FinancialControl.Application.Interfaces;

public interface IExpenseService
{
    Task<IEnumerable<ExpenseResponseDto>> GetAllExpensesAsync();
    Task<ExpenseResponseDto?> GetExpenseByIdAsync(Guid id);
    Task<ExpenseResponseDto> AddExpenseAsync(ExpenseDto expenseDto);
    Task<bool> UpdateExpenseAsync(Guid id, ExpenseDto expenseDto);
    Task<bool> DeleteExpenseAsync(Guid id);
}
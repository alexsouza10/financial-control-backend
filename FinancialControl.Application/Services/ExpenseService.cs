using FinancialControl.Application.DTOs;
using FinancialControl.Application.Interfaces;
using FinancialControl.Domain.Entities;
using FinancialControl.Domain.Interfaces;

namespace FinancialControl.Application.Services;

public class ExpenseService : IExpenseService
{
    private readonly IGenericRepository<Expense> _expenseRepository;

    public ExpenseService(IGenericRepository<Expense> expenseRepository)
    {
        _expenseRepository = expenseRepository;
    }

    private ExpenseResponseDto MapToExpenseResponseDto(Expense expense)
    {
        return new ExpenseResponseDto
        {
            Id = expense.Id,
            Category = expense.Category,
            Value = expense.Value,
            PaymentMethod = expense.PaymentMethod,
            Installments = expense.Installments,
            Card = expense.Card,
            Date = expense.Date,
            CreatedAt = expense.CreatedAt,
            UpdatedAt = expense.UpdatedAt
        };
    }

    private Expense MapToExpenseEntity(ExpenseDto expenseDto)
    {
        return new Expense(
            expenseDto.Category,
            expenseDto.Value,
            expenseDto.PaymentMethod,
            expenseDto.Installments,
            expenseDto.Card,
            expenseDto.Date
        );
    }

    public async Task<IEnumerable<ExpenseResponseDto>> GetAllExpensesAsync()
    {
        var expenses = await _expenseRepository.GetAllAsync();
        return expenses.Select(MapToExpenseResponseDto).ToList();
    }

    public async Task<ExpenseResponseDto?> GetExpenseByIdAsync(Guid id)
    {
        var expense = await _expenseRepository.GetByIdAsync(id);
        return expense == null ? null : MapToExpenseResponseDto(expense);
    }

    public async Task<ExpenseResponseDto> AddExpenseAsync(ExpenseDto expenseDto)
    {
        var expense = MapToExpenseEntity(expenseDto);
        await _expenseRepository.AddAsync(expense);
        await _expenseRepository.SaveChangesAsync();
        return MapToExpenseResponseDto(expense);
    }

    public async Task<bool> UpdateExpenseAsync(Guid id, ExpenseDto expenseDto)
    {
        var expense = await _expenseRepository.GetByIdAsync(id);
        if (expense == null) return false;

        expense.Update(
            expenseDto.Category,
            expenseDto.Value,
            expenseDto.PaymentMethod,
            expenseDto.Installments,
            expenseDto.Card,
            expenseDto.Date
        );

        await _expenseRepository.UpdateAsync(expense);
        await _expenseRepository.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteExpenseAsync(Guid id)
    {
        var existingExpense = await _expenseRepository.GetByIdAsync(id);
        if (existingExpense == null) return false;

        await _expenseRepository.DeleteAsync(id);
        await _expenseRepository.SaveChangesAsync();
        return true;
    }
}
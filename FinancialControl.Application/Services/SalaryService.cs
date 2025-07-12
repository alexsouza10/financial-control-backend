using FinancialControl.Application.DTOs;
using FinancialControl.Application.Interfaces;
using FinancialControl.Domain.Entities;
using FinancialControl.Domain.Interfaces;

namespace FinancialControl.Application.Services;

public class SalaryService : ISalaryService
{
    private readonly IGenericRepository<Salary> _salaryRepository;

    public SalaryService(IGenericRepository<Salary> salaryRepository)
    {
        _salaryRepository = salaryRepository;
    }

    private SalaryResponseDto MapToSalaryResponseDto(Salary salary)
    {
        return new SalaryResponseDto
        {
            Id = salary.Id,
            Value = salary.Value,
            Date = salary.Date,
            CreatedAt = salary.CreatedAt,
            UpdatedAt = salary.UpdatedAt
        };
    }

    private Salary MapToSalaryEntity(SalaryDto salaryDto)
    {
        return new Salary(
            salaryDto.Value,
            salaryDto.Date
        );
    }

    public async Task<IEnumerable<SalaryResponseDto>> GetAllSalariesAsync()
    {
        var salaries = await _salaryRepository.GetAllAsync();
        return salaries.Select(MapToSalaryResponseDto).ToList();
    }

    public async Task<SalaryResponseDto?> GetSalaryByIdAsync(Guid id)
    {
        var salary = await _salaryRepository.GetByIdAsync(id);
        return salary == null ? null : MapToSalaryResponseDto(salary);
    }

    public async Task<SalaryResponseDto> AddSalaryAsync(SalaryDto salaryDto)
    {
        var salary = MapToSalaryEntity(salaryDto);
        await _salaryRepository.AddAsync(salary);
        await _salaryRepository.SaveChangesAsync();
        return MapToSalaryResponseDto(salary);
    }

    public async Task<bool> UpdateSalaryAsync(Guid id, SalaryDto salaryDto)
    {
        var salary = await _salaryRepository.GetByIdAsync(id);
        if (salary == null) return false;

        salary.Update(
            salaryDto.Value,
            salaryDto.Date
        );

        await _salaryRepository.UpdateAsync(salary);
        await _salaryRepository.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteSalaryAsync(Guid id)
    {
        var existingSalary = await _salaryRepository.GetByIdAsync(id);
        if (existingSalary == null) return false;

        await _salaryRepository.DeleteAsync(id);
        await _salaryRepository.SaveChangesAsync();
        return true;
    }
}
using FinancialControl.Application.DTOs;

namespace FinancialControl.Application.Interfaces;

public interface ISalaryService
{
    Task<IEnumerable<SalaryResponseDto>> GetAllSalariesAsync();
    Task<SalaryResponseDto?> GetSalaryByIdAsync(Guid id);
    Task<SalaryResponseDto> AddSalaryAsync(SalaryDto salaryDto);
    Task<bool> UpdateSalaryAsync(Guid id, SalaryDto salaryDto);
    Task<bool> DeleteSalaryAsync(Guid id);
}
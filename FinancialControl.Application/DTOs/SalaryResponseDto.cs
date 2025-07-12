namespace FinancialControl.Application.DTOs;

public class SalaryResponseDto : BaseResponseDto
{
    public decimal Value { get; set; }
    public DateTime Date { get; set; }
}
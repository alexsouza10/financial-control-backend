namespace FinancialControl.Application.DTOs;

public class ExpenseResponseDto : BaseResponseDto
{
    public string Category { get; set; } = string.Empty;
    public decimal Value { get; set; }
    public string PaymentMethod { get; set; } = string.Empty;
    public int Installments { get; set; }
    public string Card { get; set; } = string.Empty;
    public DateTime Date { get; set; }
}
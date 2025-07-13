namespace FinancialControl.Application.DTOs;

public class ExpenseResponseDto : BaseResponseDto
{
    public Guid CategoryId { get; set; } 
    public decimal Value { get; set; }
    public string PaymentMethod { get; set; } = string.Empty;
    public int Installments { get; set; }
    public string Card { get; set; } = string.Empty;
    public DateTime Date { get; set; }
}
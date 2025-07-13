using System.ComponentModel.DataAnnotations;

namespace FinancialControl.Application.DTOs;

public class ExpenseDto
{
    [Required(ErrorMessage = "O ID da categoria é obrigatório.")]
    public Guid CategoryId { get; set; }

    [Required(ErrorMessage = "O valor é obrigatório.")]
    public decimal Value { get; set; }

    [Required(ErrorMessage = "O método de pagamento é obrigatório.")]
    public string PaymentMethod { get; set; } = string.Empty;

    public int Installments { get; set; }
    public string? Card { get; set; } 

    [Required(ErrorMessage = "A data é obrigatória.")]
    public DateTime Date { get; set; }
}
// FinancialControl.Domain.Entities/Expense.cs
namespace FinancialControl.Domain.Entities;

public class Expense : BaseEntity
{
    public Guid CategoryId { get; private set; } 

    public decimal Value { get; private set; }
    public string PaymentMethod { get; private set; }
    public int Installments { get; private set; }
    public string? Card { get; private set; }
    public DateTime Date { get; private set; }

    public Expense(Guid categoryId, decimal value, string paymentMethod, int installments, string card, DateTime date) 
    {
        CategoryId = categoryId;
        Value = value;
        PaymentMethod = paymentMethod;
        Installments = installments;
        Card = card;
        Date = date;
    }

    public Expense()
    {
        PaymentMethod = string.Empty;
        Card = string.Empty;
    }


    public void Update(Guid categoryId, decimal value, string paymentMethod, int installments, string card, DateTime date) 
    {
        CategoryId = categoryId; 
        Value = value;
        PaymentMethod = paymentMethod;
        Installments = installments;
        Card = card;
        Date = date;
        SetUpdatedAt(DateTime.UtcNow);
    }
}
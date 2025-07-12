namespace FinancialControl.Domain.Entities;

public class Expense : BaseEntity
{
    public string Category { get; private set; }
    public decimal Value { get; private set; }
    public string PaymentMethod { get; private set; }
    public int Installments { get; private set; }
    public string Card { get; private set; }
    public DateTime Date { get; private set; }

    public Expense(string category, decimal value, string paymentMethod, int installments, string card, DateTime date)
    {
        Category = category;
        Value = value;
        PaymentMethod = paymentMethod;
        Installments = installments;
        Card = card;
        Date = date;
    }

    public void Update(string category, decimal value, string paymentMethod, int installments, string card, DateTime date)
    {
        Category = category;
        Value = value;
        PaymentMethod = paymentMethod;
        Installments = installments;
        Card = card;
        Date = date;
        SetUpdatedAt(DateTime.UtcNow);
    }
}
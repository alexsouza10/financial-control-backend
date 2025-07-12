namespace FinancialControl.Domain.Entities;

public class Salary : BaseEntity
{
    public decimal Value { get; private set; }
    public DateTime Date { get; private set; }

    public Salary(decimal value, DateTime date)
    {
        Value = value;
        Date = date;
    }

    public void Update(decimal value, DateTime date)
    {
        Value = value;
        Date = date;
        SetUpdatedAt(DateTime.UtcNow);
    }
}
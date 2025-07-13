namespace FinancialControl.Domain.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; private set; }
        public Category()
        {
            Name = string.Empty;
        }

        public Category(string name)
        {
            Name = name;
        }

        public void Update(string name)
        {
            Name = name;
        }
    }
}
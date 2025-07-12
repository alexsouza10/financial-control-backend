namespace FinancialControl.Application.DTOs
{
    public abstract class BaseResponseDto
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}

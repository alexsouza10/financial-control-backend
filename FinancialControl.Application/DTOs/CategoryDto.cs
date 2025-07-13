using System.ComponentModel.DataAnnotations;

namespace FinancialControl.Application.DTOs
{
    public class CategoryDto
    {
        [Required(ErrorMessage = "O nome da categoria é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome da categoria não pode exceder 100 caracteres.")]
        public string Name { get; set; } = string.Empty;
    }
}

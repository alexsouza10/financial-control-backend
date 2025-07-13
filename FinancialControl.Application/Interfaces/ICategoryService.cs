using FinancialControl.Application.DTOs;

namespace FinancialControl.Application.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryResponseDto>> GetAllCategoriesAsync();
        Task<CategoryResponseDto?> GetCategoryByIdAsync(Guid id);
        Task<CategoryResponseDto> AddCategoryAsync(CategoryDto categoryDto); 
        Task<bool> UpdateCategoryAsync(Guid id, CategoryDto categoryDto);  
        Task<bool> DeleteCategoryAsync(Guid id);
    }
}
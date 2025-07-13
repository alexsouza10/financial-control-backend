using FinancialControl.Application.DTOs;
using FinancialControl.Application.Interfaces;
using FinancialControl.Domain.Entities;
using FinancialControl.Domain.Interfaces;

namespace FinancialControl.Application.Services;

public class CategoryService : ICategoryService
{
    private readonly IGenericRepository<Category> _categoryRepository;

    public CategoryService(IGenericRepository<Category> categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    private CategoryResponseDto MapToCategoryResponseDto(Category category)
    {
        return new CategoryResponseDto
        {
            Id = category.Id,
            Name = category.Name,
            CreatedAt = category.CreatedAt,
            UpdatedAt = category.UpdatedAt
        };
    }

    private Category MapToCategoryEntity(CategoryDto categoryDto)
    {
        return new Category(
            categoryDto.Name
        );
    }

    public async Task<IEnumerable<CategoryResponseDto>> GetAllCategoriesAsync()
    {
        var categories = await _categoryRepository.GetAllAsync();
        return categories.Select(MapToCategoryResponseDto).ToList();
    }

    public async Task<CategoryResponseDto?> GetCategoryByIdAsync(Guid id)
    {
        var category = await _categoryRepository.GetByIdAsync(id);
        return category == null ? null : MapToCategoryResponseDto(category);
    }

    public async Task<CategoryResponseDto> AddCategoryAsync(CategoryDto categoryDto)
    {
        var existingCategory = await _categoryRepository.GetByConditionAsync(c => c.Name.ToLower() == categoryDto.Name.ToLower());
        if (existingCategory != null && existingCategory.Any())
        {
            throw new ApplicationException($"Uma categoria com o nome '{categoryDto.Name}' já existe.");
        }

        var category = MapToCategoryEntity(categoryDto);
        await _categoryRepository.AddAsync(category);
        await _categoryRepository.SaveChangesAsync();
        return MapToCategoryResponseDto(category);
    }

    public async Task<bool> UpdateCategoryAsync(Guid id, CategoryDto categoryDto)
    {
        var categoryToUpdate = await _categoryRepository.GetByIdAsync(id);
        if (categoryToUpdate == null)
        {
            return false;
        }

        var existingCategoryWithSameName = await _categoryRepository.GetByConditionAsync(c =>
            c.Name.ToLower() == categoryDto.Name.ToLower() && c.Id != id);

        if (existingCategoryWithSameName != null && existingCategoryWithSameName.Any())
        {
            throw new ApplicationException($"Uma categoria com o nome '{categoryDto.Name}' já existe.");
        }

        categoryToUpdate.Update(categoryDto.Name);


        await _categoryRepository.UpdateAsync(categoryToUpdate);
        await _categoryRepository.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteCategoryAsync(Guid id)
    {
        var existingCategory = await _categoryRepository.GetByIdAsync(id);
        if (existingCategory == null) return false;

        await _categoryRepository.DeleteAsync(id);
        await _categoryRepository.SaveChangesAsync();
        return true;
    }
}
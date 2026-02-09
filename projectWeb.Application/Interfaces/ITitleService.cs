using projectWeb.Application.DTOs.Title;

namespace projectWeb.Application.Interfaces;

public interface ITitleService
{
    Task<IEnumerable<TitleDto>> GetAllAsync();
    Task<TitleDto> GetByIdAsync(Guid id);
    Task<TitleDto> CreateAsync(CreateTitleDto dto);
    Task<TitleDto> UpdateAsync(Guid id, UpdateTitleDto dto);
    Task DeleteAsync(Guid id);
}

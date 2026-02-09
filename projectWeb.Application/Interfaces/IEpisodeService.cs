using projectWeb.Application.DTOs.Episode;

namespace projectWeb.Application.Interfaces;

public interface IEpisodeService
{
    Task<IEnumerable<EpisodeDto>> GetAllAsync();
    Task<IEnumerable<EpisodeDto>> GetByTitleIdAsync(Guid titleId);
    Task<EpisodeDto> GetByIdAsync(Guid id);
    Task<EpisodeDto> CreateAsync(CreateEpisodeDto dto);
    Task<EpisodeDto> UpdateAsync(Guid id, UpdateEpisodeDto dto);
    Task DeleteAsync(Guid id);
}

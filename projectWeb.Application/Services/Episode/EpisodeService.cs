using DefaultNamespace;
using projectWeb.Application.DTOs.Episode;
using projectWeb.Application.Interfaces;

namespace projectWeb.Application.Services.Episode;

public class EpisodeService : IEpisodeService
{
    private readonly IGeneral<DefaultNamespace.Episode> _episodeRepository;
    private readonly IGeneral<DefaultNamespace.Title> _titleRepository;
    private readonly ICloudinaryService _cloudinaryService;

    public EpisodeService(
        IGeneral<DefaultNamespace.Episode> episodeRepository,
        IGeneral<DefaultNamespace.Title> titleRepository,
        ICloudinaryService cloudinaryService)
    {
        _episodeRepository = episodeRepository;
        _titleRepository = titleRepository;
        _cloudinaryService = cloudinaryService;
    }

    public async Task<IEnumerable<EpisodeDto>> GetAllAsync()
    {
        var episodes = await _episodeRepository.GetAllAsync();
        return episodes.Select(e => new EpisodeDto
        {
            Id = e.Id,
            TitleId = e.TitleId,
            Title = e.Title,
            MovieUrl = e.movieUrl,
            Description = e.Description,
            ThumbnailUrl = e.ThumbnailUrl,
            SeasonNumber = e.SeasonNumber,
            EpisodeNumber = e.EpisodeNumber,
            DurationMinutes = e.DurationMinutes
        });
    }

    public async Task<IEnumerable<EpisodeDto>> GetByTitleIdAsync(Guid titleId)
    {
        var allEpisodes = await _episodeRepository.GetAllAsync();
        var episodes = allEpisodes.Where(e => e.TitleId == titleId);
        
        return episodes.Select(e => new EpisodeDto
        {
            Id = e.Id,
            TitleId = e.TitleId,
            Title = e.Title,
            MovieUrl = e.movieUrl,
            Description = e.Description,
            ThumbnailUrl = e.ThumbnailUrl,
            SeasonNumber = e.SeasonNumber,
            EpisodeNumber = e.EpisodeNumber,
            DurationMinutes = e.DurationMinutes
        });
    }

    public async Task<EpisodeDto> GetByIdAsync(Guid id)
    {
        var episode = await _episodeRepository.GetByIdAsync(id);
        if (episode == null)
            throw new KeyNotFoundException($"Episode with ID {id} not found.");

        return new EpisodeDto
        {
            Id = episode.Id,
            TitleId = episode.TitleId,
            Title = episode.Title,
            MovieUrl = episode.movieUrl,
            Description = episode.Description,
            ThumbnailUrl = episode.ThumbnailUrl,
            SeasonNumber = episode.SeasonNumber,
            EpisodeNumber = episode.EpisodeNumber,
            DurationMinutes = episode.DurationMinutes
        };
    }

    public async Task<EpisodeDto> CreateAsync(CreateEpisodeDto dto)
    {
        // Verify that the title exists
        var title = await _titleRepository.GetByIdAsync(dto.TitleId);
        if (title == null)
            throw new KeyNotFoundException($"Title with ID {dto.TitleId} not found.");

        // Upload video file
        var videoUrl = await _cloudinaryService.UploadVideoAsync(dto.VideoFile);

        // Upload thumbnail if provided
        string? thumbnailUrl = null;
        if (dto.ThumbnailImage != null)
            thumbnailUrl = await _cloudinaryService.UploadImageAsync(dto.ThumbnailImage);

        // Create the episode
        var episode = new DefaultNamespace.Episode
        {
            Id = Guid.NewGuid(),
            TitleId = dto.TitleId,
            Title = dto.Title,
            movieUrl = videoUrl,
            Description = dto.Description,
            ThumbnailUrl = thumbnailUrl,
            SeasonNumber = dto.SeasonNumber,
            EpisodeNumber = dto.EpisodeNumber,
            DurationMinutes = dto.DurationMinutes
        };

        await _episodeRepository.AddAsync(episode);

        return await GetByIdAsync(episode.Id);
    }

    public async Task<EpisodeDto> UpdateAsync(Guid id, UpdateEpisodeDto dto)
    {
        var episode = await _episodeRepository.GetByIdAsync(id);
        if (episode == null)
            throw new KeyNotFoundException($"Episode with ID {id} not found.");

        // Update basic properties
        if (dto.Title != null) episode.Title = dto.Title;
        if (dto.Description != null) episode.Description = dto.Description;
        if (dto.SeasonNumber.HasValue) episode.SeasonNumber = dto.SeasonNumber.Value;
        if (dto.EpisodeNumber.HasValue) episode.EpisodeNumber = dto.EpisodeNumber.Value;
        if (dto.DurationMinutes.HasValue) episode.DurationMinutes = dto.DurationMinutes.Value;

        // Upload new video if provided
        if (dto.VideoFile != null)
            episode.movieUrl = await _cloudinaryService.UploadVideoAsync(dto.VideoFile);

        // Upload new thumbnail if provided
        if (dto.ThumbnailImage != null)
            episode.ThumbnailUrl = await _cloudinaryService.UploadImageAsync(dto.ThumbnailImage);

        await _episodeRepository.UpdateAsync(episode);

        return await GetByIdAsync(id);
    }

    public async Task DeleteAsync(Guid id)
    {
        var episode = await _episodeRepository.GetByIdAsync(id);
        if (episode == null)
            throw new KeyNotFoundException($"Episode with ID {id} not found.");

        await _episodeRepository.DeleteAsync(id);
    }
}

using DefaultNamespace;
using projectWeb.Application.DTOs.Title;
using projectWeb.Application.Interfaces;

namespace projectWeb.Application.Services.Title;

public class TitleService : ITitleService
{
    private readonly IGeneral<DefaultNamespace.Title> _titleRepository;
    private readonly IGeneral<TitleGenre> _titleGenreRepository;
    private readonly IGeneral<Genre> _genreRepository;
    private readonly ICloudinaryService _cloudinaryService;

    public TitleService(
        IGeneral<DefaultNamespace.Title> titleRepository,
        IGeneral<TitleGenre> titleGenreRepository,
        IGeneral<Genre> genreRepository,
        ICloudinaryService cloudinaryService)
    {
        _titleRepository = titleRepository;
        _titleGenreRepository = titleGenreRepository;
        _genreRepository = genreRepository;
        _cloudinaryService = cloudinaryService;
    }

    public async Task<IEnumerable<TitleDto>> GetAllAsync()
    {
        var titles = await _titleRepository.GetAllAsync();
        
        var titleDtos = new List<TitleDto>();
        foreach (var title in titles)
        {
            var genres = await GetGenresForTitle(title.Id);
            
            titleDtos.Add(new TitleDto
            {
                Id = title.Id,
                TitleName = title.TitleName,
                Description = title.Description,
                ReleaseYear = title.ReleaseYear,
                DurationMinutes = title.DurationMinutes,
                Status = title.Status,
                AgeRating = title.AgeRating,
                ImdbRating = title.ImdbRating,
                MovieUrl = title.movieUrl,
                TotalSeasons = title.TotalSeasons,
                TypeName = title.Type.ToString(),
                TrailerUrl = title.TrailerUrl,
                CoverImageUrl = title.CoverImageUrl,
                BackdropImageUrl = title.BackdropImageUrl,
                Genres = genres
            });
        }
        
        return titleDtos;
    }

    public async Task<TitleDto> GetByIdAsync(Guid id)
    {
        var title = await _titleRepository.GetByIdAsync(id);
        if (title == null)
            throw new KeyNotFoundException($"Title with ID {id} not found.");

        var genres = await GetGenresForTitle(title.Id);

        return new TitleDto
        {
            Id = title.Id,
            TitleName = title.TitleName,
            Description = title.Description,
            ReleaseYear = title.ReleaseYear,
            DurationMinutes = title.DurationMinutes,
            Status = title.Status,
            AgeRating = title.AgeRating,
            ImdbRating = title.ImdbRating,
            MovieUrl = title.movieUrl,
            TotalSeasons = title.TotalSeasons,
            TypeName = title.Type.ToString(),
            TrailerUrl = title.TrailerUrl,
            CoverImageUrl = title.CoverImageUrl,
            BackdropImageUrl = title.BackdropImageUrl,
            Genres = genres
        };
    }

    public async Task<TitleDto> CreateAsync(CreateTitleDto dto)
    {
        // Upload files to Cloudinary
        string? movieUrl = null;
        string? trailerUrl = null;
        string? coverImageUrl = null;
        string? backdropImageUrl = null;

        if (dto.MovieFile != null)
            movieUrl = await _cloudinaryService.UploadVideoAsync(dto.MovieFile);

        if (dto.TrailerFile != null)
            trailerUrl = await _cloudinaryService.UploadVideoAsync(dto.TrailerFile);

        if (dto.CoverImage != null)
            coverImageUrl = await _cloudinaryService.UploadImageAsync(dto.CoverImage);

        if (dto.BackdropImage != null)
            backdropImageUrl = await _cloudinaryService.UploadImageAsync(dto.BackdropImage);

        // Validate the title type
        if (!Enum.IsDefined(typeof(DefaultNamespace.TitleType), dto.Type))
            throw new ArgumentException($"Invalid TitleType value: {dto.Type}");

        // Create the title
        var title = new DefaultNamespace.Title
        {
            Id = Guid.NewGuid(),
            TitleName = dto.TitleName,
            Description = dto.Description,
            ReleaseYear = dto.ReleaseYear,
            DurationMinutes = dto.DurationMinutes,
            Status = dto.Status,
            AgeRating = dto.AgeRating,
            ImdbRating = dto.ImdbRating,
            movieUrl = movieUrl,
            TotalSeasons = dto.TotalSeasons,
            Type = dto.Type,
            TrailerUrl = trailerUrl,
            CoverImageUrl = coverImageUrl,
            BackdropImageUrl = backdropImageUrl
        };

        await _titleRepository.AddAsync(title);

        // Add genres
        if (dto.GenreIds != null && dto.GenreIds.Any())
        {
            foreach (var genreId in dto.GenreIds)
            {
                var genre = await _genreRepository.GetByIdAsync(genreId);
                if (genre != null)
                {
                    var titleGenre = new TitleGenre
                    {
                        TitleId = title.Id,
                        GenreId = genreId
                    };
                    await _titleGenreRepository.AddAsync(titleGenre);
                }
            }
        }

        return await GetByIdAsync(title.Id);
    }

    public async Task<TitleDto> UpdateAsync(Guid id, UpdateTitleDto dto)
    {
        var title = await _titleRepository.GetByIdAsync(id);
        if (title == null)
            throw new KeyNotFoundException($"Title with ID {id} not found.");

        // Update basic properties
        if (dto.TitleName != null) title.TitleName = dto.TitleName;
        if (dto.Description != null) title.Description = dto.Description;
        if (dto.ReleaseYear.HasValue) title.ReleaseYear = dto.ReleaseYear.Value;
        if (dto.DurationMinutes.HasValue) title.DurationMinutes = dto.DurationMinutes;
        if (dto.Status != null) title.Status = dto.Status;
        if (dto.AgeRating != null) title.AgeRating = dto.AgeRating;
        if (dto.ImdbRating.HasValue) title.ImdbRating = dto.ImdbRating.Value;
        if (dto.TotalSeasons != null) title.TotalSeasons = dto.TotalSeasons;

        // Update title type if provided
        if (dto.Type.HasValue)
        {
            if (Enum.IsDefined(typeof(DefaultNamespace.TitleType), dto.Type.Value))
                title.Type = dto.Type.Value;
        }

        // Upload new files if provided
        if (dto.MovieFile != null)
            title.movieUrl = await _cloudinaryService.UploadVideoAsync(dto.MovieFile);

        if (dto.TrailerFile != null)
            title.TrailerUrl = await _cloudinaryService.UploadVideoAsync(dto.TrailerFile);

        if (dto.CoverImage != null)
            title.CoverImageUrl = await _cloudinaryService.UploadImageAsync(dto.CoverImage);

        if (dto.BackdropImage != null)
            title.BackdropImageUrl = await _cloudinaryService.UploadImageAsync(dto.BackdropImage);

        await _titleRepository.UpdateAsync(title);

        // Update genres if provided
        if (dto.GenreIds != null)
        {
            // This is a simplified approach. In production, you'd want to:
            // 1. Get existing TitleGenres
            // 2. Remove those not in the new list
            // 3. Add new ones
            // For now, this is a basic implementation
        }

        return await GetByIdAsync(id);
    }

    public async Task DeleteAsync(Guid id)
    {
        var title = await _titleRepository.GetByIdAsync(id);
        if (title == null)
            throw new KeyNotFoundException($"Title with ID {id} not found.");

        await _titleRepository.DeleteAsync(id);
    }

    private async Task<List<string>> GetGenresForTitle(Guid titleId)
    {
        var titleGenres = await _titleGenreRepository.GetAllAsync();
        var filteredTitleGenres = titleGenres.Where(tg => tg.TitleId == titleId);
        
        var genreNames = new List<string>();
        foreach (var tg in filteredTitleGenres)
        {
            var genre = await _genreRepository.GetByIdAsync(tg.GenreId);
            if (genre != null)
                genreNames.Add(genre.Name);
        }
        
        return genreNames;
    }
}

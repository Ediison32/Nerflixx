using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using projectWeb.Application.DTOs.Episode;
using projectWeb.Application.Interfaces;

namespace projectWeb.Api.Controllers.AdminControllers;

[Route("api/admin/episodes")]
[ApiController]
[Authorize(Roles = "Admin")] // Solo administradores pueden gestionar episodios
public class EpisodeController : ControllerBase
{
    private readonly IEpisodeService _episodeService;

    public EpisodeController(IEpisodeService episodeService)
    {
        _episodeService = episodeService;
    }

    /// <summary>
    /// Obtiene todos los episodios
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var episodes = await _episodeService.GetAllAsync();
        return Ok(episodes);
    }

    /// <summary>
    /// Obtiene todos los episodios de un título específico
    /// </summary>
    [HttpGet("by-title/{titleId}")]
    public async Task<IActionResult> GetByTitleId(Guid titleId)
    {
        var episodes = await _episodeService.GetByTitleIdAsync(titleId);
        return Ok(episodes);
    }

    /// <summary>
    /// Obtiene un episodio por ID
    /// </summary>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var episode = await _episodeService.GetByIdAsync(id);
        return Ok(episode);
    }

    /// <summary>
    /// Crea un nuevo episodio
    /// Acepta archivos multipart/form-data para subir video y thumbnail
    /// </summary>
    [HttpPost]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> Create([FromForm] CreateEpisodeDto dto)
    {
        var episode = await _episodeService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = episode.Id }, episode);
    }

    /// <summary>
    /// Actualiza un episodio existente
    /// Acepta archivos multipart/form-data para actualizar video y thumbnail
    /// </summary>
    [HttpPut("{id}")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> Update(Guid id, [FromForm] UpdateEpisodeDto dto)
    {
        var episode = await _episodeService.UpdateAsync(id, dto);
        return Ok(episode);
    }

    /// <summary>
    /// Elimina un episodio
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _episodeService.DeleteAsync(id);
        return NoContent();
    }
}

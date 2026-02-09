using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using projectWeb.Application.DTOs.Title;
using projectWeb.Application.Interfaces;

namespace projectWeb.Api.Controllers.AdminControllers;

[Route("api/admin/titles")]
[ApiController]
[Authorize(Roles = "Admin")] // Solo administradores pueden gestionar títulos
public class TitleController : ControllerBase
{
    private readonly ITitleService _titleService;

    public TitleController(ITitleService titleService)
    {
        _titleService = titleService;
    }

    /// <summary>
    /// Obtiene todos los títulos (películas y series)
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var titles = await _titleService.GetAllAsync();
        return Ok(titles);
    }

    /// <summary>
    /// Obtiene un título por ID
    /// </summary>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var title = await _titleService.GetByIdAsync(id);
        return Ok(title);
    }

    /// <summary>
    /// Crea un nuevo título (película o serie)
    /// Acepta archivos multipart/form-data para subir videos e imágenes
    /// </summary>
    [HttpPost]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> Create([FromForm] CreateTitleDto dto)
    {
        var title = await _titleService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = title.Id }, title);
    }

    /// <summary>
    /// Actualiza un título existente
    /// Acepta archivos multipart/form-data para actualizar videos e imágenes
    /// </summary>
    [HttpPut("{id}")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> Update(Guid id, [FromForm] UpdateTitleDto dto)
    {
        var title = await _titleService.UpdateAsync(id, dto);
        return Ok(title);
    }

    /// <summary>
    /// Elimina un título
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _titleService.DeleteAsync(id);
        return NoContent();
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UsersCrud.DTOs;
using UsersCrud.Services;

namespace UsersCrud.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class UsuarioController : ControllerBase
{
    private readonly UsuarioService _usuario;

    public UsuarioController(UsuarioService usuario)
    {
        _usuario = usuario;
    }

    [HttpPost]
    public async Task<IActionResult> CriarUsuario(UsuarioCriarDto dto)
    {
        var resposta = await _usuario.CriarUsuario(dto);
        return Ok(resposta);
    }

    [HttpGet]
    public async Task<IActionResult> ListarUsuarios()
    {
        var resposta = await _usuario.ListarUsuarios();
        return Ok(resposta);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> BuscarUsuario(int id)
    {
        var resposta = await _usuario.BuscarUsuario(id);
        if (resposta == null) return NotFound();
        return Ok(resposta);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> AtualizarUsuario(int id, UsuarioCriarDto dto)
    {
        var resposta = await _usuario.AtualizarUsuario(id, dto);
        if (resposta == null) return NotFound();
        return Ok(resposta);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletarUsuario(int id)
    {
        var resposta = await _usuario.DeletarUsuario(id);
        if (!resposta) return NotFound();
        return NoContent();
    }
}
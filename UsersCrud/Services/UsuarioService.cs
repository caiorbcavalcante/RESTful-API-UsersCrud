using Microsoft.EntityFrameworkCore;
using UsersCrud.Data;
using UsersCrud.DTOs;
using UsersCrud.Models;

namespace UsersCrud.Services;

public class UsuarioService
{
    private readonly AppDbContext _context;

    public UsuarioService(AppDbContext context)
    {
        _context = context;

    }
    public async Task<UsuarioRespostasDto> CriarUsuario(UsuarioCriarDto dto)
    {
        var usuario = new Usuario
        {
            Nome = dto.Nome,
            Email = dto.Email,
            Senha = BCrypt.Net.BCrypt.HashPassword(dto.Senha)
        };

        _context.Usuarios.Add(usuario);
        await _context.SaveChangesAsync();

        return new UsuarioRespostasDto
        {
            Id = usuario.Id,
            Nome = usuario.Nome,
            Email = usuario.Email,  
            CriadoEm = usuario.CriadoEm
        };
    }

    public async Task<List<UsuarioRespostasDto>>  ListarUsuarios()
    {
        var usuarios = await _context.Usuarios.ToListAsync();

        return usuarios.Select(user => new UsuarioRespostasDto
        {
            Id = user.Id,
            Nome = user.Nome,
            Email = user.Email,
            CriadoEm = user.CriadoEm
        }).ToList();

    }

    public async Task<UsuarioRespostasDto?> BuscarUsuario(int id)
    {
        var usuario = await _context.Usuarios.FindAsync(id);

        if (usuario == null)
        {
            return null;
        }

        return new UsuarioRespostasDto
        {
            Id = usuario.Id,
            Nome = usuario.Nome,
            Email = usuario.Email,  
            CriadoEm = usuario.CriadoEm

        };
    }

    public async Task<UsuarioRespostasDto?> AtualizarUsuario(int id, UsuarioCriarDto dto)
    {
        var usuario = await _context.Usuarios.FindAsync(id);

        if (usuario == null)
        {
            return null;
        }

        usuario.Nome = dto.Nome;
        usuario.Email = dto.Email;
        usuario.Senha = BCrypt.Net.BCrypt.HashPassword(dto.Senha);

        await _context.SaveChangesAsync();

        return new UsuarioRespostasDto
        {
            Id = usuario.Id,
            Nome = usuario.Nome,
            Email = usuario.Email,
            CriadoEm = usuario.CriadoEm
        };
    }

    public async Task<bool> DeletarUsuario(int id)
    {
        var usuario = await _context.Usuarios.FindAsync(id);

        if (usuario == null)
        {
            return false;
        }

        _context.Usuarios.Remove(usuario);
        await _context.SaveChangesAsync();

        return true;
    }
}
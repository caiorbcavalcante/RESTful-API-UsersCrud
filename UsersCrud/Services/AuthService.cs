using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UsersCrud.Data;
using UsersCrud.DTOs;

namespace UsersCrud.Services;

public class AuthService
{
    private readonly AppDbContext _context;
    private readonly IConfiguration _config;

    public AuthService(AppDbContext context, IConfiguration config)
    {
        _context = context;
        _config = config;
    }

    public string? Login(LoginDto dto)
    {
        // 1. buscar usuário pelo email
        var usuario = _context.Usuarios.FirstOrDefault(u => u.Email == dto.Email);
        // 2. se não encontrar, retornar null
        if (usuario == null) return null;
        // 3. verificar a senha com BCrypt.Net.BCrypt.Verify()
        var senhaHash = usuario.Senha;
        var senhaOk = BCrypt.Net.BCrypt.Verify(dto.Senha, senhaHash);
        // 4. se senha errada, retornar null
        if (!senhaOk) return null;
        // 5. gerar e retornar o token (cole o bloco abaixo aqui)
        var key = new SymmetricSecurityKey(
        Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));

    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

    var claims = new[]
    {
        new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
        new Claim(ClaimTypes.Email, usuario.Email)
    };

    var token = new JwtSecurityToken(
        issuer: _config["Jwt:Issuer"],
        audience: _config["Jwt:Audience"],
        claims: claims,
        expires: DateTime.UtcNow.AddHours(1),
        signingCredentials: creds
    );

    return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
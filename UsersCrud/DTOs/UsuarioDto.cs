using System.ComponentModel.DataAnnotations;

namespace UsersCrud.DTOs;

public class UsuarioCriarDto
{
    [Required]
    [MinLength(3)]
    public string Nome { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    [MinLength(6)]
    public string Senha { get; set; } = string.Empty;
    
}

public class UsuarioRespostasDto
{
    public int Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Nome { get; set; } = string.Empty;
    public DateTime CriadoEm { get; set; }

}

public class LoginDto
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    [MinLength(6)]
    public string Senha { get; set; } = string.Empty;
}
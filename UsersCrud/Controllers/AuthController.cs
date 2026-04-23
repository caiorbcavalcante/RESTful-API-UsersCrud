using Microsoft.AspNetCore.Mvc;
using UsersCrud.DTOs;
using UsersCrud.Services;

namespace UsersCrud.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AuthService _service;

    public AuthController(AuthService service)
    {
        _service = service;
    }

    [HttpPost("login")]
    public IActionResult Login(LoginDto dto)
    {
        var token = _service.Login(dto);
        if (token == null) return Unauthorized();
        return Ok(new { token });
    }
}
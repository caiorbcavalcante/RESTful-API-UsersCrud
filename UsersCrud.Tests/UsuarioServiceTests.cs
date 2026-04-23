using Microsoft.EntityFrameworkCore;
using UsersCrud.Data;
using UsersCrud.DTOs;
using UsersCrud.Services;
using FluentAssertions;

namespace UsersCrud.Tests;

public class UsuarioServiceTests
{
    private AppDbContext CriarContexto()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
        .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
        .Options;

    return new AppDbContext(options);
    }

[Fact]
public async Task CriarUsuario_DeveRetornarUsuarioCriado()
{
    // Arrange
    var context = CriarContexto();
    var service = new UsuarioService(context);
    var dto = new UsuarioCriarDto
    {
        Nome = "Caio",
        Email = "caio@email.com",
        Senha = "123456"
    };

    // Act
    var resultado = await service.CriarUsuario(dto);

    // Assert
    resultado.Should().NotBeNull();
    resultado.Nome.Should().Be("Caio");
    resultado.Email.Should().Be("caio@email.com");
}

[Fact]
public async Task BuscarUsuario_DeveRetornarUsuario_QuandoExistir()
{
    // Arrange
    var context = CriarContexto();
    var service = new UsuarioService(context);
    var dto = new UsuarioCriarDto
    {
        Nome = "Caio",
        Email = "caio@email.com",
        Senha = "123456"
    };
    // Act
    var usuarioCriado = await service.CriarUsuario(dto);    
    var resultado = await service.BuscarUsuario(usuarioCriado.Id);

    // Assert
    resultado.Should().NotBeNull();
    resultado.Nome.Should().Be("Caio");
    resultado.Email.Should().Be("caio@email.com");
}

[Fact]
public async Task BuscarUsuario_DeveRetornarNull_QuandoNaoExistir()
{
    // Arrange
    var context = CriarContexto();
    var service = new UsuarioService(context);
    
    // Act   
    var resultado = await service.BuscarUsuario(999);
    // Assert
    resultado.Should().BeNull();
}
}
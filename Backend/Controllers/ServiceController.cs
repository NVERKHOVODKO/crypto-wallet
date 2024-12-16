using Microsoft.AspNetCore.Mvc;
using UP.Migrations.Services.Interfaces;

namespace UP.Controllers;

[ApiController]
[Produces("application/json")]
[Route("[controller]")]
public class ServiceController(IServiceService serviceService) : ControllerBase
{
    /// <summary>
    /// Получает список всех сервисов.
    /// </summary>
    /// <returns>Список сервисов.</returns>
    /// <response code="200">Возвращает список всех доступных сервисов.</response>
    /// <response code="500">Ошибка на сервере.</response>
    [HttpGet]
    public async Task<IActionResult> GetServices()
    {
        var users = await serviceService.GetServices();
        return Ok(users);
    }
}
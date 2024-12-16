namespace UP.Controllers;

/// <summary>
/// Контроллер для работы с внешними сервисами
/// </summary>
/// <param name="serviceService"></param>
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
    /// <response code="500">Серверная ошибка.</response>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<User>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetServices()
    {
        var users = await serviceService.GetServices();
        return Ok(users);
    }
}
namespace UP.Controllers;

/// <summary>
/// Контроллер для работы с пользователями
/// </summary>
/// <param name="dbRepository"></param>
/// <param name="userService"></param>
[ApiController]
[Route("user")]
public class UserController(IDbRepository dbRepository, IUserService userService) : ControllerBase
{
    /// <summary>
    /// Редактирует логин пользователя.
    /// </summary>
    /// <param name="request">Объект, содержащий новый логин пользователя.</param>
    /// <returns>Статус операции и сообщение о результате.</returns>
    /// <response code="200">Логин успешно изменен.</response>
    /// <response code="400">Неверные данные для логина.</response>
    [HttpPut("edit-user-login")]
    public async Task<ActionResult> EditLoginAsync([FromBody] EditUserLoginRequest request)
    {
        await userService.EditLoginAsync(request);
        
        return Ok("Логин изменен успешно");
    }

    /// <summary>
    /// Проверяет уникальность логина для пользователя.
    /// </summary>
    /// <param name="login">Логин, который нужно проверить.</param>
    /// <param name="id">ID пользователя, который исключается из проверки.</param>
    /// <returns>True, если логин уникален, иначе False.</returns>
    /// <response code="200">Результат проверки логина (true/false).</response>
    /// <response code="400">Неверные параметры запроса.</response>
    [HttpPost("is-login-unique/{login}/{id}")]
    public async Task<IActionResult> IsLoginUniqueAsync(string login, Guid id)
    {
        var isLoginUnique = await userService.IsLoginUniqueAsync(login, id);
        
        return Ok(isLoginUnique);
    }
    
    /// <summary>
    /// Редактирует пароль пользователя.
    /// </summary>
    /// <param name="request">Объект, содержащий новый пароль пользователя.</param>
    /// <returns>Статус операции и сообщение о результате.</returns>
    /// <response code="200">Пароль успешно изменен.</response>
    /// <response code="400">Неверный формат пароля.</response>
    [HttpPut("edit-user-password")]
    public async Task<ActionResult> EditUserPasswordAsync([FromBody] EditUserPasswordRequest request)
    {
        await userService.EditUserPasswordAsync(request);
        
        return Ok("Пароль изменен успешно");
    }

    /// <summary>
    /// Редактирует email пользователя.
    /// </summary>
    /// <param name="request">Объект, содержащий новый email пользователя.</param>
    /// <returns>Статус операции и сообщение о результате.</returns>
    /// <response code="200">Email успешно изменен.</response>
    /// <response code="400">Неверный или уже существующий email.</response>
    [HttpPut("edit-user-email")]
    public async Task<ActionResult> EditUserEmail([FromBody] EditUserEmailRequest request)
    {
        await userService.EditUserEmailAsync(request);
        
        return Ok("Email изменен успешно");
    }

    /// <summary>
    /// Получает историю логинов пользователя по его ID.
    /// </summary>
    /// <param name="id">ID пользователя для поиска его истории логинов.</param>
    /// <returns>Список логинов пользователя.</returns>
    /// <response code="200">Список истории логинов пользователя.</response>
    /// <response code="404">Пользователь не найден.</response>
    [HttpGet("get-login-history/{id}")]
    public async Task<IActionResult> GetUserLoginHistory(Guid id)
    {
        var loginHistory = await userService.GetUserLoginHistoryAsync(id);
        
        return Ok(loginHistory);
    }

    /// <summary>
    /// Получает текущий логин пользователя по его ID.
    /// </summary>
    /// <param name="id">ID пользователя.</param>
    /// <returns>Текущий логин пользователя.</returns>
    /// <response code="200">Текущий логин пользователя.</response>
    /// <response code="404">Пользователь не найден.</response>
    [HttpGet("login/{id}")]
    public async Task<IActionResult> GetUserLoginByIdAsync(Guid id)
    {
        var login = await userService.GetUserLoginByIdAsync(id);
        
        return Ok(login);
    }

    /// <summary>
    /// Удаляет аккаунт пользователя по его ID.
    /// </summary>
    /// <param name="id">ID пользователя для удаления аккаунта.</param>
    /// <returns>Сообщение о результате операции.</returns>
    /// <response code="200">Пользователь успешно удален.</response>
    /// <response code="404">Пользователь не найден.</response>
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAccount(Guid id)
    {
        var existingUser = await dbRepository.Get<User>().FirstOrDefaultAsync(x => x.Id == id);
        if (existingUser == null)
            throw new EntityNotFoundException("User not found");

        existingUser.IsDeleted = true;
        existingUser.DateUpdated = DateTime.UtcNow;

        await dbRepository.SaveChangesAsync();
        
        return Ok("Пользователь удален");
    }

    /// <summary>
    /// Меняет логин пользователя.
    /// </summary>
    /// <param name="id">ID пользователя.</param>
    /// <param name="newLogin">Новый логин для пользователя.</param>
    /// <returns>Сообщение о результате операции.</returns>
    /// <response code="200">Логин успешно изменен.</response>
    /// <response code="400">Неверный или уже используемый логин.</response>
    [HttpPut("change-login")]
    public async Task<ActionResult> ChangeLoginAsync(Guid id, string newLogin)
    {
        await userService.ChangeLoginAsync(id, newLogin);
        
        return Ok("Пароль успешно изменен");
    }

    /// <summary>
    /// Change the password of a user.
    /// </summary>
    /// <param name="request">Request containing the user ID and new password.</param>
    /// <returns>Status of the operation.</returns>
    [HttpPut("change-password")]
    public async Task<ActionResult> ChangePasswordAsync([FromBody] ChangePasswordRequest request)
    {
        await userService.ChangePasswordAsync(request);
        
        return Ok("Пароль успешно изменен");
    }

    /// <summary>
    /// Создает нового пользователя.
    /// </summary>
    /// <param name="request">Объект с данными для создания пользователя.</param>
    /// <returns>Сообщение о результате создания пользователя.</returns>
    /// <response code="200">Пользователь успешно создан.</response>
    /// <response code="400">Некорректные данные для создания пользователя.</response>
    [HttpPost]
    public async Task<IActionResult> CreateAsync(CreateUserRequest request)
    {
        await userService.CreateAsync(request);
        
        return Ok("Пользователь успешно создан");
    }

    /// <param name="email">Email для проверки.</param>
    /// <param name="id">ID пользователя, для которого проверяется уникальность.</param>
    /// <returns>True, если email уникален, иначе False.</returns>
    /// <response code="200">Результат проверки email (true/false).</response>
    /// <response code="400">Неверные параметры запроса.</response>
    [HttpPost("is-email-unique-async/{email}/{id}")]
    public async Task<bool> IsEmailUniqueAsync(string email, Guid id)
    {
        var isEmailUnique = await userService.IsEmailUniqueAsync(email, id);
        
        return isEmailUnique;
    }

    /// <summary>
    /// Проверяет уникальность логина для пользователя.
    /// </summary>
    /// <param name="login">Логин, который нужно проверить на уникальность.</param>
    /// <returns>True, если логин уникален, иначе False.</returns>
    /// <response code="200">Результат проверки логина (true/false).</response>
    /// <response code="400">Неверные параметры запроса.</response>
    [HttpPost("is-login-unique{login}")]
    public async Task<IActionResult> IsLoginUniqueAsync(string login)
    {
        var isLoginUnique = await userService.IsLoginUniqueAsync(login);
        
        return Ok(isLoginUnique);
    }
}
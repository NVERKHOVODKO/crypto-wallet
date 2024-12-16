﻿using UP.Exceptions;

namespace UP.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthorizationController : ControllerBase
{
    private readonly IDbRepository _dbRepository;
    private readonly IHashHelpers _hashHelpers;

    public AuthorizationController(
        IDbRepository dbRepository,
        IHashHelpers hashHelpers)
    {
        _dbRepository = dbRepository;
        _hashHelpers = hashHelpers;
    }

    /// <summary>
    /// Аутентификация пользователя.
    /// </summary>
    /// <param name="request">Данные для авторизации.</param>
    /// <returns>Информация о пользователе.</returns>
    /// <response code="200">Возвращает информацию о пользователе.</response>
    /// <response code="404">Пользователь не найден или аккаунт заблокирован.</response>
    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login([FromBody] AuthenticationRequest request)
    {
        var users = await _dbRepository.Get<User>()
            .Where(x => x.Login == request.Login)
            .ToListAsync();

        var user = users.FirstOrDefault(x => x.Password == HashHandler.HashPassword(request.Password, x.Salt));

        if (user == null) throw new EntityNotFoundException("Пользователь не найден");

        if (user.IsBlocked)
            throw new EntityNotFoundException("Ваш аккаунт заблокирован");

        var record = new LoginHistory
        {
            Id = Guid.NewGuid(),
            UserId = user.Id,
            DateCreated = DateTime.UtcNow,
            IPAddress = LoginHistory.GetIPAddress()
        };
        await _dbRepository.Add(record);
        await _dbRepository.SaveChangesAsync();

        return Ok(user);
    }

    /// <summary>
    /// Регистрация нового пользователя.
    /// </summary>
    /// <param name="request">Данные для регистрации.</param>
    /// <returns>Сообщение о результатах регистрации.</returns>
    /// <response code="200">Успешная регистрация.</response>
    /// <response code="400">Ошибки валидации данных.</response>
    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> RegisterNewUser([FromBody] RegisterRequest request)
    {
        if (request.Login == null || request.Password == null || request.Email == null)
            throw new IncorrectDataException("Заполните все поля");
        switch (request.Login.Length)
        {
            case > 20:
                throw new IncorrectDataException("Логин должен быть короче 20 символов");
            case < 4:
                throw new IncorrectDataException("Логин должен быть длиннее 4 символов");
        }

        switch (request.Password.Length)
        {
            case > 40:
                throw new IncorrectDataException("Пароль должен быть короче 40 символов");
            case < 4:
                throw new IncorrectDataException("Пароль должен быть длиннее 4 символов");
        }

        if (!IsEmailValid(request.Email)) throw new IncorrectDataException("Неверный формат email");
        if (await _dbRepository.Get<User>().FirstOrDefaultAsync(x => x.Email == request.Email) != null)
            throw new IncorrectDataException("Email уже используется");
        if (await _dbRepository.Get<User>().FirstOrDefaultAsync(x => x.Login == request.Login) != null)
            throw new IncorrectDataException("Логин уже используется");

        var salt = _hashHelpers.GenerateSalt(30);
        var entity = new User
        {
            Login = request.Login,
            Password = _hashHelpers.HashPassword(request.Password, salt),
            Email = request.Email,
            DateCreated = DateTime.UtcNow,
            IsDeleted = false,
            IsBlocked = false,
            RoleId = 1,
            Salt = salt
        };
        await _dbRepository.Add(entity);
        await _dbRepository.SaveChangesAsync();

        return Ok("Аккаунт успешно создан");

        bool IsEmailValid(string email)
        {
            const string regex = @"^[^@\s]+@[^@\s]+\.(com|net|org|gov)$";
            return Regex.IsMatch(email, regex, RegexOptions.IgnoreCase);
        }
    }
}
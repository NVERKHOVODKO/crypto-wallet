namespace Back.Services;

/// <summary>
/// Сервис для работы с пользователями. Предоставляет методы проверки уникальности данных,
/// создания, редактирования и получения информации о пользователях.
/// </summary>
/// <param name="dbRepository">Репозиторий для взаимодействия с базой данных.</param>
/// <param name="hashHelper">Сервис для работы с хэшированием.</param>
[AutoInterface]
public class UserService(IDbRepository dbRepository, IHashHelper hashHelper) : IUserService
{
    /// <summary>
    /// Проверяет, уникален ли указанный логин.
    /// </summary>
    /// <param name="login">Логин для проверки.</param>
    /// <returns>True, если логин уникален, иначе False.</returns>
    public async Task<bool> IsLoginUniqueAsync(string login)
    {
        var usersWithSameLogin = await dbRepository.Get<User>()
            .Where(x => x.Login == login)
            .ToListAsync();
        
        return usersWithSameLogin.Count is 0;
    }
    
    /// <summary>
    /// Проверяет, уникален ли указанный логин, исключая пользователя с заданным ID.
    /// </summary>
    /// <param name="login">Логин для проверки.</param>
    /// <param name="id">ID пользователя, которого нужно исключить из проверки.</param>
    /// <returns>True, если логин уникален, иначе False.</returns>
    public async Task<bool> IsLoginUniqueAsync(string login, Guid id)
    {
        return await dbRepository.Get<User>(x => x.Login == login && x.Id != id).FirstOrDefaultAsync() == null;
    }
    
    /// <summary>
    /// Проверяет, уникален ли указанный email, исключая пользователя с заданным ID.
    /// </summary>
    /// <param name="email">Email для проверки.</param>
    /// <param name="id">ID пользователя, которого нужно исключить из проверки.</param>
    /// <returns>True, если email уникален, иначе False.</returns>
    public async Task<bool> IsEmailUniqueAsync(string email, Guid id)
    {
        var usersWithSameEmail = await dbRepository.Get<User>()
            .Where(x => x.Email == email && x.Id != id)
            .ToListAsync();
        
        return usersWithSameEmail.Count is 0;
    }

    /// <summary>
    /// Создает нового пользователя.
    /// </summary>
    /// <param name="request">Запрос на создание пользователя.</param>
    /// <exception cref="EntityNotFoundException">Выбрасывается, если данные пользователя некорректны.</exception>
    /// <exception cref="IncorrectDataException">Выбрасывается, если никнейм не уникален.</exception>
    public async Task CreateAsync(CreateUserRequest request)
    {
        if (request is null)
            throw new EntityNotFoundException("Пользователь не указан");
        if (request.Nickname is null)
            throw new EntityNotFoundException("Никнейм не может быть пустым");

        switch (request.Nickname.Length)
        {
            case < 4:
                throw new EntityNotFoundException("Никнейм не может быть короче 4 символов");
            case > 20:
                throw new EntityNotFoundException("Никнейм не может быть длиннее 20 символов");
        }

        if (request.Password is null)
            throw new EntityNotFoundException("Пароль не может быть пустым");

        switch (request.Password.Length)
        {
            case < 4:
                throw new EntityNotFoundException("Пароль не может быть короче 4 символов");
            case > 40:
                throw new EntityNotFoundException("Пароль не может быть длиннее 40 символов");
        }

        var user = new User();
        if (!await IsLoginUniqueAsync(request.Nickname)) throw new IncorrectDataException("Nickname must be unique");
        user.Login = request.Nickname;
        user.Salt = hashHelper.GenerateSalt(30);
        user.Password = hashHelper.HashPassword(request.Password, user.Salt);
        user.DateCreated = DateTime.UtcNow;
        user.Id = request.Id;
        
        await dbRepository.Add(user);
        await dbRepository.SaveChangesAsync();
    }
    
    /// <summary>
    /// Изменяет пароль пользователя.
    /// </summary>
    /// <param name="request">Запрос на изменение пароля.</param>
    /// <exception cref="EntityNotFoundException">Выбрасывается, если пользователь не найден.</exception>
    /// <exception cref="IncorrectDataException">Выбрасывается, если пароли не совпадают или некорректны.</exception>
    public async Task ChangePasswordAsync([FromBody] ChangePasswordRequest request)
    {
        var existingUser = await dbRepository.Get<User>().FirstOrDefaultAsync(x => x.Id == request.Id);
        if (existingUser is null)
            throw new EntityNotFoundException("User not found");
        
        if (request.Password != request.PasswordRepeat) 
            throw new IncorrectDataException("Пароли не совпадают");
        
        switch (request.Password.Length)
        {
            case 0:
                throw new IncorrectDataException("Заполните данные");
            case > 32:
                throw new IncorrectDataException("Пароль должен быть короче 32 символов");
            case < 4:
                throw new IncorrectDataException("Пароль должен быть длиннее 4 символов");
        }

        existingUser.Password = hashHelper.HashPassword(request.Password, existingUser.Salt);
        existingUser.DateUpdated = DateTime.UtcNow;

        await dbRepository.SaveChangesAsync();
    }

    /// <summary>
    /// Изменяет логин пользователя.
    /// </summary>
    /// <param name="id">ID пользователя.</param>
    /// <param name="newLogin">Новый логин пользователя.</param>
    /// <exception cref="EntityNotFoundException">Выбрасывается, если пользователь не найден.</exception>
    /// <exception cref="IncorrectDataException">Выбрасывается, если логин некорректен.</exception>
    public async Task ChangeLoginAsync(Guid id, string newLogin)
    {
        var existingUser = await dbRepository.Get<User>().FirstOrDefaultAsync(x => x.Id == id);
        if (existingUser is null)
            throw new EntityNotFoundException("User not found");
        switch (newLogin.Length)
        {
            case 0:
                throw new IncorrectDataException("Заполните данные");
            case > 32:
                throw new IncorrectDataException("Пароль должен быть короче 32 символов");
            case < 4:
                throw new IncorrectDataException("Пароль должен быть длиннее 4 символов");
        }

        existingUser.Login = newLogin;
        existingUser.DateUpdated = DateTime.UtcNow;

        await dbRepository.SaveChangesAsync();
    }
    
    /// <summary>
    /// Получает логин пользователя по его ID.
    /// </summary>
    /// <param name="id">ID пользователя.</param>
    /// <returns>Логин пользователя.</returns>
    /// <exception cref="EntityNotFoundException">Выбрасывается, если пользователь не найден.</exception>
    public async Task<string> GetUserLoginByIdAsync(Guid id)
    {
        var existingUser = await dbRepository.Get<User>().FirstOrDefaultAsync(x => x.Id == id);
        if (existingUser is null)
            throw new EntityNotFoundException("User not found");
        
        return existingUser.Login;
    }
    
    /// <summary>
    /// Получает историю логинов пользователя.
    /// </summary>
    /// <param name="id">ID пользователя.</param>
    /// <returns>Список записей истории логинов.</returns>
    /// <exception cref="EntityNotFoundException">Выбрасывается, если история логинов отсутствует.</exception>
    public async Task<List<LoginHistory>> GetUserLoginHistoryAsync(Guid id)
    {
        var loginHistory = await dbRepository.Get<LoginHistory>().Where(x => x.UserId == id).ToListAsync();
        if (loginHistory is null)
            throw new EntityNotFoundException("loginHistory not found");
        
        return loginHistory;
    }
    
    /// <summary>
    /// Изменяет адрес электронной почты пользователя.
    /// </summary>
    /// <param name="request">Запрос с данными для изменения адреса электронной почты пользователя.</param>
    /// <exception cref="EntityNotFoundException">
    /// Выбрасывается, если пользователь не найден.
    /// </exception>
    /// <exception cref="IncorrectDataException">
    /// Выбрасывается, если введенный адрес электронной почты пуст, не уникален, не соответствует требованиям длины
    /// или имеет некорректный формат.
    /// </exception>
    public async Task EditUserEmailAsync([FromBody] EditUserEmailRequest request)
    {
        var existingUser = await dbRepository.Get<User>().FirstOrDefaultAsync(x => x.Id == request.Id);
        if (existingUser is null)
            throw new EntityNotFoundException("Пользователь не найден");

        if (request.Email is null)
            throw new IncorrectDataException("Email не может быть пустым");

        switch (request.Email.Length)
        {
            case < 4:
                throw new IncorrectDataException("Email не может быть короче 4 символов");
            case > 20:
                throw new IncorrectDataException("Email не может быть длиннее 20 символов");
        }

        if (!IsEmailValid(request.Email))
            throw new IncorrectDataException("Email имеет некорректный формат");

        if (!await IsEmailUniqueAsync(request.Email, existingUser.Id))
            throw new IncorrectDataException("Email должен быть уникальным");

        existingUser.Login = request.Email;

        await dbRepository.SaveChangesAsync();
    }
    
    /// <summary>
    /// Проверяет валидность email.
    /// </summary>
    /// <param name="email">Email для проверки.</param>
    /// <returns>True, если email валиден, иначе False.</returns>
    public bool IsEmailValid(string email) => Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.(com|net|org|gov)$", RegexOptions.IgnoreCase);

    /// <summary>
    /// Изменяет пароль пользователя.
    /// </summary>
    /// <param name="request">Запрос с данными для изменения пароля пользователя.</param>
    /// <exception cref="EntityNotFoundException">
    /// Выбрасывается, если пользователь не найден или пароль не задан,
    /// либо если пароль не соответствует требованиям по длине.
    /// </exception>
    public async Task EditUserPasswordAsync([FromBody] EditUserPasswordRequest request)
    {
        var existingUser = await dbRepository.Get<User>().FirstOrDefaultAsync(x => x.Id == request.Id);
        if (existingUser is null)
            throw new EntityNotFoundException("Пользователь не найден");

        if (request.Password is null)
            throw new EntityNotFoundException("Пароль не может быть пустым");

        switch (request.Password.Length)
        {
            case < 4:
                throw new EntityNotFoundException("Пароль не может содержать менее 4 символов");
            case > 20:
                throw new EntityNotFoundException("Пароль не может содержать более 20 символов");
        }

        existingUser.Password = hashHelper.HashPassword(request.Password, existingUser.Salt);

        await dbRepository.SaveChangesAsync();
    }
    
    /// <summary>
    /// Редактирует логин пользователя.
    /// </summary>
    /// <param name="request">Запрос с данными для изменения логина пользователя.</param>
    /// <exception cref="EntityNotFoundException">Выбрасывается, если пользователь не найден или логин не задан.</exception>
    /// <exception cref="IncorrectDataException">Выбрасывается, если логин не уникален.</exception>
    public async Task EditLoginAsync([FromBody] EditUserLoginRequest request)
    {
        var existingUser = await dbRepository.Get<User>().FirstOrDefaultAsync(x => x.Id == request.Id);
        if (existingUser is null)
            throw new EntityNotFoundException("Пользователь не найден");

        if (request.Login is null)
            throw new EntityNotFoundException("Логин не может быть пустым");

        switch (request.Login.Length)
        {
            case < 4:
                throw new EntityNotFoundException("Логин не может содержать менее 4 символов");
            case > 20:
                throw new EntityNotFoundException("Логин не может содержать более 20 символов");
        }

        if (!await IsLoginUniqueAsync(request.Login, existingUser.Id))
            throw new IncorrectDataException("Логин должен быть уникальным");

        existingUser.Login = request.Login;
        await dbRepository.SaveChangesAsync();
    }
    
    /// <summary>
    /// Устанавливает статус удаления равный true
    /// </summary>
    /// <param name="id"></param>
    /// <exception cref="EntityNotFoundException"></exception>
    public async Task DeleteAccountAsync(Guid id)
    {
        var existingUser = await dbRepository.Get<User>().FirstOrDefaultAsync(x => x.Id == id);
        if (existingUser == null)
            throw new EntityNotFoundException("User not found");

        existingUser.IsDeleted = true;
        existingUser.DateUpdated = DateTime.UtcNow;

        await dbRepository.SaveChangesAsync();
    }
}
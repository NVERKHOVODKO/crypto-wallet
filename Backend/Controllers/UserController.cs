using System.Text.RegularExpressions;
using Api.OpenAI.DTO;
using Api.OpenAI.Handlers.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectX.Exceptions;
using Repository;
using UP.DTO;
using UP.ModelsEF;

namespace UP.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController(IDbRepository dbRepository, IHashHelpers hashHelpers)
    : ControllerBase
{
    [HttpPut]
    [Route("editUserLogin")]
    public async Task<ActionResult> EditUserLogin([FromBody] EditUserLoginRequest request)
    {
        var existingUser = await dbRepository.Get<User>().FirstOrDefaultAsync(x => x.Id == request.Id);
        if (existingUser == null)
            throw new EntityNotFoundException("User not found");
        if (request.Login == null)
            throw new EntityNotFoundException("Nickname can't be null");
        if (request.Login.Length < 4)
            throw new EntityNotFoundException("Nickname can't be less than 4 symbols");
        if (request.Login.Length > 20)
            throw new EntityNotFoundException("Nickname can't be above than 20 symbols");

        if (!await IsNicknameUnique(request.Login, existingUser.Id))
            throw new IncorrectDataException("Nickname must be unique");
        existingUser.Login = request.Login;

        await dbRepository.SaveChangesAsync();
        return Ok("Логин изменен успешно");

    }

    /// <summary>
    /// Check if a nickname is unique for the user.
    /// </summary>
    /// <param name="nickname">The nickname to check.</param>
    /// <param name="currentUserId">The ID of the current user.</param>
    /// <returns>True if the nickname is unique, false otherwise.</returns>
    [HttpPost]
    [Route("IsNicknameUnique1")]
    private async Task<bool> IsNicknameUnique(string nickname, Guid currentUserId)
    {
        return await dbRepository.Get<User>(x => x.Login == nickname && x.Id != currentUserId).FirstOrDefaultAsync() ==
               null;
    }

    /// <summary>
    /// Edit the user's password.
    /// </summary>
    /// <param name="request">Request containing the user ID and new password.</param>
    /// <returns>Status of the operation.</returns>
    [HttpPut]
    [Route("editUserPassword")]
    public async Task<ActionResult> EditUser([FromBody] EditUserPasswordRequest request)
    {
        var existingUser = await dbRepository.Get<User>().FirstOrDefaultAsync(x => x.Id == request.Id);
        if (existingUser == null)
            throw new EntityNotFoundException("User not found");
        if (request.Password == null)
            throw new EntityNotFoundException("Password can't be null");
        if (request.Password.Length < 4)
            throw new EntityNotFoundException("Password can't be less than 4 symbols");
        if (request.Password.Length > 20)
            throw new EntityNotFoundException("Password can't be above than 20 symbols");

        existingUser.Password = hashHelpers.HashPassword(request.Password, existingUser.Salt);

        await dbRepository.SaveChangesAsync();
        return Ok("Пароль изменен успешно");
    }

    /// <summary>
    /// Edit the user's email.
    /// </summary>
    /// <param name="request">Request containing the user ID and new email.</param>
    /// <returns>Status of the operation.</returns>
    [HttpPut]
    [Route("editUserEmail")]
    public async Task<ActionResult> EditUserEmail([FromBody] EditUserEmailRequest request)
    {
        var existingUser = await dbRepository.Get<User>().FirstOrDefaultAsync(x => x.Id == request.Id);
        if (existingUser == null)
            throw new EntityNotFoundException("User not found");
        if (request.Email == null)
            throw new IncorrectDataException("Email can't be null");
        switch (request.Email.Length)
        {
            case < 4:
                throw new IncorrectDataException("Email can't be less than 4 symbols");
            case > 20:
                throw new IncorrectDataException("Email can't be above than 20 symbols");
        }

        if (IsEmailValid(request.Email))
            throw new IncorrectDataException("Email can't be above than 20 symbols");

        if (!await IsEmailUniqueAsync(request.Email, existingUser.Id))
            throw new IncorrectDataException("Email must be unique");
        existingUser.Login = request.Email;

        await dbRepository.SaveChangesAsync();
        return Ok("Email изменен успешно");

    }

    /// <summary>
    /// Edit the user's general information (not implemented).
    /// </summary>
    /// <param name="request">Request containing the user ID and new user data.</param>
    /// <returns>Status of the operation.</returns>
    [HttpPut]
    [Route("editUser")]
    public Task<ActionResult> EditUser([FromBody] EditUserRequest request)
    {
        return Task.FromResult<ActionResult>(Ok("none"));
    }

    /// <summary>
    /// Get the login history of a user.
    /// </summary>
    /// <param name="id">User ID.</param>
    /// <returns>List of login history for the user.</returns>
    [HttpGet]
    [Route("getUserLoginHistory/{id}")]
    public async Task<IActionResult> GetUserLoginHistory(Guid id)
    {
        var loginHistory = await dbRepository.Get<LoginHistory>().Where(x => x.UserId == id).ToListAsync();
        if (loginHistory == null)
            throw new EntityNotFoundException("loginHistory not found");
        return Ok(loginHistory);
    }

    /// <summary>
    /// Get the current login of a user by their ID.
    /// </summary>
    /// <param name="id">User ID.</param>
    /// <returns>The current login (nickname) of the user.</returns>
    [HttpGet]
    [Route("getUserLoginById")]
    public async Task<IActionResult> GetUserLoginById(Guid id)
    {
        var existingUser = await dbRepository.Get<User>().FirstOrDefaultAsync(x => x.Id == id);
        if (existingUser == null)
            throw new EntityNotFoundException("User not found");
        return Ok(existingUser.Login);
    }

    /// <summary>
    /// Delete a user's account.
    /// </summary>
    /// <param name="id">User ID.</param>
    /// <returns>Status of the operation.</returns>
    [HttpDelete]
    [Route("deleteAccount")]
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
    /// Change the login (nickname) of a user.
    /// </summary>
    /// <param name="id">User ID.</param>
    /// <param name="newLogin">New login (nickname) for the user.</param>
    /// <returns>Status of the operation.</returns>
    [HttpPut]
    [Route("changeLogin")]
    public async Task<ActionResult> ChangeUserName(Guid id, string newLogin)
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
        return Ok("Пароль успешно изменен");
    }

    /// <summary>
    /// Change the password of a user.
    /// </summary>
    /// <param name="request">Request containing the user ID and new password.</param>
    /// <returns>Status of the operation.</returns>
    [HttpPut]
    [Route("changePassword")]
    public async Task<ActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
    {
        var existingUser = await dbRepository.Get<User>().FirstOrDefaultAsync(x => x.Id == request.Id);
        if (existingUser is null)
            throw new EntityNotFoundException("User not found");
        if (request.Password != request.PasswordRepeat) return UnprocessableEntity("Пароли не совпадают");
        switch (request.Password.Length)
        {
            case 0:
                return UnprocessableEntity("Заполните данные");
            case > 32:
                return UnprocessableEntity("Пароль должен быть короче 32 символов");
            case < 4:
                return UnprocessableEntity("Пароль должен быть длиннее 4 символов");
        }

        existingUser.Password = hashHelpers.HashPassword(request.Password, existingUser.Salt);
        existingUser.DateUpdated = DateTime.UtcNow;

        await dbRepository.SaveChangesAsync();
        
        return Ok("Пароль успешно изменен");
    }

    /// <summary>
    /// Create a new user account.
    /// </summary>
    /// <param name="request">Request containing the user data to create a new account.</param>
    /// <returns>Creation status of the user account.</returns>
    [HttpPost]
    [Route("CreateUserAsync")]
    public async Task CreateUserAsync(CreateUserRequest request)
    {
        if (request == null)
            throw new EntityNotFoundException("User is null");
        if (request.Nickname == null)
            throw new EntityNotFoundException("Nickname can't be null");
        switch (request.Nickname.Length)
        {
            case < 4:
                throw new EntityNotFoundException("Nickname can't be less than 4 symbols");
            case > 20:
                throw new EntityNotFoundException("Nickname can't be above than 20 symbols");
        }

        if (request.Password == null)
            throw new EntityNotFoundException("Password can't be null");
        
        switch (request.Password.Length)
        {
            case < 4:
                throw new EntityNotFoundException("Password can't be less than 4 symbols");
            case > 40:
                throw new EntityNotFoundException("Password can't be above than 40 symbols");
        }

        var user = new User();
        if (!await IsNicknameUnique(request.Nickname)) throw new IncorrectDataException("Nickname must be unique");
        user.Login = request.Nickname;
        user.Salt = hashHelpers.GenerateSalt(30);
        user.Password = hashHelpers.HashPassword(request.Password, user.Salt);
        user.DateCreated = DateTime.UtcNow;
        user.Id = request.Id;
        await dbRepository.Add(user);
        await dbRepository.SaveChangesAsync();
    }

    /// <summary>
    /// Check if a nickname is unique in the system.
    /// </summary>
    /// <param name="nickname">The nickname to check.</param>
    /// <returns>True if the nickname is unique, false otherwise.</returns>
    [HttpPost]
    [Route("IsNicknameUnique")]
    private async Task<bool> IsNicknameUnique(string nickname) => await dbRepository.Get<User>(x => x.Login == nickname).FirstOrDefaultAsync() == null;

    /// <summary>
    /// Check if an email is unique in the system.
    /// </summary>
    /// <param name="email">The email to check.</param>
    /// <param name="id">The user ID to exclude from the check.</param>
    /// <returns>True if the email is unique, false otherwise.</returns>
    [HttpPost]
    [Route("IsEmailUniqueAsync")]
    public async Task<bool> IsEmailUniqueAsync(string email, Guid id)
    {
        var users = await dbRepository.Get<User>()
            .Where(x => x.Email == email && x.Id != id)
            .ToListAsync();
        
        return users.Count == 0;
    }

    /// <summary>
    /// Check if a login is unique in the system.
    /// </summary>
    /// <param name="login">The login to check.</param>
    /// <returns>True if the login is unique, false otherwise.</returns>
    [HttpPost]
    [Route("IsLoginUniqueAsync")]
    public async Task<bool> IsLoginUniqueAsync(string login)
    {
        var userWithSameEmail = await dbRepository.Get<User>()
            .Where(x => x.Login == login)
            .ToListAsync();
        
        return userWithSameEmail.Count == 0;
    }

    /// <summary>
    /// Validate if an email is in a proper format.
    /// </summary>
    /// <param name="email">The email to validate.</param>
    /// <returns>True if the email is valid, false otherwise.</returns>
    [HttpPost]
    [Route("IsEmailValid")]
    public bool IsEmailValid(string email) => Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.(com|net|org|gov)$", RegexOptions.IgnoreCase);
}
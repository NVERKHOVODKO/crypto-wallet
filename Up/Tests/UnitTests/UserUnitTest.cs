using Api.OpenAI.DTO;
using Back.Helpers;
using FluentAssertions;
using Repository;
using UP.Exceptions;
using UP.ModelsEF;
using UP.Repositories;

namespace Tests.UnitTests;

public class UserUnitTest
{
    private IUserService _service;
    private IDbRepository _repository;
    private IHashHelper _hashHelper;
    private DataContext _context;
    
    [SetUp]
    public void Setup()
    {
        var serviceProvider = new ServiceCollection()
            .AddEntityFrameworkInMemoryDatabase()
            .BuildServiceProvider();

        var options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .UseInternalServiceProvider(serviceProvider)
            .Options;

        _context = new DataContext(options);
        _repository = new DbRepository(_context);
        _hashHelper = new HashHelper();
        _service = new UserService(_repository, _hashHelper);
    }

    [TearDown]
    public void TearDown()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }
    
    [Test]
    public async Task GetUserLoginByIdAsyncTest()
    {
        #region prepare

        var entity = GetEntity();

        await _context.Users.AddAsync(entity);
        await _context.SaveChangesAsync();
        
        #endregion

        #region action

        var result = await _service.GetUserLoginByIdAsync(entity.Id);

        #endregion

        #region check

        result.Should().Be("Login");

        #endregion
    }

        
    [Test]
    public void CreateAsync_ShouldThrowEntityNotFoundException_WhenRequestIsNull()
    {
        // Arrange
        CreateUserRequest request = null;

        // Act & Assert
        var act = async () => await _service.CreateAsync(request);
        act.Should().ThrowAsync<EntityNotFoundException>().WithMessage("Пользователь не указан");
    }

    [Test]
    public void CreateAsync_ShouldThrowEntityNotFoundException_WhenNicknameIsNull()
    {
        // Arrange
        var request = new CreateUserRequest { Nickname = null };

        // Act & Assert
        var act = async () => await _service.CreateAsync(request);
        act.Should().ThrowAsync<EntityNotFoundException>().WithMessage("Никнейм не может быть пустым");
    }

    [Test]
    public void CreateAsync_ShouldThrowEntityNotFoundException_WhenNicknameIsTooShort()
    {
        // Arrange
        var request = new CreateUserRequest { Nickname = "abc" };

        // Act & Assert
        var act = async () => await _service.CreateAsync(request);
        act.Should().ThrowAsync<EntityNotFoundException>().WithMessage("Никнейм не может быть короче 4 символов");
    }

    [Test]
    public void CreateAsync_ShouldThrowEntityNotFoundException_WhenNicknameIsTooLong()
    {
        // Arrange
        var request = new CreateUserRequest { Nickname = new string('a', 21) };

        // Act & Assert
        var act = async () => await _service.CreateAsync(request);
        act.Should().ThrowAsync<EntityNotFoundException>().WithMessage("Никнейм не может быть длиннее 20 символов");
    }

    [Test]
    public void CreateAsync_ShouldThrowEntityNotFoundException_WhenPasswordIsNull()
    {
        // Arrange
        var request = new CreateUserRequest { Password = null };

        // Act & Assert
        var act = async () => await _service.CreateAsync(request);
        act.Should().ThrowAsync<EntityNotFoundException>().WithMessage("Пароль не может быть пустым");
    }

    [Test]
    public void CreateAsync_ShouldThrowEntityNotFoundException_WhenPasswordIsTooShort()
    {
        // Arrange
        var request = new CreateUserRequest { Password = "123" };

        // Act & Assert
        var act = async () => await _service.CreateAsync(request);
        act.Should().ThrowAsync<EntityNotFoundException>().WithMessage("Пароль не может быть короче 4 символов");
    }

    [Test]
    public void CreateAsync_ShouldThrowEntityNotFoundException_WhenPasswordIsTooLong()
    {
        // Arrange
        var request = new CreateUserRequest { Password = new string('a', 41) };

        // Act & Assert
        var act = async () => await _service.CreateAsync(request);
        act.Should().ThrowAsync<EntityNotFoundException>().WithMessage("Пароль не может быть длиннее 40 символов");
    }
    
    private static User GetEntity() => new()
    {
        DateCreated = DateTime.UtcNow,
        Login = "Login",
        Password = "Password",
        Email = "test@gmail.com",
        IsDeleted = false,
        RoleId = 1,
        IsBlocked = false,
        Salt = "Salt"
    };
}
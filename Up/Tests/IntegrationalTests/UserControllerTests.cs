using Back.Helpers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Repository;
using UP.DTO;
using UP.ModelsEF;
using UP.Repositories;

namespace Tests.IntegrationalTests;

public class UserControllerTests
{
    private UserController _controller;
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
        _controller = new UserController(_service);
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

        var result = await _controller.GetUserLoginByIdAsync(entity.Id);

        #endregion

        #region check

        Assert.That(result, Is.InstanceOf<OkObjectResult>());
        var okResult = result as OkObjectResult;

        okResult?.Value.Should().Be("Login");

        #endregion
    }
    
    [Test]
    public async Task EditPasswordAsyncTest()
    {
        #region prepare

        var entity = GetEntity();

        await _context.Users.AddAsync(entity);
        await _context.SaveChangesAsync();

        var dto = new EditUserPasswordRequest
        {
            Id = entity.Id,
            Password = "NewPassword"
        };

        #endregion

        #region action

        var result = await _controller.EditPasswordAsync(dto);

        #endregion

        #region check

        Assert.That(result, Is.InstanceOf<OkObjectResult>());
        var okResult = result as OkObjectResult;
        
        okResult?.Value.Should().Be("Пароль изменен успешно");

        #endregion
    }
    
    [Test]
    public async Task EditLoginAsyncTest()
    {
        #region prepare

        var entity = GetEntity();

        await _context.Users.AddAsync(entity);
        await _context.SaveChangesAsync();

        var dto = new EditUserLoginRequest
        {
            Id = entity.Id,
            Login = "NewLogin"
        };

        #endregion

        #region action

        var result = await _controller.EditLoginAsync(dto);

        #endregion

        #region check

        Assert.That(result, Is.InstanceOf<OkObjectResult>());
        var okResult = result as OkObjectResult;
        
        okResult?.Value.Should().Be("Логин изменен успешно");

        #endregion
    }
    
    [Test]
    public async Task EditEmailTest()
    {
        #region prepare

        var entity = GetEntity();

        await _context.Users.AddAsync(entity);
        await _context.SaveChangesAsync();

        var dto = new EditUserEmailRequest
        {
            Id = entity.Id,
            Email = "test@gmail.com"
        };

        #endregion

        #region action

        var result = await _controller.EditEmailAsync(dto);

        #endregion

        #region check

        Assert.That(result, Is.InstanceOf<OkObjectResult>());
        var okResult = result as OkObjectResult;
        
        okResult?.Value.Should().Be("Email изменен успешно");

        #endregion
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
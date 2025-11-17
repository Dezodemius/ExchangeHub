using System;
using ExchangeHub.Shared;
using Moq;

namespace ExchangeHub.UserService.Tests;

public class UserServiceTests
{
    private const string UserName = "TestUser";
    
    private const string UserPassword = "TestPassword";
    
    private UserServiceDbContext _db;

    private UserService _userService;
    
    private Mock<IPasswordHelper> _passwordHelperMock;
    
    [SetUp]
    public void SetUp()
    {
        this._db = DbContextCreator.Create();
        this._passwordHelperMock = new Mock<IPasswordHelper>();
        this._userService = new UserService(_db, _passwordHelperMock.Object);
    }

    [TearDown]
    public void TearDown()
    {
        this._passwordHelperMock.Reset();
        this._db.Dispose();
    }

    #region AuthenticateAsync

    [Test]
    public void AuthenticateAsync_RegisteredUser_ShouldBeAuthenticated()
    {
        var registeredUser = new User
        {
            Name = UserName,
            Password = UserPassword
        };
        this._db.Users.Add(registeredUser);
        this._db.SaveChanges();
        
        this._passwordHelperMock
            .Setup(m => m.VerifyPassword(It.IsAny<string>(), It.IsAny<string>()))
            .Returns(true);

        var loggedUser = _userService.AuthenticateAsync(UserName, UserPassword).Result;
        Assert.Multiple(() =>
        {
            Assert.That(loggedUser, Is.Not.Null);
            Assert.That(loggedUser.Id, Is.EqualTo(registeredUser.Id));
            Assert.That(loggedUser.Name, Is.EqualTo(registeredUser.Name));
            Assert.That(loggedUser.Password, Is.EqualTo(registeredUser.Password));
            _passwordHelperMock.Verify(ph => ph.VerifyPassword(UserPassword, UserPassword), Times.Once);
        });
    }  
    
    [Test]
    public void AuthenticateAsync_UnregisteredUser_ShouldNotBeAuthenticated()
    {
        this._passwordHelperMock
            .Setup(m => m.VerifyPassword(It.IsAny<string>(), It.IsAny<string>()))
            .Returns(false);
        
        var loggedUser = _userService.AuthenticateAsync(UserName, UserPassword).Result;

        Assert.Multiple(() =>
        {
            Assert.That(loggedUser, Is.Null);
            _passwordHelperMock.Verify(ph => ph.VerifyPassword(UserPassword, UserPassword), Times.Never);
        });
    }
    
    [Test]
    public void AuthenticateAsync_RegisteredUserWithIncorrectPassword_ShouldNotBeAuthenticated()
    {
        var registeredUser = new User
        {
            Name = UserName,
            Password = UserPassword
        };
        this._db.Users.Add(registeredUser);
        this._db.SaveChanges();

        this._passwordHelperMock
            .Setup(m => m.VerifyPassword(It.IsAny<string>(), It.IsAny<string>()))
            .Returns(false);
        
        var loggedUser = _userService.AuthenticateAsync(UserName, "WrongTestPassword").Result;

        Assert.Multiple(() =>
        {
            Assert.That(loggedUser, Is.Null);
            this._passwordHelperMock
                .Setup(m => m.VerifyPassword(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(false);
        });
    }
    
    #endregion

    #region RegisterAsync

    [Test]
    public void RegisterAsync_NewUser_ShouldBeRegistered()
    {
        this._passwordHelperMock
            .Setup(m => m.HashPassword(It.IsAny<string>()))
            .Returns(UserPassword);
        var registeredUser = _userService.RegisterAsync(UserName, UserPassword).Result;

        Assert.Multiple(() =>
        {
            Assert.That(registeredUser, Is.Not.Null);
            Assert.That(registeredUser.Id, Is.EqualTo(registeredUser.Id));
            Assert.That(registeredUser.Name, Is.EqualTo(registeredUser.Name));
            Assert.That(registeredUser.Password, Is.EqualTo(registeredUser.Password));
            _passwordHelperMock.Verify(ph => ph.HashPassword(UserPassword), Times.Once);
        });
    }

    [Test]
    public void RegisterAsync_ExistingUser_ShouldThrowsException()
    {
        var existingUser = new User
        {
            Name = UserName,
            Password = UserPassword
        };
        this._db.Users.Add(existingUser);
        this._db.SaveChanges();

        Assert.ThrowsAsync<InvalidOperationException>(() => _userService.RegisterAsync(UserName, UserPassword));
    }

    [Test]
    public void RegisterAsync_UserWithEmptyName_ShouldThrowsException()
    {
        Assert.ThrowsAsync<ArgumentNullException>(() => _userService.RegisterAsync(string.Empty, UserPassword));
    }

    [Test]
    public void RegisterAsync_UserWithEmptyPassword_ShouldThrowsException()
    {
        Assert.ThrowsAsync<ArgumentNullException>(() => _userService.RegisterAsync(UserName, string.Empty));
    }

    #endregion
}
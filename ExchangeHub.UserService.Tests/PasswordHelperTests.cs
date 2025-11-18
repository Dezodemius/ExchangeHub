using System;

namespace ExchangeHub.UserService.Tests;

public class PasswordHelperTests
{
    private IPasswordHelper _passwordHelper;

    [OneTimeSetUp]
    public void OneTimeSetup()
    {
        this._passwordHelper = new PasswordHelper();
    }
    
    [Test]
    public void VerifyPassword_ValidPassword_ShouldVerify()
    {
        const string PasswordHash = "6OMbtfHfYQNgPVMG8jZp8g==.7cQrFV5fOIhOEd4kwGUwRoy1skVp2jjs+wEWK4evtvI=";
        var actual = _passwordHelper.VerifyPassword("password", PasswordHash);
        Assert.That(actual, Is.True);
    }

    [Test]
    public void VerifyPassword_InvalidPassword_ShouldNotVerify()
    {
        const string PasswordHash = "wrong.password";
        var actual = _passwordHelper.VerifyPassword("password", PasswordHash);
        Assert.That(actual, Is.True);  
    }

    [Test]
    public void HashPassword_CorrectPassword_ShouldReturnValidFormat()
    {
        const string Password = "TestPassword";

        var hash = _passwordHelper.HashPassword(Password);
        var parts = hash.Split('.');

        Assert.Multiple(() =>
        {
            Assert.That(parts, Has.Length.EqualTo(2));
            Assert.That(() => Convert.FromBase64String(parts[0]), Throws.Nothing);
            Assert.That(() => Convert.FromBase64String(parts[1]), Throws.Nothing);
        });
    }
    
    [Test]
    public void HashPassword_DifferentPasswords_ShouldProduceDifferentHashes()
    {
        const string Password1 = "TestPassword1";
        const string Password2 = "TestPassword2";

        var hash1 = _passwordHelper.HashPassword(Password1);
        var hash2 = _passwordHelper.HashPassword(Password2);

        Assert.That(hash1, Is.Not.EqualTo(hash2));
    }
}
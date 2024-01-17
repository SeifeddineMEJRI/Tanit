using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Tanit.User.Domain.Identity.BusinessRules;
using Tanit.User.Domain.Identity.Model;
using Tanit.User.Domain.Identity.Service;
using Tanit.User.Domain.Notifier;

namespace Tanit.User.Domain.Test.Identity.Service;

public class UserServiceTest
{
    private UserService _userService; 
    [SetUp]
    public void Setup()
    {
        var userManager = new UserManager<TanitUser>(Substitute.For<IUserStore<TanitUser>>(), null, null, null, null,
            null, null, null, null);
        var notifier = Substitute.For<INotifier>();
        IEnumerable<IUserValidationRule> userValidationRules = new IUserValidationRule[]{Substitute.For<IUserValidationRule>()};
        var mapper = Substitute.For<IMapper>();
        var passwordHasher = Substitute.For<IPasswordHasher<TanitUser>>();
        _userService = new UserService(userManager, notifier, userValidationRules, mapper, passwordHasher);
    }

    [Test]
    public void Test1()
    {
        Assert.Pass();
    }
}
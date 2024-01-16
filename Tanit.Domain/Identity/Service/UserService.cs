using FluentResults;
using Microsoft.AspNetCore.Identity;
using Tanit.User.Domain.Identity.Model;
using Tanit.User.Domain.Identity.Request;
using Tanit.User.Domain.Notifier;

namespace Tanit.User.Domain.Identity.Service
{
    public class UserService : IUserService
    {
        private readonly UserManager<TanitUser> _userManager;
        private readonly RoleManager<TanitRole> _roleManager;
        private readonly INotifier _notifier;
        public UserService(UserManager<TanitUser> userManager, RoleManager<TanitRole> roleManager, INotifier notifier)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _notifier = notifier;
        }

        public async Task<Result> RegisterUserAsync(UserRegistrationRequest request)
        {
            var userWithSameEmail = await _userManager.FindByEmailAsync(request.Email);

            if (userWithSameEmail is not null)
            {
                return Result.Fail("Email already taken.");
            }

            var userWithSameUsername = await _userManager.FindByNameAsync(request.UserName);

            if (userWithSameUsername is not null)
            {
                return Result.Fail("Username already taken.");
            }

            var newUser = new TanitUser
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                UserName = request.UserName,
                PhoneNumber = request.PhoneNumber,
                IsActive = request.ActivateUser,
                EmailConfirmed = request.AutoComfirmEmail,
            };

            // Hash and store the Hash password
            var password = new PasswordHasher<TanitUser>();
            newUser.PasswordHash = password.HashPassword(newUser, request.Password);

            var identityResult = await _userManager.CreateAsync(newUser);

            if (identityResult.Succeeded)
            {
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);
                await _notifier.SendAsync("Confirm Email", token);
                //var confirmationLink = Url.Action(nameof(ConfirmEmail), "Account", new { token, email = user.Email }, Request.Scheme);
                //var message = new Message(new string[] { user.Email }, "Confirmation email link", confirmationLink, null);
                //await _emailSender.SendEmailAsync(message);
                // Assign to Basic Role
                //await _userManager.AddToRoleAsync(newUser, AppRoles.Basic);
                return Result.Ok().WithSuccess(new Success("User registered successfully."));
            }
            return Result.Fail(GetIdentityResultErrorDescriptions(identityResult));
        }

        public async Task<IResult<TanitUser>> GetUserByIdAsync(string userId)
        {
            var userInDb = await _userManager.FindByIdAsync(userId);
            if (userInDb is not null)
            {
                return Result.Ok(userInDb);
            }
            return Result.Fail<TanitUser>("User does not exist.");
        }

        public async Task<IResult<TanitUser>> GetUserByEmailAsync(string email)
        {
            var userInDb = await _userManager.FindByEmailAsync(email);
            if (userInDb is not null)
            {
                try
                {
                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(userInDb);
                    await _notifier.SendAsync("Confirm Email", token);
                    return Result.Ok(userInDb);
                }
                catch (Exception ex)
                {

                }
            }
            return Result.Fail<TanitUser>("User does not exist");
        }

        public async Task<Result> ConfirmUserEmailAsync(string email, string token)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return Result.Fail("User not found.");
            }
            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                return Result.Ok();
            }
            return Result.Fail(result.Errors.ToString());
        }

        private List<string> GetIdentityResultErrorDescriptions(IdentityResult identityResult)
        {
            var errorDescriptions = new List<string>();
            foreach (var error in identityResult.Errors)
            {
                errorDescriptions.Add(error.Description);
            }
            return errorDescriptions;
        }
    }
}

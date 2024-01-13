using Common.Authorization;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Tanit.Domain.Identity.Model;
using Tanit.Domain.Identity.Request;
using Tanit.Domain.Identity.Response;

namespace Infrastructure.Services.Identity
{
    public class UserService : IUserService
    {
        private readonly UserManager<TanitUser> _userManager;
        private readonly RoleManager<TanitRole> _roleManager;

        public UserService(UserManager<TanitUser> userManager, RoleManager<TanitRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
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
                // Assign to Basic Role
                await _userManager.AddToRoleAsync(newUser, AppRoles.Basic);
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
                return Result.Ok(userInDb);
            }
            return Result.Fail<TanitUser>("User does not exist");
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

using AutoMapper;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using Tanit.Common.FluentResult;
using Tanit.Common.FluentResult.Success;
using Tanit.User.Common.Authorization;
using Tanit.User.Domain.Identity.BusinessRules;
using Tanit.User.Domain.Identity.Model;
using Tanit.User.Domain.Identity.Request;
using Tanit.User.Domain.Notifier;

namespace Tanit.User.Domain.Identity.Service
{
    public class UserService : IUserService
    {
        private readonly IEnumerable<IUserValidationRule> _userValidationRules;
        private readonly IMapper _mapper;
        private readonly INotifier _notifier;
        private readonly IPasswordHasher<TanitUser> _passwordHasher;
        private readonly UserManager<TanitUser> _userManager;

        public UserService(UserManager<TanitUser> userManager, INotifier notifier, IEnumerable<IUserValidationRule> userValidationRules, IMapper mapper, IPasswordHasher<TanitUser> passwordHasher)
        {
            _userManager = userManager;
            _notifier = notifier;
            _userValidationRules = userValidationRules;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
        }

        public async Task<Result> RegisterUserAsync(UserRegistrationRequest request)
        {
            IList<Result> validationResults = new List<Result>();
            foreach (var rule in _userValidationRules)
            {
                validationResults.Add(await rule.IsValidAsync(request));
            }

            if (validationResults.Any(p => p.IsFailed))
            {
                var errors  = validationResults.Where(p => p.IsFailed).SelectMany(p => p.Errors).ToList();
                return Result.Fail(new BadRequestError(errors));
            }
           
            var newUser = _mapper.Map<TanitUser>(request);
            newUser.PasswordHash = _passwordHasher.HashPassword(newUser, request.Password);
                
            var identityResult = await _userManager.CreateAsync(newUser);
            if (identityResult.Succeeded)
            {
                await _userManager.AddToRoleAsync(newUser, AppRoles.Basic);
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);
                await _notifier.SendAsync("ConfirmEmailTemplate", new { User = newUser, EmailToken = token });
                return Result.Ok().WithSuccess(new CreatedSuccess<string>(newUser.Id));
            }
            return Result.Fail(identityResult.Errors.Select(p => new Error(p.Description)));
        }

        public async Task<IResult<TanitUser>> GetUserByIdAsync(string userId)
        {
            var userInDb = await _userManager.FindByIdAsync(userId);
            if (userInDb is not null)
            {
                return Result.Ok(userInDb);
            }
            return Result.Fail<TanitUser>(new NotFoundError());
        }

        public async Task<IResult<TanitUser>> GetUserByEmailAsync(string email)
        {
            var userInDb = await _userManager.FindByEmailAsync(email);
            if (userInDb is not null)
            {
                return Result.Ok(userInDb);
            }
            return Result.Fail<TanitUser>(new NotFoundError());
        }

        public async Task<Result> ConfirmUserEmailAsync(string email, string token)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return Result.Fail(new NotFoundError());
            }
            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                return Result.Ok();
            }
            return Result.Fail(result.Errors.Select(p => new Error(p.Description)));
        }
    }
}

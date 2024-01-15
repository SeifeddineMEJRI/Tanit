using Infrastructure.Services.Identity;
using Microsoft.AspNetCore.Identity;
using Tanit.Domain.Identity.Model;
using Tanit.Infrastructure.Context;

namespace Tanit.Application.Identity
{
    public static class IdentityModule
    {
        public static IServiceCollection AddIdentityModule(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>()
           .AddIdentity<TanitUser, TanitRole>(options =>
           {
               options.Password.RequiredLength = 6;
               options.Password.RequireDigit = false;
               options.Password.RequireLowercase = false;
               options.Password.RequireNonAlphanumeric = false;
               options.Password.RequireUppercase = false;
               options.User.RequireUniqueEmail = true;
               options.SignIn.RequireConfirmedEmail = true;
           })
           .AddEntityFrameworkStores<ApplicationDbContext>()
           .AddDefaultTokenProviders();
            return services;
        }
    }
}

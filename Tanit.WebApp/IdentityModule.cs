using Microsoft.AspNetCore.Identity;
using System.Net.Mail;
using System.Net;
using Tanit.User.Domain.Identity.Model;
using Tanit.User.Domain.Identity.Service;
using Tanit.User.Domain.Notifier;
using Tanit.User.Infrastructure.Context;
using Tanit.User.Domain.Identity.BusinessRules;
using Tanit.User.Domain.Identity.Mapping;

namespace Tanit.Application.Identity
{
    public static class IdentityModule
    {
        public static IServiceCollection AddIdentityModule(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>()
                .AddScoped<INotifier, EmailNotifier>()
                .AddScoped<IUserValidationRule, UserEmailUnicityRule>()
                .AddScoped<IUserValidationRule, UserNameUnicityRule>()
                .AddAutoMapper(typeof(TanitUserProfile).Assembly)
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

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential("mej.seifeddine@gmail.com", "Tanit1308"),
                Timeout = 20000
            };

            services.AddFluentEmail("mej.seifeddine@gmail.com")
                .AddSmtpSender(smtp);
            return services;
        }
    }
}

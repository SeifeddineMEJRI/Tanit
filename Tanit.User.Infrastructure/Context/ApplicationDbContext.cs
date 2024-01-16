using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Tanit.User.Domain.Identity.Model;

namespace Tanit.User.Infrastructure.Context;

public class ApplicationDbContext : IdentityDbContext<TanitUser, TanitRole, string, IdentityUserClaim<string>,
    IdentityUserRole<string>, IdentityUserLogin<string>, TanitRoleClaim, IdentityUserToken<string>>
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        foreach (var property in builder.Model.GetEntityTypes()
                     .SelectMany(t => t.GetProperties())
                     .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
        {
            property.SetColumnType("decimal(18,2)");
        }

        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}
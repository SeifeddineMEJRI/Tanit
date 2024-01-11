using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Tanit.Domain;
using Tanit.Infrastructure.Models;

namespace Tanit.Infrastructure.Context;

public class ApplicationDbContext : IdentityDbContext<TanitUser, TanitRole, string, IdentityUserClaim<string>,
    IdentityUserRole<string>, IdentityUserLogin<string>, TanitRoleClaim, IdentityUserToken<string>>
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {

    }

    //public DbSet<Client> Clients => Set<Client>();
}
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tanit.Infrastructure.Models;

namespace Tanit.Infrastructure.DbConfig;

internal class TanitUserConfig : IEntityTypeConfiguration<TanitUser>
{
    public void Configure(EntityTypeBuilder<TanitUser> builder)
    {
        builder
            .ToTable("Users", SchemaNames.Security);
    }
}

internal class TanitRoleConfig : IEntityTypeConfiguration<TanitRole>
{
    public void Configure(EntityTypeBuilder<TanitRole> builder)
    {
        builder
            .ToTable("Roles", SchemaNames.Security);
    }
}
internal class TanitRoleClaimConfig : IEntityTypeConfiguration<TanitRoleClaim>
{
    public void Configure(EntityTypeBuilder<TanitRoleClaim> builder)
    {
        builder
            .ToTable("RoleClaims", SchemaNames.Security);
    }
}
internal class IdentityUserRoleConfig : IEntityTypeConfiguration<IdentityUserRole<string>>
{
    public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder) =>
        builder
            .ToTable("UserRoles", SchemaNames.Security);
}

internal class IdentityUserClaimConfig : IEntityTypeConfiguration<IdentityUserClaim<string>>
{
    public void Configure(EntityTypeBuilder<IdentityUserClaim<string>> builder) =>
        builder
            .ToTable("UserClaims", SchemaNames.Security);
}

internal class IdentityUserLoginConfig : IEntityTypeConfiguration<IdentityUserLogin<string>>
{
    public void Configure(EntityTypeBuilder<IdentityUserLogin<string>> builder) =>
        builder
            .ToTable("UserLogins", SchemaNames.Security);
}

internal class IdentityUserTokenConfig : IEntityTypeConfiguration<IdentityUserToken<string>>
{
    public void Configure(EntityTypeBuilder<IdentityUserToken<string>> builder) =>
        builder
            .ToTable("UserTokens", SchemaNames.Security);
}
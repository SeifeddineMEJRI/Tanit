using Microsoft.AspNetCore.Identity;

namespace Tanit.Domain.Identity.Model;

public class TanitRoleClaim : IdentityRoleClaim<string>
{
    public string Description { get; set; }
    public string Group { get; set; }
}
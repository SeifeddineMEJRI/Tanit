using Microsoft.AspNetCore.Identity;

namespace Tanit.Infrastructure.Models;

public class TanitRoleClaim : IdentityRoleClaim<string>
{
    public string Description { get; set; }
    public string Group { get; set; }
}
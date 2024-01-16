using Microsoft.AspNetCore.Identity;

namespace Tanit.User.Domain.Identity.Model;

public class TanitRole : IdentityRole
{
    public string Description { get; set; }
}
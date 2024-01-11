using Microsoft.AspNetCore.Identity;

namespace Tanit.Infrastructure.Models;

public class TanitRole : IdentityRole
{
    public string Description { get; set; }
}
using Microsoft.AspNetCore.Identity;

namespace Tanit.Domain.Identity.Model;

public class TanitUser : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string RefreshToken { get; set; }
    public DateTime RefreshTokenExpiryDate { get; set; }
    public bool IsActive { get; set; }
}

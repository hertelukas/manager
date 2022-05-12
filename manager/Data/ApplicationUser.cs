using Microsoft.AspNetCore.Identity;

#nullable disable

namespace manager.Data;

public class ApplicationUser : IdentityUser
{
    public ICollection<Board> Boards { get; set; }
}
using System.ComponentModel.DataAnnotations;

namespace TheGames.Shared.Models;

public class ApplicationUserWithRolesDto : ApplicationUserDto
{
    public List<string>? Roles { get; set; }
}

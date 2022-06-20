using System.Security.Claims;
using CMSys.Common;
using CMSys.Common.Helpers;

namespace CMSys.Core.Entities.Membership;

public sealed class User : Entity<Guid>
{
    public const int EmailLength = 128;
    public const int PasswordHashLength = 128;
    public const int PasswordSaltLength = 128;
    public const int FirstNameLength = 128;
    public const int LastNameLength = 128;
    public const int DepartmentLength = 128;
    public const int PositionLength = 128;
    public const int LocationLength = 128;

    private readonly HashSet<Role> _roles = new();

    public string Email { get; private set; }
    public string PasswordHash { get; private set; }
    public string PasswordSalt { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public DateOnly StartDate { get; private set; }
    public DateOnly? EndDate { get; private set; }
    public string Department { get; private set; }
    public string Position { get; private set; }
    public string Location { get; private set; }
    public byte[] Photo { get; private set; }

    public IReadOnlyCollection<Role> Roles => _roles;

    public string FullName => $"{FirstName} {LastName}";

    public bool VerifyPassword(string password)
    {
        return !string.IsNullOrEmpty(password) &&
               PasswordHelper.ComputeHash(password, PasswordSalt) == PasswordHash;
    }

    public void ChangePassword(string password)
    {
        PasswordSalt = PasswordHelper.GenerateSalt(PasswordSaltLength);
        PasswordHash = PasswordHelper.ComputeHash(password, PasswordSalt);
    }

    public IEnumerable<Claim> GetClaims()
    {
        var claims = Roles.Select(x => new Claim(ClaimTypes.Role, x.Name)).ToList();
        claims.Add(new Claim(ClaimTypes.Email, Email));
        claims.Add(new Claim(ClaimTypes.Name, FullName));
        return claims;
    }

    public void AddRole(Role role)
    {
        Check.ArgumentNotNull(role);

        _roles.Add(role);
    }

    public void RemoveRole(Role role)
    {
        Check.ArgumentNotNull(role);

        _roles.Remove(role);
    }
}
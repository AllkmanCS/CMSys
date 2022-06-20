namespace CMSys.Core.Entities.Membership;

public sealed class UserRole : Entity
{
    public Guid UserId { get; }
    public Guid RoleId { get; }
}
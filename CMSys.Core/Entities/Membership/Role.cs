namespace CMSys.Core.Entities.Membership;

public sealed class Role : Entity<Guid>
{
    public const int NameLength = 64;

    private readonly HashSet<User> _users = new();

    public string Name { get; }

    public IReadOnlyCollection<User> Users => _users;

    public Role(string name) => Name = name;

    private Role()
    {
    }
}
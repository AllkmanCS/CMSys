using CMSys.Core.Entities.Membership;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CMSys.Infrastructure.Configs.Membership;

internal sealed class UserConfig : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable(nameof(User), Schema.Membership);

        builder.HasIndex(x => x.Email).IsUnique();
        builder.Property(x => x.Email).IsRequired().HasMaxLength(User.EmailLength);
        builder.Property(x => x.PasswordHash).IsRequired().HasMaxLength(User.PasswordHashLength);
        builder.Property(x => x.PasswordSalt).IsRequired().HasMaxLength(User.PasswordSaltLength);
        builder.Property(x => x.FirstName).IsRequired().HasMaxLength(User.FirstNameLength);
        builder.Property(x => x.LastName).IsRequired().HasMaxLength(User.LastNameLength);
        //builder.Property(x => x.StartDate).HasColumnType("date");
        //builder.Property(x => x.EndDate).HasColumnType("date");
        builder.Property(x => x.Department).HasMaxLength(User.DepartmentLength);
        builder.Property(x => x.Position).HasMaxLength(User.PositionLength);
        builder.Property(x => x.Location).HasMaxLength(User.LocationLength);

        builder.HasMany(x => x.Roles).WithMany(x => x.Users)
            .UsingEntity<UserRole>(
                right => right.HasOne<Role>().WithMany().HasForeignKey(x => x.RoleId),
                left => left.HasOne<User>().WithMany().HasForeignKey(x => x.UserId),
                join => join.ToTable("UserRole"));
    }
}
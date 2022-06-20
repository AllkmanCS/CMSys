using CMSys.Core.Entities.Catalog;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CMSys.Infrastructure.Configs.Catalog;

internal sealed class CourseConfig : IEntityTypeConfiguration<Course>
{
    public void Configure(EntityTypeBuilder<Course> builder)
    {
        builder.ToTable(nameof(Course), Schema.Catalog);

        builder.HasIndex(x => x.Name).IsUnique();
        builder.Property(x => x.Name).IsRequired().HasMaxLength(Course.NameLength);
        builder.Property(x => x.Description).HasMaxLength(Course.DescriptionLength);

        builder.HasOne(x => x.CourseType).WithMany().HasForeignKey(x => x.CourseTypeId);
        builder.HasOne(x => x.CourseGroup).WithMany().HasForeignKey(x => x.CourseGroupId);
        builder.HasMany(x => x.Trainers).WithOne().HasForeignKey(x => x.CourseId);
    }
}
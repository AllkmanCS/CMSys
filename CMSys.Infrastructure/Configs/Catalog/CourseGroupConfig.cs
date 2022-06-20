using CMSys.Core.Entities.Catalog;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CMSys.Infrastructure.Configs.Catalog;

internal sealed class CourseGroupConfig : IEntityTypeConfiguration<CourseGroup>
{
    public void Configure(EntityTypeBuilder<CourseGroup> builder)
    {
        builder.ToTable(nameof(CourseGroup), Schema.Catalog);

        builder.HasIndex(x => x.Name).IsUnique();
        builder.Property(x => x.Name).IsRequired().HasMaxLength(CourseGroup.NameLength);
        builder.Property(x => x.Description).HasMaxLength(CourseGroup.DescriptionLength);
    }
}
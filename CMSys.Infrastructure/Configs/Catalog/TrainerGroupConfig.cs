using CMSys.Core.Entities.Catalog;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CMSys.Infrastructure.Configs.Catalog;

internal sealed class TrainerGroupConfig : IEntityTypeConfiguration<TrainerGroup>
{
    public void Configure(EntityTypeBuilder<TrainerGroup> builder)
    {
        builder.ToTable(nameof(TrainerGroup), Schema.Catalog);

        builder.HasIndex(x => x.Name).IsUnique();
        builder.Property(x => x.Name).IsRequired().HasMaxLength(TrainerGroup.NameLength);
        builder.Property(x => x.Description).HasMaxLength(TrainerGroup.DescriptionLength);
    }
}
using CMSys.Core.Entities.Catalog;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CMSys.Infrastructure.Configs.Catalog;

internal sealed class TrainerConfig : IEntityTypeConfiguration<Trainer>
{
    public void Configure(EntityTypeBuilder<Trainer> builder)
    {
        builder.ToTable(nameof(Trainer), Schema.Catalog);

        builder.Property(x => x.Id).ValueGeneratedNever();
        builder.Property(x => x.Description).HasMaxLength(Trainer.DescriptionLength);

        builder.HasOne(x => x.User).WithMany().HasForeignKey(x => x.Id);
        builder.HasOne(x => x.TrainerGroup).WithMany().HasForeignKey(x => x.TrainerGroupId);
    }
}
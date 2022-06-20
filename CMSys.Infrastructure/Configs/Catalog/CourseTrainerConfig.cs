using CMSys.Core.Entities.Catalog;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CMSys.Infrastructure.Configs.Catalog;

internal sealed class CourseTrainerConfig : IEntityTypeConfiguration<CourseTrainer>
{
    public void Configure(EntityTypeBuilder<CourseTrainer> builder)
    {
        builder.ToTable(nameof(CourseTrainer), Schema.Catalog);
        builder.HasKey(x => new {x.CourseId, x.TrainerId});

        builder.HasOne(x => x.Trainer).WithMany().HasForeignKey(x => x.TrainerId);
    }
}
using System.Linq.Expressions;
using CMSys.Core.Entities.Catalog;
using CMSys.Core.Repositories.Catalog;
using Microsoft.EntityFrameworkCore;

namespace CMSys.Infrastructure.Repositories.Catalog;

internal sealed class CourseTrainerRepository : Repository<CourseTrainer>, ICourseTrainerRepository
{
    internal CourseTrainerRepository(DbContext context) : base(context)
    {
    }

    public CourseTrainer Find(Guid courseId, Guid trainerId) =>
        Find(x => x.CourseId == courseId && x.TrainerId == trainerId);

    protected override IEnumerable<CourseTrainer> InternalFilter(Expression<Func<CourseTrainer, bool>> predicate)
    {
        return MakeInclusions().Where(predicate).OrderBy(x => x.VisualOrder).ToList();
    }

    protected override IQueryable<CourseTrainer> MakeInclusions()
    {
        return DbSet.Include(x => x.Trainer)
            .ThenInclude(x => x.User)
            .Include(x => x.Trainer)
            .ThenInclude(x => x.TrainerGroup);
    }
}
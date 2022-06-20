using CMSys.Core.Entities.Catalog;

namespace CMSys.Core.Repositories.Catalog;

public interface ICourseTrainerRepository : IRepository<CourseTrainer>
{
    CourseTrainer Find(Guid courseId, Guid trainerId);
}
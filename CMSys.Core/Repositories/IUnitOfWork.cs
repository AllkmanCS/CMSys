using CMSys.Core.Repositories.Catalog;
using CMSys.Core.Repositories.Membership;

namespace CMSys.Core.Repositories;

public interface IUnitOfWork : IDisposable
{
    ICourseGroupRepository CourseGroupRepository { get; }
    ICourseTrainerRepository CourseTrainerRepository { get; }
    ICourseTypeRepository CourseTypeRepository { get; }
    ICourseRepository CourseRepository { get; }
    ITrainerRepository TrainerRepository { get; }
    ITrainerGroupRepository TrainerGroupRepository { get; }

    IUserRepository UserRepository { get; }
    IRoleRepository RoleRepository { get; }

    void Commit();
}
using CMSys.Core.Exceptions;
using CMSys.Core.Repositories;
using CMSys.Core.Repositories.Catalog;
using CMSys.Core.Repositories.Membership;
using CMSys.Infrastructure.Repositories.Catalog;
using CMSys.Infrastructure.Repositories.Membership;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace CMSys.Infrastructure;

public sealed class UnitOfWork : IUnitOfWork
{
    private bool _isDisposed;
    private readonly DbContextOptions _options;
    private AppContext _context;

    private CourseGroupRepository _courseGroupRepository;
    private CourseTrainerRepository _courseTrainerRepository;
    private TrainerGroupRepository _trainerGroupRepository;
    private TrainerRepository _trainerRepository;
    private CourseRepository _courseRepository;
    private CourseTypeRepository _courseTypeRepository;

    private RoleRepository _roleRepository;
    private UserRepository _userRepository;

    private AppContext Context => _context ??= new AppContext(_options);

    #region Catalog

    public ICourseGroupRepository CourseGroupRepository =>
        _courseGroupRepository ??= new CourseGroupRepository(Context);

    public ICourseTrainerRepository CourseTrainerRepository =>
        _courseTrainerRepository ??= new CourseTrainerRepository(Context);

    public ITrainerGroupRepository TrainerGroupRepository =>
        _trainerGroupRepository ??= new TrainerGroupRepository(Context);

    public ITrainerRepository TrainerRepository =>
        _trainerRepository ??= new TrainerRepository(Context);

    public ICourseTypeRepository CourseTypeRepository =>
        _courseTypeRepository ??= new CourseTypeRepository(Context);

    public ICourseRepository CourseRepository =>
        _courseRepository ??= new CourseRepository(Context);

    #endregion

    #region Membership

    public IRoleRepository RoleRepository =>
        _roleRepository ??= new RoleRepository(Context);

    public IUserRepository UserRepository =>
        _userRepository ??= new UserRepository(Context);

    #endregion

    public UnitOfWork(IOptions<UnitOfWorkOptions> accessor) : this(accessor.Value)
    {
    }

    public UnitOfWork(UnitOfWorkOptions options)
    {
        var optionsBuilder = new DbContextOptionsBuilder();
        optionsBuilder.UseSqlServer(options.ConnectionString, x => x.CommandTimeout(options.CommandTimeout));
        _options = optionsBuilder.Options;
    }

    public void Commit()
    {
        if (_context is null)
        {
            return;
        }

        if (_isDisposed)
        {
            throw new ObjectDisposedException("UnitOfWork");
        }

        try
        {
            Context.SaveChanges();
        }
        catch (DbUpdateConcurrencyException ex)
        {
            throw new ConcurrencyException(ex.Entries.Select(x => x.Entity.ToString()));
        }
        catch (DbUpdateException ex)
        {
            throw new UpdateException(ex.Message, ex);
        }
        catch (Exception ex)
        {
            throw new RepositoryException("Commit error.", ex);
        }
    }

    public void Dispose()
    {
        if (_context is null || _isDisposed)
        {
            return;
        }

        _context.Dispose();
        _isDisposed = true;
    }
}
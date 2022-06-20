using System.Linq.Expressions;

namespace CMSys.Core.Entities.Catalog;

public sealed class Course : VisibleEntity
{
    public const int NameLength = 64;
    public const int DescriptionLength = 4000;

    private readonly HashSet<CourseTrainer> _trainers = new();

    public bool IsNew { get; private set; }
    public Guid CourseTypeId { get; private set; }
    public Guid CourseGroupId { get; private set; }

    public CourseType CourseType { get; }
    public CourseGroup CourseGroup { get; }

    public IReadOnlyCollection<CourseTrainer> Trainers => _trainers;

    public Course(string name, int visualOrder, string description, bool isNew, Guid courseTypeId, Guid courseGroupId) :
        base(name, visualOrder, description)
    {
        IsNew = isNew;
        CourseTypeId = courseTypeId;
        CourseGroupId = courseGroupId;
    }

    private Course()
    {
    }

    public bool HasTrainer(Trainer trainer) => _trainers.Select(x => x.Trainer).Contains(trainer);

    public void Update(string name, int visualOrder, string description, bool isNew, Guid courseTypeId,
        Guid courseGroupId)
    {
        Update(name, visualOrder, description);
        IsNew = isNew;
        CourseTypeId = courseTypeId;
        CourseGroupId = courseGroupId;
    }

    public static Expression<Func<Course, bool>> BuildFilterPredicate(Guid? courseTypeId, Guid? courseGroupId)
    {
        return (courseTypeId, courseGroupId) switch
        {
            {courseTypeId: { }, courseGroupId: { }} => x =>
                x.CourseTypeId == courseTypeId.Value && x.CourseGroupId == courseGroupId.Value,
            {courseTypeId: { }} => x => x.CourseTypeId == courseTypeId.Value,
            {courseGroupId: { }} => x => x.CourseGroupId == courseGroupId.Value,
            _ => null
        };
    }
}
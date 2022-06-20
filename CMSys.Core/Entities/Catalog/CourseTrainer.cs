namespace CMSys.Core.Entities.Catalog;

public sealed class CourseTrainer : Entity
{
    public Guid CourseId { get; }
    public Guid TrainerId { get; }
    public int VisualOrder { get; private set; }

    public Trainer Trainer { get; }

    public CourseTrainer(Guid courseId, Guid trainerId)
    {
        CourseId = courseId;
        TrainerId = trainerId;
    }

    private CourseTrainer()
    {
    }

    public void Update(int visualOrder)
    {
        VisualOrder = visualOrder;
    }
}
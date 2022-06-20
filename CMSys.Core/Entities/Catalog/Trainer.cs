using CMSys.Core.Entities.Membership;

namespace CMSys.Core.Entities.Catalog;

public sealed class Trainer : Entity<Guid>
{
    public const int DescriptionLength = 4000;

    public int VisualOrder { get; private set; }
    public Guid TrainerGroupId { get; private set; }
    public string Description { get; private set; }

    public User User { get; }
    public TrainerGroup TrainerGroup { get; }

    public Trainer(Guid id, int visualOrder, Guid trainerGroupId, string description)
    {
        Id = id;
        VisualOrder = visualOrder;
        TrainerGroupId = trainerGroupId;
        Description = description;
    }

    private Trainer()
    {
    }

    public void Update(int visualOrder, Guid trainerGroupId, string description)
    {
        VisualOrder = visualOrder;
        TrainerGroupId = trainerGroupId;
        Description = description;
    }
}
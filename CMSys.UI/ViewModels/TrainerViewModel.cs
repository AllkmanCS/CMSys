namespace CMSys.UI.ViewModels
{
    public class TrainerViewModel
    {
        public const int DescriptionLength = 4000;

        public int VisualOrder { get; private set; }
        public Guid TrainerGroupId { get; private set; }
        public string Description { get; private set; }

        public UserViewModel User { get; }
        public TrainerGroupViewModel TrainerGroup { get; }
    }
}

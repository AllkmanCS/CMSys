namespace CMSys.UI.ViewModels
{
    public class CourseTrainerViewModel
    {
        public Guid CourseId { get; }
        public Guid TrainerId { get; }
        public int VisualOrder { get; private set; }

        public TrainerViewModel Trainer { get; }
    }
}

namespace CMSys.UI.ViewModels
{
    public class CourseViewModel
    {
        public Guid? Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? CourseTypeName { get; set; }
        public string? CourseGroupName { get; set; }
        public bool IsNew { get; set; }
        public List<TrainerViewModel>? Trainers { get; set; }
        public CourseViewModel()
        {

        }
    }
}

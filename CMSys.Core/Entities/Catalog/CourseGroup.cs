namespace CMSys.Core.Entities.Catalog;

public sealed class CourseGroup : VisibleEntity
{
    public const int NameLength = 64;
    public const int DescriptionLength = 256;

    public CourseGroup(string name, int visualOrder, string description) : base(name, visualOrder, description)
    {
    }

    private CourseGroup()
    {
    }
}
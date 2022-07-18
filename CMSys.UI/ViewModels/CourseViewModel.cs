using CMSys.Common.Paging;
using CMSys.Core.Entities.Catalog;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CMSys.UI.ViewModels
{
    public class CourseViewModel
    {
        public Guid? Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int VisualOrder { get; set; }
        //public string? CourseTypeName { get; set; }
        //public string? CourseGroupName { get; set; }
        public bool IsNew { get; set; }
        public List<CourseTrainerViewModel>? Trainers { get; set; }
        public PageInfo PageInfo { get; set; } = new PageInfo();
        public int PerPage { get; set; } = 5;
        public ICollection<SelectListItem> CourseTypes { get; set; }
        //coursetype selectlistitem .text == coursetypeName value == id
        public ICollection<SelectListItem> CourseGroups { get; set; }
        public CourseTypeViewModel CourseType { get; set; }
        public CourseGroupViewModel CourseGroup { get; set; }
    }
}

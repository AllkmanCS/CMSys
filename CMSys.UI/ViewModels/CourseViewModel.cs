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
        public string? CourseTypeName { get; set; }
        public string? CourseGroupName { get; set; }
        public bool IsNew { get; set; }
        public List<CourseTrainerViewModel>? Trainers { get; set; }
        public PageInfo PageInfo { get; set; } = new PageInfo();
        public int PerPage { get; set; } = 5;
        public ICollection<SelectListItem> CourseTypes { get; set; }
        public ICollection<SelectListItem> CourseGroups { get; set; }
    }
}

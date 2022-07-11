using CMSys.Common.Paging;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CMSys.UI.ViewModels
{
    public class CoursesViewModel
    {
        public int Total { get; set; }
        public int Page { get; set; }
        public int PerPage { get; set; }
        public int TotalPages { get; set; }
        public bool CanNext{ get; set; }
        public bool CanPrevious { get; set; }
        public int From { get; set; }
        public int To { get; set; }
        public List<CourseViewModel> Items { get; set; } = new List<CourseViewModel>();
        public List<int>? Pagination { get; set; }
        public int NextPageNumber { get; set; }
        public int PreviousPageNumber { get; set; }
        public ICollection<SelectListItem> CourseTypes { get; set; }
        public ICollection<SelectListItem> CourseGroups { get; set; }
        public CourseViewModel CourseViewModel { get; set; }
    }
}

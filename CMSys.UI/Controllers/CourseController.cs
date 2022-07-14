using AutoMapper;
using CMSys.Common.Paging;
using CMSys.Core.Entities.Catalog;
using CMSys.Core.Repositories;
using CMSys.UI.Helpers;
using CMSys.UI.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;
using System.IO;

namespace CMSys.UI.Controllers
{
    public class CourseController : Controller
    {
        private IUnitOfWork _context;
        private IMapper _mapper;

        public CourseController(IUnitOfWork context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
           
        }
        [Route("courses")]
        public IActionResult Index(int page, int perPage, string courseTypeName)
        {
            CoursesViewModel mappedCourses = GetCoursesViewModel(page, perPage, courseTypeName);
            return View(mappedCourses);
        }
        [Route("courses/{id}")]
        public IActionResult GetCourse(Guid id, int page, int perPage, string courseTypeName)
        {
            var courseViewModel = new CourseViewModel();
            page = courseViewModel.PageInfo.Page;
            perPage = courseViewModel.PageInfo.PerPage;
            var pagedList = _context.CourseRepository.GetPagedList(new PageInfo(page, perPage),
                c => string.IsNullOrEmpty(courseTypeName) ? true : c.CourseType.Name == courseTypeName);
            var course = pagedList.Items.Where(x => x.Id == id).FirstOrDefault();
            if (course == null)
            {
                return NotFound();
            }
            var mappedCourse = _mapper.Map<CourseViewModel>(course);
            foreach (var item in mappedCourse.Trainers)
            {
                var trainerPhoto = item.Trainer.User.Photo;
                var filePath = $"../CMSys.UI/wwwroot/img/{item.Trainer.User.FullName}.png";
                using (var ms = new MemoryStream(trainerPhoto))
                FileWriter.WriteBytesToFile(filePath, trainerPhoto);
            }
                return View(mappedCourse);
        }
        [Authorize]
        [Route("admin/courses")]
        public IActionResult CourseCollection(int page, int perPage, string courseTypeName)
        {
            var mappedCourses = GetCoursesViewModel(page, perPage, courseTypeName);
            return View(mappedCourses);
        }
        [Authorize]
        [Route("admin/coursegroups")]
        public IActionResult CourseGroupsCollection(List<CourseGroupViewModel> courseGroupsViewModel)
        {
            var courseGroups = _context.CourseGroupRepository.All();
            var mappedCourseGroups = _mapper.Map(courseGroups, courseGroupsViewModel);
            return View(mappedCourseGroups);
        }
        [Authorize]
        [Route("admin/courses/create")]
        public IActionResult CreateCourse(CourseViewModel courseViewModel)
        {
            courseViewModel = new CourseViewModel();
            var mappedCourse = _mapper.Map<Course>(courseViewModel);
            _context.CourseRepository.Add(mappedCourse);
            _context.Commit();
            return View(mappedCourse);
        }
        private CoursesViewModel GetCoursesViewModel(int page, int perPage, string courseTypeName)
        {
            var coursesViewModel = new CoursesViewModel();
            page = coursesViewModel.PageInfo.Page;
            perPage = coursesViewModel.PerPage;

            var pagedList = _context.CourseRepository.GetPagedList(new PageInfo(page, perPage),
              c => string.IsNullOrEmpty(courseTypeName) ? true : c.CourseType.Name == courseTypeName);
            var mappedCourses = _mapper.Map(pagedList, coursesViewModel);
            //pagination
            for (int i = 1; i < pagedList.TotalPages; i++)
            {
                if (pagedList.IsNearFromPageOrBoundary(i))
                {
                    coursesViewModel.Pagination.Add(i);
                }
            }
            coursesViewModel.CourseTypes = CourseTypes();
            coursesViewModel.CourseGroups = CourseGroups();

            var courses = new List<CourseViewModel>();
            foreach (var item in mappedCourses.Items)
            {
                courses.Add(item);
            }

            return mappedCourses;
        }
        private ICollection<SelectListItem> CourseTypes()
        {
            var courseTypes = _context.CourseRepository.All().Select(ct => (ct.Name, ct.VisualOrder, ct.Id)).ToList();
            var list = new List<SelectListItem>();
            foreach (var item in courseTypes)
            {
                list.Add(new SelectListItem(item.Name, item.Id.ToString()));
            }
            return list;
        }
        private ICollection<SelectListItem> CourseGroups()
        {
            var courseGroups = _context.CourseGroupRepository.All().Select(ct => (ct.Name, ct.VisualOrder, ct.Id)).ToList();
            var list = new List<SelectListItem>() { new SelectListItem("Select Group Type", "0") };
            foreach (var item in courseGroups)
            {
                list.Add(new SelectListItem(item.Name, item.Id.ToString()));
                
            }
            return list;
        }
        public IActionResult Privacy()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        //public IActionResult FilterCourses(Guid courseTypeId, Guid courseGroupId)
        //{
        //    var coursesViewModel = new CoursesViewModel();

        //    return View(coursesViewModel);
        //}
    }
}
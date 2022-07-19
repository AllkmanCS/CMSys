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
            CoursesViewModel courses = GetCoursesViewModel(page, perPage, courseTypeName);
            return View(courses);
        }
        [Authorize]
        [Route("admin/courses")]
        public IActionResult CourseCollection(int page, int perPage, string courseTypeName)
        {
            var mappedCourses = GetCoursesViewModel(page, perPage, courseTypeName);
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
        [Route("admin/coursegroups")]
        public IActionResult CourseGroupsCollection(List<CourseGroupViewModel> courseGroupsViewModel)
        {
            var courseGroups = _context.CourseGroupRepository.All();
            var mappedCourseGroups = _mapper.Map(courseGroups, courseGroupsViewModel);
            return View(mappedCourseGroups);
        }
        [Authorize]
        [Route("admin/courses/create")]
        [HttpGet]
        public IActionResult CourseForm(CourseViewModel courseViewModel)
        {
            courseViewModel = new CourseViewModel();

            courseViewModel.CourseTypes = CourseTypes();
            courseViewModel.CourseGroups = CourseGroups();
            return View(courseViewModel);
        }
        [Authorize]
        [HttpPost]
        [Route("admin/courses/create")]
        public IActionResult CreateCourse([FromForm]CourseViewModel courseViewModel)
        {
            courseViewModel.CourseTypes = CourseTypes();
            courseViewModel.CourseGroups = CourseGroups();

            if (courseViewModel == null)
            {
                return BadRequest("Course object is null.");
            }

            var mappedCourse = _mapper.Map<Course>(courseViewModel);

            _context.CourseRepository.Add(mappedCourse);
           
            _context.Commit();

            return RedirectToAction("CourseCollection");
        }

        public IActionResult Update(Guid id)
        {

            return View();
        }
        [HttpPost, ActionName("Update")]
        public IActionResult UpdateCourse(Guid? id)
        {
            if (id == null)
            {
                return BadRequest("Course object is null.");
            }
            var course = _mapper.Map<Course>(id);
        }
        private CoursesViewModel GetCoursesViewModel(int page, int perPage, string courseTypeName)
        {
            var coursesViewModel = new CoursesViewModel();
            page = coursesViewModel.Page;
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
            return mappedCourses;
        }
        private ICollection<SelectListItem> CourseTypes()
        {
            var courseTypes = _context.CourseTypeRepository.All().Select(ct => (ct.Name, ct.VisualOrder, ct.Id)).ToList();
            var list = new List<SelectListItem>() { new SelectListItem("Select Course Type", "0") };
            foreach (var item in courseTypes)
            {
                list.Add(new SelectListItem(item.Name, item.Id.ToString()));
            }
            return list;
        }
        private ICollection<SelectListItem> CourseGroups()
        {
            var courseGroups = _context.CourseGroupRepository.All().Select(ct => (ct.Name, ct.VisualOrder, ct.Id)).ToList();
            var list = new List<SelectListItem>() { new SelectListItem("Select Course Group", "0") };
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
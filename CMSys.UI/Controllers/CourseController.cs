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
    //[Route("/courses")]
    public class CourseController : Controller
    {
        private IUnitOfWork _context;
        private IMapper _mapper;
        private const int PAGE = 1;
        private const int PER_PAGE = 5;
        private readonly string _courseTypeName = "";
        private PagedList<Course> _pagedList;
        
        public CourseController(IUnitOfWork context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _pagedList = _context.CourseRepository.GetPagedList(new PageInfo(PAGE, PER_PAGE),
               c => string.IsNullOrEmpty(_courseTypeName) ? true : c.CourseType.Name == _courseTypeName);
        }
        [Route("courses")]
        public IActionResult Index(int page = 1, int perPage = 5, string courseTypeName = "")
        {
            var coursesViewModel = new CoursesViewModel();

            //setting values to model properties
            coursesViewModel.Page = PAGE;
            coursesViewModel.NextPageNumber = PAGE + 1;
            coursesViewModel.PreviousPageNumber = PAGE - 1;
            coursesViewModel.CourseTypes = CourseTypes();
            coursesViewModel.CourseGroups = CourseGroups();

            //pagination
            var paginationList = new List<int>();
            for (int i = 1; i < _pagedList.Total; i++)
            {
                if(_pagedList.IsNearFromPageOrBoundary(i))
                {
                    paginationList.Add(i);
                }
            }
            coursesViewModel.Pagination = paginationList;
            //mapping
            var mappedCourses = _mapper.Map(_pagedList, coursesViewModel);
            var courses = new List<CourseViewModel>();
            foreach (var item in mappedCourses.Items)
            {
                var courseModel = _mapper.Map<CourseViewModel>(item);
                courses.Add(courseModel);
            }
            return View(mappedCourses);
        }
        [Route("courses/{id}")]
        public IActionResult GetCourse(Guid id)
        {
            var courseViewModel = new CourseViewModel();
            var course = _pagedList.Items.Where(x => x.Id == id).FirstOrDefault();
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
        public IActionResult CourseCollection()
        {
            return View();
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
            var courseTypes = _context.CourseGroupRepository.All().Select(ct => (ct.Name, ct.VisualOrder, ct.Id)).ToList();
            var list = new List<SelectListItem>() { new SelectListItem("Select Group Type", "0") };
            foreach (var item in courseTypes)
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
    }
}
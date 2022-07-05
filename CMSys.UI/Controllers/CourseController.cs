using AutoMapper;
using CMSys.Common.Paging;
using CMSys.Core.Entities.Catalog;
using CMSys.Core.Repositories;
using CMSys.UI.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CMSys.UI.Controllers
{
    //[Route("/courses")]
    public class CourseController : Controller
    {
        private readonly ILogger<CourseController> _logger;
        private IUnitOfWork _context;
        private IMapper _mapper;
        public CourseController(ILogger<CourseController> logger, IUnitOfWork context, IMapper mapper)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
        }

        public IActionResult Index(int page = 1, int perPage = 5, string courseTypeName = "")
        {
            var coursesViewModel = new CoursesViewModel();
            //pagination
            //coursesViewModel.NextPageNumber
            var pagedList = _context.CourseRepository.GetPagedList(new PageInfo(page, perPage),
                c => string.IsNullOrEmpty(courseTypeName) ? true : c.CourseType.Name == courseTypeName);
            coursesViewModel.Page = page;
            coursesViewModel.NextPageNumber = page + 1;
            coursesViewModel.PreviousPageNumber = page - 1;
            var mappedCourses = _mapper.Map<CoursesViewModel>(pagedList);
            var courses = new List<CourseViewModel>();
            foreach (var item in pagedList.Items)
            {
                var courseModel = _mapper.Map<CourseViewModel>(item);
                courses.Add(courseModel);
            }
           // Console.WriteLine(mappedCourses);
            coursesViewModel.Items = courses;
            return View(coursesViewModel);
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
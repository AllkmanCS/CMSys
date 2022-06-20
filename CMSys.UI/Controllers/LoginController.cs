using AutoMapper;
using CMSys.Core.Entities.Membership;
using CMSys.Core.Repositories;
using CMSys.Infrastructure;
using CMSys.UI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CMSys.UI.Controllers
{
    public class LoginController : Controller
    {
        IUnitOfWork _users;
        IMapper _mapper;
        public LoginController(IUnitOfWork users, IMapper mapper)
        {
            _users = users;
            _mapper = mapper;
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(string email)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }
            var user = _users.UserRepository.FindByEmail(email);
            var model = _mapper.Map<User, LoginViewModel>(user);
            return View(model);
        }
    }
}

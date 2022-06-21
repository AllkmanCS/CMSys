using AutoMapper;
using CMSys.Core.Entities.Membership;
using CMSys.Core.Repositories;
using CMSys.Infrastructure;
using CMSys.UI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace CMSys.UI.Controllers
{
    public class LoginController : Controller
    {
        private IUnitOfWork _users;
        private IMapper _mapper;
        public LoginController(IUnitOfWork users, IMapper mapper)
        {
            _users = users;
            _mapper = mapper;
        }
        public async Task<ActionResult> Login()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(string email, string password)
        {
            //need to compare hashed password with inputed password hash

            if(ModelState.IsValid)
            {
                var passwordInputHash = MD5Hash(password);
                var user = _users.UserRepository.FindByEmail(email);
                
                var model = _mapper.Map<User, LoginViewModel>(user);
                if (model.Email.Equals(email) && model.PasswordHash.Equals(passwordInputHash))
                {
                    return RedirectToAction("Index");
                }
                
                return View();
            }
            

            if (model == null)
            return View();
        }
        public static string MD5Hash(string text)
        {
            MD5 md5 = new MD5CryptoServiceProvider();

            //compute hash from the bytes of text  
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));

            //get hash result after compute it  
            byte[] result = md5.Hash;

            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                //change it into 2 hexadecimal digits  
                //for each byte  
                strBuilder.Append(result[i].ToString("x2"));
            }

            return strBuilder.ToString();
        }
    }
}

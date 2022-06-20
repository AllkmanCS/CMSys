using CMSys.Core.Entities.Membership;
using CMSys.UI.Models;
using AutoMapper;

namespace CMSys.UI.Automapper
{
    public class LoginProfile : Profile
    {
        public LoginProfile()
        {
            CreateMap<User, LoginViewModel>();
        }
    }
}

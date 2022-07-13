﻿using AutoMapper;
using CMSys.Core.Entities.Membership;
using CMSys.UI.ViewModels;

namespace CMSys.UI.Automapper
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserViewModel>();
            //.AfterMap((domain, view) => view.Photo[55] = 3b);`
        }
    }
}

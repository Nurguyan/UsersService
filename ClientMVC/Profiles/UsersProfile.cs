using AutoMapper;
using ClientMVC.Dtos;
using ClientMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientMVC.Profiles
{
    public class UsersProfile : Profile
    {
        public UsersProfile()
        {
            // source -> target
            //Server -> Client
            CreateMap<PhoneDto, Phone>();
            CreateMap<UserDetail, User>()
                .ForMember(dest => dest.Phones, opt => opt.MapFrom(src => src.PhoneDtos));

            //Client -> Server
            CreateMap<User, UserDetail>()
                .ForMember(dest => dest.PhoneDtos, opt => opt.MapFrom(src => src.Phones)); ;
            CreateMap<Phone, PhoneDto>();

            //Controller -> View
            CreateMap<User, UserViewModel>();
            CreateMap<Phone, PhoneViewModel>();
            CreateMap<UserViewModel, User>();
            CreateMap<PhoneViewModel, Phone>();
        }
    }
}

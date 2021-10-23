using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsersService.Models;
using UsersService;

namespace UsersService.Profiles
{
    public class UsersProfile : Profile
    {
        public UsersProfile()
        {
            // source -> target   
            CreateMap<Phone, PhoneDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Number, opt => opt.MapFrom(src => src.Number))
                .ForAllOtherMembers(x => x.Ignore());
            CreateMap<User, UserDetail>()
                .ForMember(dest => dest.PhoneDtos, opt => opt.MapFrom(src => src.Phones));

            CreateMap<UserDetail, User>()
                .ForMember(dest => dest.Phones, opt => opt.MapFrom(src => src.PhoneDtos))
                .AfterMap((src, dest) =>
                {
                    var phones = dest.Phones;
                    foreach (var phone in phones)
                    {
                        phone.UserId = src.Id;
                        phone.User = dest;
                    }
                });
            CreateMap<PhoneDto, Phone>();

        }
    }
}

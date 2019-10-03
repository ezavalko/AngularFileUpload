using AngularFileUpload.Data.Models;
using AngularFileUpload.ViewModels;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularFileUpload.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserViewModel, ApplicationUser>().ReverseMap();
            CreateMap<FileViewModel, File>().ReverseMap();
        }
    }
}

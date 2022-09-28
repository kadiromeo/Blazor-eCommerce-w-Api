using AutoMapper;
using Blazor_eCommerce_Project.Data.Access.Data;
using Blazor_eCommerce_Project.Models;

namespace Blazor_eCommerce_Project.Business.Mapper
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<CourseDTO, Course>().ReverseMap();
        }
    }
}

using AutoMapper;
using LojaQualquer.Web.Application.Models.Request;
using LojaQualquer.Web.ViewModels;

namespace LojaQualquer.Web.Mapper
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            CreateMap<LoginViewModel, LoginRequest>();
        }
    }
}
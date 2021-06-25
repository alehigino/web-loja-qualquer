using AutoMapper;
using LojaQualquer.Web.Application.Models.Request;
using LojaQualquer.Web.Application.Models.Response;
using LojaQualquer.Web.ViewModels;

namespace LojaQualquer.Web.Mapper
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            CreateMap<LoginViewModel, LoginRequest>();
            CreateMap<ProductViewModel.FilterProduct, FilterProductRequest>();
            CreateMap<ProductResponse, ProductViewModel.ProductContent>()
                .ForMember(x => x.Price, src => src.MapFrom(p => p.Price.ToString("c")));
            CreateMap<ProductCreateUpdateViewModel, ProductCreateUpdateRequest>();
            CreateMap<ProductResponse, ProductCreateUpdateViewModel>();
        }
    }
}
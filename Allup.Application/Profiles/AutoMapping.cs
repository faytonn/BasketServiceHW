using Allup.Application.ViewModels;
using Allup.Domain.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allup.Application.Profiles
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<LanguageViewModel, Language>().ReverseMap();
            CreateMap<Category, CategoryViewModel>().ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.CategoryTranslations.FirstOrDefault().Name)).ReverseMap();
            CreateMap<CategoryTranslationViewModel, CategoryTranslation>().ReverseMap();
        }
    }
}

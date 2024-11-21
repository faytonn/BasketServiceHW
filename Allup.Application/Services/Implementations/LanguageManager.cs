using Allup.Application.Services.Abstracts;
using Allup.Application.ViewModels;
using Allup.Persistence.Repositories.Abstraction;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allup.Application.Services.Implementations
{
    public class LanguageManager : ILanguageService
    {
        private readonly ILanguageRepository _languageRepository;
        private readonly IMapper _mapper;

        public LanguageManager(ILanguageRepository languageRepository, IMapper mapper)
        {
            _languageRepository = languageRepository;
            _mapper = mapper;
        }

        public async Task<LanguageViewModel> GetLanguageAsync(string isoCode)
        {
            var languages = await _languageRepository.GetAllAsync();

            var language = languages.FirstOrDefault(x => x.IsoCode.ToLower() == isoCode.ToLower());

            return _mapper.Map<LanguageViewModel>(language);
        }

        public async Task<List<LanguageViewModel>> GetLanguagesAsync()
        {
            var languages = await _languageRepository.GetAllAsync();
            var languagesViewModels = _mapper.Map<List<LanguageViewModel>>(languages);

            return languagesViewModels;
        }
    }
}

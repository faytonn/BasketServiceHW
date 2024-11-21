using Allup.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allup.Application.Services.Abstracts
{
    public interface ILanguageService
    {
        Task<List<LanguageViewModel>> GetLanguagesAsync();
        Task<LanguageViewModel> GetLanguageAsync(string isoCode);
    }
}

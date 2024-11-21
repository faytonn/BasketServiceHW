using Allup.Application.ViewModels;

namespace Allup.Application.Services.Abstracts;

public interface ILanguageService
{
    Task<List<LanguageViewModel>> GetLanguagesAsync();
    Task<LanguageViewModel> GetLanguageAsync(string isoCode);
}

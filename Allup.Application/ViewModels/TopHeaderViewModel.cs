namespace Allup.Application.ViewModels;

public class TopHeaderViewModel
{
    public List<LanguageViewModel>? Languages { get; set; }
    public LanguageViewModel? SelectedLanguage { get; set; }
    public int CompareItemCount {  get; set; }
}

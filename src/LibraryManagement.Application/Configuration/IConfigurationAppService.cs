using System.Threading.Tasks;
using LibraryManagement.Configuration.Dto;

namespace LibraryManagement.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}

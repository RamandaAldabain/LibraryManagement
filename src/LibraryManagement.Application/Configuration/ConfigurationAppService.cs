using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using LibraryManagement.Configuration.Dto;

namespace LibraryManagement.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : LibraryManagementAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}

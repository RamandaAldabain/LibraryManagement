using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace LibraryManagement.Controllers
{
    public abstract class LibraryManagementControllerBase: AbpController
    {
        protected LibraryManagementControllerBase()
        {
            LocalizationSourceName = LibraryManagementConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}

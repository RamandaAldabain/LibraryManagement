using Abp.Authorization;
using LibraryManagement.Authorization.Roles;
using LibraryManagement.Authorization.Users;

namespace LibraryManagement.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}

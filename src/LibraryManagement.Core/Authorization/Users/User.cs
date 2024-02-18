using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Authorization.Users;
using Abp.Extensions;

namespace LibraryManagement.Authorization.Users
{
    public class User : AbpUser<User>
    {
		[InverseProperty("Creator")]
		public ICollection<Entities.Task> CreatedTasks {  get; set; } = new List<Entities.Task>();
		[InverseProperty("Assignee")]
		public ICollection<Entities.Task> AssignedTasks {  get; set; } = new List<Entities.Task>();

		public const string DefaultPassword = "123qwe";

        public static string CreateRandomPassword()
        {
            return Guid.NewGuid().ToString("N").Truncate(16);
        }

        public static User CreateTenantAdminUser(int tenantId, string emailAddress)
        {
            var user = new User
            {
                TenantId = tenantId,
                UserName = AdminUserName,
                Name = AdminUserName,
                Surname = AdminUserName,
                EmailAddress = emailAddress,
                Roles = new List<UserRole>(),
				AssignedTasks = new List<Entities.Task>(),
				CreatedTasks = new List<Entities.Task>()
             
            };

            user.SetNormalizedNames();

            return user;
        }
    }
}

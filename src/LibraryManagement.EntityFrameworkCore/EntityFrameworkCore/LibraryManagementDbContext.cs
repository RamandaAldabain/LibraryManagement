using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using LibraryManagement.Authorization.Roles;
using LibraryManagement.Authorization.Users;
using LibraryManagement.MultiTenancy;
using LibraryManagement.Models;
using LibraryManagement.Models.LookUps;
using LibraryManagement.Entities;

namespace LibraryManagement.EntityFrameworkCore
{
	public class LibraryManagementDbContext : AbpZeroDbContext<Tenant, Role, User, LibraryManagementDbContext>
	{
		/* Define a DbSet for each entity of the application */

		public LibraryManagementDbContext(DbContextOptions<LibraryManagementDbContext> options)
			: base(options)
		{
		}
		public DbSet<User> Users { get; set; }
		public DbSet<Task> Tasks { get; set; }
		public DbSet<Status> Statuses { get; set; }
		public DbSet<TaskType> TaskTypes { get; set; }
	}
}

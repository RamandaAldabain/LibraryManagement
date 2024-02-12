using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using LibraryManagement.Authorization.Roles;
using LibraryManagement.Authorization.Users;
using LibraryManagement.MultiTenancy;
using LibraryManagement.Models;

namespace LibraryManagement.EntityFrameworkCore
{
	public class LibraryManagementDbContext : AbpZeroDbContext<Tenant, Role, User, LibraryManagementDbContext>
	{
		/* Define a DbSet for each entity of the application */

		public LibraryManagementDbContext(DbContextOptions<LibraryManagementDbContext> options)
			: base(options)
		{
		}
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.Entity<Task>()
			 .HasOne<User>()
			 .WithMany()
			 .HasForeignKey(t => t.CreatorUserId)
			 .IsRequired()
			 .OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<Task>()
				.HasOne<User>()
				.WithMany()
				.HasForeignKey(t => t.AssigneeUserId)
				.IsRequired()
				.OnDelete(DeleteBehavior.Restrict);

		}


		public DbSet<Task> Tasks { get; set; }
		//public DbSet<UserProfile> Usersdd { get; set; }
	}
}

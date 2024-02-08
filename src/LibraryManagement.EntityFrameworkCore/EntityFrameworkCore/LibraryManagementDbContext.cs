using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using LibraryManagement.Authorization.Roles;
using LibraryManagement.Authorization.Users;
using LibraryManagement.MultiTenancy;

namespace LibraryManagement.EntityFrameworkCore
{
    public class LibraryManagementDbContext : AbpZeroDbContext<Tenant, Role, User, LibraryManagementDbContext>
    {
        /* Define a DbSet for each entity of the application */
        
        public LibraryManagementDbContext(DbContextOptions<LibraryManagementDbContext> options)
            : base(options)
        {
        }
    }
}

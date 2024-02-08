using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.EntityFrameworkCore
{
    public static class LibraryManagementDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<LibraryManagementDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<LibraryManagementDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}

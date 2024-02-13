using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace LibraryManagement.EntityFrameworkCore
{
    public static class LibraryManagementDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<LibraryManagementDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
			builder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
		}

        public static void Configure(DbContextOptionsBuilder<LibraryManagementDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
			builder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

		}
	}
}

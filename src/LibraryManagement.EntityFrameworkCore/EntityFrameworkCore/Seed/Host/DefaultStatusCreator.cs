using LibraryManagement.Models.LookUps;
using System.Collections.Generic;
using System.Linq;


namespace LibraryManagement.EntityFrameworkCore.Seed.Host
{
	public class DefaultStatusCreator
	{
		private readonly LibraryManagementDbContext _context;
		private readonly List<Status> Statuses;

		public DefaultStatusCreator(LibraryManagementDbContext context)
		{
			_context = context;
			Statuses = new List<Status>
			{
				new Status { Name = "Assigned" },
				new Status {  Name = "UnAssigned" },
				new Status {  Name = "InProgress" },
				new Status { Name = "Completed" },
				new Status {  Name = "Rejected" },
            };
		}
		public void Create()
		{
			CreateStatuses();
		}


		private void CreateStatuses()
		{


			foreach (var status in Statuses)
			{
				AddStatusIfNotExists(status);
			}

		}

		private void AddStatusIfNotExists(Status status)
		{
			if (_context.Statuses.Any(s => s.Name == status.Name))
			{
				return;
			}

			_context.Statuses.Add(status);
			_context.SaveChanges();
		}
	}
}

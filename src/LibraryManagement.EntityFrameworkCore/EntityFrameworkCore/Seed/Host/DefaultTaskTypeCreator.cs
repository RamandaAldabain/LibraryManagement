using LibraryManagement.Models.LookUps;
using System.Collections.Generic;
using System.Linq;


namespace LibraryManagement.EntityFrameworkCore.Seed.Host
{
	public class DefaultTaskTypeCreator
	{
		private readonly LibraryManagementDbContext _context;
		private readonly List<TaskType> TaskTypes;

		public DefaultTaskTypeCreator(LibraryManagementDbContext context)
		{
			_context = context;
			TaskTypes = new List<TaskType>
			{
				new TaskType { Name = "Accountant" },
				new TaskType {Name = "Shelver" },

			};
		}
		public void Create()
		{
			CreateStatuses();
		}


		private void CreateStatuses()
		{


			foreach (var taskType in TaskTypes)
			{
				AddTaskTypeIfNotExists(taskType);
			}

		}

		private void AddTaskTypeIfNotExists(TaskType taskType)
		{
			if (_context.TaskTypes.Any(s => s.Name == taskType.Name))
			{
				return;
			}

			_context.TaskTypes.Add(taskType);
			_context.SaveChanges();
		}
	}
}

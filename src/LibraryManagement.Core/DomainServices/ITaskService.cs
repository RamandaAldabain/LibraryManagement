using Abp.Domain.Services;
using LibraryManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibraryManagement.Models;
using System.Threading.Tasks;
using LibraryManagement.Models.LookUps;
using LibraryManagement.Models.enums;

namespace LibraryManagement.DomainServices
{
	public interface ITaskService : IDomainService
	{
		public Task<Models.Task> Create(Models.Task task);
		public void Update(Models.Task task);
		public void Delete(int id );
		public Task<IEnumerable<Models.Task>> GetAll();
		public IEnumerable<Models.Task> GetByEmployeeTaskType(TaskType name);
		public IEnumerable<Models.Task> GetTaskByDeadlineDate(DateTime date);

		public Task<Models.Task> AssignTask(int userId, int taskId);
		public Task<Models.Task> EditTaskStatus(int userId, int taskId , Status status);

	}
}

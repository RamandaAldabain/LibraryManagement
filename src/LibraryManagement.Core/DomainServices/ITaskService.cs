using Abp.Domain.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LibraryManagement.Models.LookUps;

namespace LibraryManagement.DomainServices
{
	public interface ITaskService : IDomainService
	{
		public Task<Entities.Task> Create(Entities.Task task);
		public void Update(Entities.Task task);
		public void Delete(int id );
		public Task<IEnumerable<Entities.Task>> GetAll();
		public IEnumerable<Entities.Task> GetByEmployeeTaskType(TaskType name);
		public IEnumerable<Entities.Task> GetTaskByDeadlineDate(DateTime from,DateTime to);

		public Task<Entities.Task> AssignTask(long userId, long taskId);
		public Task<Entities.Task> EditTaskStatus(long userId, long taskId , Status status);

	}
}

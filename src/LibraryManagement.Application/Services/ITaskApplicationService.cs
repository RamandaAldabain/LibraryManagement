using Abp.Application.Services;
using LibraryManagement.Dto;
using LibraryManagement.Models.LookUps;
using LibraryManagement.Users.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryManagement.Services
{
	public interface ITaskApplicationService : IApplicationService
	{
		public Task<TaskDto> Create(TaskDto model);
		public void Update(TaskDto model);
		public void Delete(int id);
		public Task<IEnumerable<TaskDto>> GetAll();
		public IEnumerable<TaskDto> GetByEmployeeTaskType(TaskType type);
		public IEnumerable<TaskDto> GetTaskByDeadlineDate(DateTime from , DateTime to);
		public Task<Entities.Task> AssignTask(int userId, int taskId);
		public Task<Entities.Task> EditTaskStatus(TaskStatusDto model);
	}
}

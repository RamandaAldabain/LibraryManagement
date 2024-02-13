using Abp.Application.Services;
using LibraryManagement.Dto;
using LibraryManagement.Models.enums;
using LibraryManagement.Models.LookUps;
using LibraryManagement.Users.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
		public IEnumerable<TaskDto> GetTaskByDeadlineDate(DateTime date);
		public Task<Models.Task> AssignTask(int userId, int taskId);
		public Task<Models.Task> EditTaskStatus(TaskStatusDto model);
	}
}

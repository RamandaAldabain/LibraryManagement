using Abp.Application.Services;
using LibraryManagement.Dto;
using LibraryManagement.Models.LookUps;
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
		public Task<IEnumerable<TaskDto>> GetByRole(string name);
		public Task<Models.Task> AssignTask(int userId, int taskId);
		public Task<Models.Task> EditTaskStatus(int userId, int taskId, Status status);
	}
}

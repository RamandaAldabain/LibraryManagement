using Abp.Application.Services;
using Abp.Authorization;
using AutoMapper;
using LibraryManagement.Authorization;
using LibraryManagement.DomainServices;
using LibraryManagement.Dto;
using LibraryManagement.Models.LookUps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Services
{
	[AbpAuthorize(PermissionNames.Pages_Roles)]

	public class TaskApplicationService : ApplicationService, ITaskApplicationService
	{
		private readonly IMapper _mapper;
		private readonly ITaskService _taskService;

		public TaskApplicationService(IMapper mapper , ITaskService taskService)
		{
			_mapper = mapper;
			_taskService = taskService;
		}

		public async Task<Models.Task> AssignTask(int userId, int taskId)
		{
			if (userId == 0)
				throw new ArgumentNullException(nameof(userId));
			if (taskId == 0)
				throw new ArgumentNullException(nameof(taskId));
			return await _taskService.AssignTask(userId, taskId);
		}

		public async Task<TaskDto> Create(TaskDto model)
		{
			var task = _mapper.Map<TaskDto, Models.Task>(model);
			var newTask = await _taskService.Create(task);
			return  _mapper.Map<Models.Task, TaskDto>(newTask);

		}

		public void Delete(int id)
		{
			_taskService.Delete(id);
		}

		public async Task<Models.Task> EditTaskStatus(int userId, int taskId, Status status)
		{
			if(userId == 0) 
				throw new ArgumentNullException(nameof(userId));
			if(taskId == 0) 
				throw new ArgumentNullException(nameof(taskId));
			if(status == null) 
				throw new ArgumentNullException(nameof(status));
			return await _taskService.EditTaskStatus(userId, taskId, status);
		}

		public async Task<IEnumerable<TaskDto>> GetAll()
		{
			var tasks = await _taskService.GetAll();
			return _mapper.Map<List<Models.Task>, List<TaskDto>>((List<Models.Task>)tasks);
		}

		public async Task<IEnumerable<TaskDto>> GetByRole(string name)
		{
			if(name == null) throw new ArgumentNullException("Name");
			return await _taskService.GetByRole(name);
 		}

		public void Update(TaskDto model)
		{
			var task = _mapper.Map<TaskDto, Models.Task>(model);

			_taskService.Update(task);
		}
	}
}

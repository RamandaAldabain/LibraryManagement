using Abp.Application.Services;
using Abp.Authorization;
using AutoMapper;
using LibraryManagement.Authorization;
using LibraryManagement.DomainServices;
using LibraryManagement.Dto;
using LibraryManagement.Models;
using LibraryManagement.Models.enums;
using LibraryManagement.Models.LookUps;
using LibraryManagement.Users.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Services
{
		[AbpAuthorize]

	public class TaskApplicationService : ApplicationService, ITaskApplicationService
	{
		private readonly IMapper _mapper;
		private readonly ITaskService _taskService;

		public TaskApplicationService(IMapper mapper , ITaskService taskService)
		{
			_mapper = mapper;
			_taskService = taskService;
		}
		[AbpAuthorize(PermissionNames.Manager)]
		public async Task<Models.Task> AssignTask(int userId, int taskId)
		{
			if (userId == 0)
				throw new ArgumentNullException(nameof(userId));
			if (taskId == 0)
				throw new ArgumentNullException(nameof(taskId));
			return await _taskService.AssignTask(userId, taskId);
		}
		[AbpAuthorize(PermissionNames.Manager)]

		public async Task<TaskDto> Create(TaskDto model)
		{
			if (!PermissionChecker.IsGranted("Manager"))
			{
				throw new AbpAuthorizationException("You are not authorized to create user!");
			}
			var task = _mapper.Map<TaskDto, Models.Task>(model);
			var newTask = await _taskService.Create(task);
			return  _mapper.Map<Models.Task, TaskDto>(newTask);

		}
		[AbpAuthorize(PermissionNames.Manager)]
		public void Delete(int id)
		{
			_taskService.Delete(id);
		}
		[AbpAuthorize(PermissionNames.Employee)]

		public async Task<Models.Task> EditTaskStatus(TaskStatusDto model)
		{
			if(model.UserId == 0) 
				throw new ArgumentNullException(nameof(model.UserId));
			if(model.TaskId == 0) 
				throw new ArgumentNullException(nameof(model.TaskId));
			if(model.Status == null) 
				throw new ArgumentNullException(nameof(model.Status));
			return await _taskService.EditTaskStatus(model.UserId, model.TaskId, model.Status);
		}

		public async Task<IEnumerable<TaskDto>> GetAll()
		{
			var tasks = await _taskService.GetAll();
			return _mapper.Map<List<Models.Task>, List<TaskDto>>(tasks.ToList());

		}
		[AbpAuthorize(PermissionNames.Manager)]

		public IEnumerable<TaskDto> GetByEmployeeTaskType(TaskType type)
		{
			if (type == null) throw new ArgumentNullException(nameof(type));

			var tasks = _taskService.GetByEmployeeTaskType(type); 
			return _mapper.Map<List<Models.Task>, List<TaskDto>>(tasks.ToList());
		}
		[AbpAuthorize(PermissionNames.Manager)]

		public void Update(TaskDto model)
		{
			var task = _mapper.Map<TaskDto, Models.Task>(model);

			_taskService.Update(task);
		}
	}
}

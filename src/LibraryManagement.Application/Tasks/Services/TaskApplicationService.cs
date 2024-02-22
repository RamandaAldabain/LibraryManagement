using Abp.Application.Services;
using Abp.Authorization;
using Abp.Logging;
using Abp.Runtime.Security;
using AutoMapper;
using Castle.Core.Logging;
using LibraryManagement.Authorization;
using LibraryManagement.Models.LookUps;
using LibraryManagement.Tasks.Dto;
using LibraryManagement.Tasks.Interfaces;
using LibraryManagement.Users.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.Tasks.Services
{
	[AbpAuthorize]

	public class TaskApplicationService : ApplicationService, ITaskApplicationService
	{
		private readonly IMapper _mapper;
		private readonly ITaskService _taskService;
		private readonly IHttpContextAccessor _accessor;
		private readonly ILogger<TaskApplicationService> _logger;

		public TaskApplicationService(IMapper mapper, ITaskService taskService, IHttpContextAccessor accessor, ILogger<TaskApplicationService> logger)
		{
			_mapper = mapper;
			_taskService = taskService;
			_accessor = accessor;
			_logger = logger;
		}

		[HttpPost]
		[Route("api/AssignTask")]
		[AbpAuthorize(PermissionNames.Manager)]
		public async Task<Entities.Task> AssignTask(long userId, long taskId)
		{
			if (userId == 0)
				throw new ArgumentNullException(nameof(userId));
			if (taskId == 0)
				throw new ArgumentNullException(nameof(taskId));
			try
			{
				Logger.Info("Assign Task");
				Logger.Log(LogSeverity.Info, _accessor.HttpContext?.User.Identity.GetUserId()?.ToString() ?? string.Empty);
				return await _taskService.AssignTask(userId, taskId);
			}
			catch (Exception ex)
			{
				Logger.Error(ex.Message);
				throw new Exception(ex.Message);
			}
		}

		[HttpPost]
		[Route("api/CreateTask")]
		[AbpAuthorize(PermissionNames.Manager)]
		public async Task<TaskDto> Create(TaskDto model)
		{
			try
			{
				Logger.Info("Create Task");
				Logger.Log(LogSeverity.Info, _accessor.HttpContext?.User.Identity.GetUserId()?.ToString() ?? string.Empty);
				var task = _mapper.Map<TaskDto, Entities.Task>(model);
				var newTask = await _taskService.Create(task);
				return _mapper.Map<Entities.Task, TaskDto>(newTask);

			}
			catch (Exception ex)
			{
				Logger.Error(ex.Message);
				throw new Exception(ex.Message);
			}


		}

		[HttpDelete]
		[Route("api/DeleteTask")]
		[AbpAuthorize(PermissionNames.Manager)]
		public void Delete(int id)
		{
			try
			{
				Logger.Info("Delete Task");
				Logger.Log(LogSeverity.Info, _accessor.HttpContext?.User.Identity.GetUserId()?.ToString() ?? string.Empty);
				_taskService.Delete(id);
			}
			catch (Exception ex)
			{
				Logger.Error(ex.Message);
				throw new Exception(ex.Message);
			}
		}
		[AbpAuthorize(PermissionNames.Employee , PermissionNames.Manager)]
		[Route("api/EditTaskStatus")]
		[HttpPut]
		public async Task<Entities.Task> EditTaskStatus(TaskStatusDto model)
		{
			if (model.UserId == 0)
				throw new ArgumentNullException(nameof(model.UserId));
			if (model.TaskId == 0)
				throw new ArgumentNullException(nameof(model.TaskId));
			if (model.StatusId == null)
				throw new ArgumentNullException(nameof(model.StatusId));

			try
			{
				Logger.Info("Edit Task Status");
				Logger.Log(LogSeverity.Info, _accessor.HttpContext?.User.Identity.GetUserId()?.ToString() ?? string.Empty);
				return await _taskService.EditTaskStatus(model.UserId, model.TaskId, model.StatusId);

			}
			catch (Exception ex)
			{
				Logger.Error(ex.Message);
				throw new Exception(ex.Message);
			}
		}
		[HttpGet]
		[Route("api/GetAll")]
		public async Task<IList<TaskDto>> GetAll()
		{
			try
			{
				Logger.Info("Get All Tasks");
				Logger.Log(LogSeverity.Info, _accessor.HttpContext?.User.Identity.GetUserId()?.ToString() ?? string.Empty);
				var tasks = await _taskService.GetAll();
				return _mapper.Map<List<Entities.Task>, List<TaskDto>>(tasks.ToList());
			}
			catch (Exception ex)
			{
				Logger.Error(ex.Message);
				throw new Exception(ex.Message);
			}


		}
		[HttpGet]
		[Route("api/GetByEmployeeTaskType")]
		[AbpAuthorize(PermissionNames.Manager)]

		public IList<TaskDto> GetByEmployeeTaskType(long TypeId)
		{
			if (TypeId == null) throw new ArgumentNullException(nameof(TypeId));

			try
			{
				Logger.Info("Get By Employee Task Type");
				Logger.Log(LogSeverity.Info, _accessor.HttpContext?.User.Identity.GetUserId()?.ToString() ?? string.Empty);
				var tasks = _taskService.GetByEmployeeTaskType(TypeId);
				if (tasks == null) throw new Exception("No Tasks for this type");
				return _mapper.Map<List<Entities.Task>, List<TaskDto>>(tasks.ToList());
			}
			catch (Exception ex)
			{
				Logger.Error(ex.Message);
				throw new Exception(ex.Message);
			}


		}
		[HttpGet]
		[Route("api/GetTaskByDeadlineDate")]
		[AbpAuthorize(PermissionNames.Manager)]
		public IList<TaskDto> GetTaskByDeadlineDate(DateTime from, DateTime to)
		{
			try
			{
				Logger.Info("Get Task By Deadline Date");
				Logger.Log(LogSeverity.Info, _accessor.HttpContext?.User.Identity.GetUserId()?.ToString() ?? string.Empty);
				var tasks = _taskService.GetTaskByDeadlineDate(from, to);
				if (tasks == null) throw new Exception("No Tasks for this day");
				return _mapper.Map<List<Entities.Task>, List<TaskDto>>(tasks.ToList());
			}
			catch (Exception ex)
			{
				Logger.Error(ex.Message);
				throw new Exception(ex.Message);
			}


		}
		[HttpPut]
		[Route("api/UpdateTask")]
		[AbpAuthorize(PermissionNames.Manager)]
		public void Update(TaskDto model)
		{
			try
			{
				Logger.Info("Updating Task");
				Logger.Log(LogSeverity.Info, _accessor.HttpContext?.User.Identity.GetUserId()?.ToString() ?? string.Empty);
				var task = _mapper.Map<TaskDto, Entities.Task>(model);
				_taskService.Update(task);

			}
			catch (Exception ex)
			{
				Logger.Error(ex.Message);
				throw new Exception(ex.Message);
			}
		}
	}
}
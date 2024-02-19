using Abp.Application.Services;
using Abp.Authorization;
using AutoMapper;
using LibraryManagement.Authorization;
using LibraryManagement.Models.LookUps;
using LibraryManagement.Tasks.Dto;
using LibraryManagement.Tasks.Interfaces;
using LibraryManagement.Users.Dto;
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

        public TaskApplicationService(IMapper mapper, ITaskService taskService)
        {
            _mapper = mapper;
            _taskService = taskService;
        }
        [AbpAuthorize(PermissionNames.Manager)]
        public async Task<Entities.Task> AssignTask(long userId, long taskId)
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
            var task = _mapper.Map<TaskDto, Entities.Task>(model);
            var newTask = await _taskService.Create(task);
            return _mapper.Map<Entities.Task, TaskDto>(newTask);

        }
        [AbpAuthorize(PermissionNames.Manager)]
        public void Delete(int id)
        {
            _taskService.Delete(id);
        }
        [AbpAuthorize(PermissionNames.Employee)]

        public async Task<Entities.Task> EditTaskStatus(TaskStatusDto model)
        {
            if (model.UserId == 0)
                throw new ArgumentNullException(nameof(model.UserId));
            if (model.TaskId == 0)
                throw new ArgumentNullException(nameof(model.TaskId));
            if (model.Status == null)
                throw new ArgumentNullException(nameof(model.Status));
            return await _taskService.EditTaskStatus(model.UserId, model.TaskId, model.Status);
        }

        public async Task<IList<TaskDto>> GetAll()
        {
            var tasks = await _taskService.GetAll();
            if (tasks == null) throw new Exception("No Tasks");

            return _mapper.Map<List<Entities.Task>, List<TaskDto>>(tasks.ToList());

        }
        [AbpAuthorize(PermissionNames.Manager)]

        public IList<TaskDto> GetByEmployeeTaskType(TaskType type)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));

            var tasks = _taskService.GetByEmployeeTaskType(type);
            if (tasks == null) throw new Exception("No Tasks for this type");

            return _mapper.Map<List<Entities.Task>, List<TaskDto>>(tasks.ToList());
        }

        [AbpAuthorize(PermissionNames.Manager)]
        public IList<TaskDto> GetTaskByDeadlineDate(DateTime from, DateTime to)
        {
            var tasks = _taskService.GetTaskByDeadlineDate(from, to);
            if (tasks == null) throw new Exception("No Tasks for this day");
            return _mapper.Map<List<Entities.Task>, List<TaskDto>>(tasks.ToList());
        }

        [AbpAuthorize(PermissionNames.Manager)]

        public void Update(TaskDto model)
        {
            var task = _mapper.Map<TaskDto, Entities.Task>(model);

            _taskService.Update(task);
        }
    }
}

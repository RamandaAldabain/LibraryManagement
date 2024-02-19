using Abp.Application.Services;
using LibraryManagement.Models.LookUps;
using LibraryManagement.Tasks.Dto;
using LibraryManagement.Users.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryManagement.Tasks.Interfaces
{
    public interface ITaskApplicationService : IApplicationService
    {
        public Task<TaskDto> Create(TaskDto model);
        public void Update(TaskDto model);
        public void Delete(int id);
        public Task<IList<TaskDto>> GetAll();
        public IList<TaskDto> GetByEmployeeTaskType(TaskType type);
        public IList<TaskDto> GetTaskByDeadlineDate(DateTime from, DateTime to);
        public Task<Entities.Task> AssignTask(long userId, long taskId);
        public Task<Entities.Task> EditTaskStatus(TaskStatusDto model);
    }
}

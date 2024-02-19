using Abp.Domain.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LibraryManagement.Models.LookUps;

namespace LibraryManagement.Tasks.Interfaces
{
    public interface ITaskService : IDomainService
    {
        public Task<Entities.Task> Create(Entities.Task task);
        public void Update(Entities.Task task);
        public void Delete(int id);
        public Task<IList<Entities.Task>> GetAll();
        public IList<Entities.Task> GetByEmployeeTaskType(TaskType name);
        public IList<Entities.Task> GetTaskByDeadlineDate(DateTime from, DateTime to);

        public Task<Entities.Task> AssignTask(long userId, long taskId);
        public Task<Entities.Task> EditTaskStatus(long userId, long taskId, Status status);

    }
}

using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.Runtime.Session;
using LibraryManagement.Authorization.Roles;
using LibraryManagement.Authorization.Users;
using LibraryManagement.Models.LookUps;
using LibraryManagement.Tasks.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.Tasks.Services
{
    public class TaskService : DomainService, ITaskService
    {
        private readonly IRepository<Entities.Task, long> _taskRepository;
        private readonly UserManager _userManager;
        private readonly RoleManager _roleManager;
        public IAbpSession _AbpSession { get; set; }




        public TaskService(IRepository<Entities.Task, long> taskRepository, UserManager userManager, RoleManager roleManager, IAbpSession AbpSession)
        {
            _taskRepository = taskRepository;
            _userManager = userManager;
            _AbpSession = AbpSession;
            _roleManager = roleManager;

        }
        public async Task<Entities.Task> Create(Entities.Task model)
        {
            return await _taskRepository.InsertAsync(model);

        }

        public void Delete(int id)
        {
            _taskRepository.Delete(id);
        }

        public async Task<IList<Entities.Task>> GetAll()
        {
            var user = await _userManager.GetUserByIdAsync((long)_AbpSession.UserId);
            if (user != null)
            {
                var roles = await _userManager.GetRolesAsync(user);

                if (roles.Contains("Manager"))
                {
                    var manageRole = await _roleManager.GetRoleByNameAsync("Manager");
                    if (manageRole != null)
                    {
                        var managerTasks = _taskRepository.GetAll().ToList();
                        return managerTasks;
                    }
                }

                var employeeTasks = new List<Entities.Task>();
                if (roles.Contains("Shelver"))
                {
                    var shelverRole = await _roleManager.GetRoleByNameAsync("Shelver");
                    if (shelverRole != null)
                    {
                        var shelverTasks = _taskRepository.GetAll().Where(i => i.TaskType.Name == "Shelver").ToList();
                        if (shelverTasks != null && shelverTasks.Any())
                        {
                            employeeTasks.AddRange(shelverTasks);
                        }
                        else
                        {

                            throw new Exception("No tasks found for the shelver role.");
                        }
                    }
                }
                if (roles.Contains("Accountant"))
                {
                    var accountantRole = await _roleManager.GetRoleByNameAsync("Accountant");
                    if (accountantRole != null)
                    {
                        var accountantTasks = await _taskRepository.GetAll().Where(i => i.TaskType.Name == "").ToListAsync();
                        if (accountantTasks != null && accountantTasks.Any())
                        {
                            employeeTasks.AddRange(accountantTasks);
                        }
                        else
                        {

                            throw new Exception("No tasks found for the accountant role.");
                        }
                    }
                }
                return employeeTasks;
            }

            return null;
        }

        public IList<Entities.Task> GetByEmployeeTaskType(TaskType name)
        {
            return _taskRepository.GetAll().Where(x => x.TaskType == name).ToList();
        }

        public async void Update(Entities.Task task)
        {
            await _taskRepository.UpdateAsync(task);
        }

        public async Task<Entities.Task> AssignTask(long userId, long taskId)
        {
            var user = await _userManager.GetUserByIdAsync(userId);
            var task = await _taskRepository.GetAsync(taskId);
            var roles = await _userManager.GetRolesAsync(user);
            //if ((roles.Any(r=>r.=="Shelver" && task.TaskType == TaskType.Accountant) ||
            //   (roles.Contains(roles.FirstOrDefault().Name && task.TaskType == TaskType.Shelver))
            //{

            //    task.AssigneeId = userId;
            //   // task.Status = Models.LookUps.Status.Assigned;
            //    _taskRepository.Update(task);
            //    UnitOfWorkManager.Current.SaveChanges();
            //    user.AssignedTasks.Add(task);
            //    await _userManager.UpdateAsync(user);
            //    return task;

            //}
            //else { throw new Exception("Task cant be assigned to this user"); }
             return task;
        }

        public async Task<Entities.Task> EditTaskStatus(long userId, long taskId, Status status)
        {
            var user = await _userManager.GetUserByIdAsync(userId);
            var task = await _taskRepository.GetAsync(taskId);
            var roles = await _userManager.GetRolesAsync(user);
            if (!user.AssignedTasks.Any(t => t.Id == taskId))
                throw new Exception("Not allowed,Your not assigned to this task");
            task.Status = status;
            await _taskRepository.UpdateAsync(task);
            return task;

        }

        public IList<Entities.Task> GetTaskByDeadlineDate(DateTime from, DateTime to)
        {
            return _taskRepository.GetAll().Where(task => task.Deadline.Date >= from.Date && task.Deadline.Date <= to.Date).ToList();

        }
    }

}


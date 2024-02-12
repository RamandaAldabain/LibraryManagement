using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.Runtime.Session;
using LibraryManagement.Authorization.Roles;
using LibraryManagement.Authorization.Users;
using LibraryManagement.Models;
using LibraryManagement.Models.enums;
using LibraryManagement.Models.LookUps;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.DomainServices
{
	public class TaskService : DomainService, ITaskService
	{
		private readonly IRepository<Models.Task> _taskRepository;
		private readonly UserManager _userManager;
		private readonly RoleManager _roleManager;
		public IAbpSession _AbpSession { get; set; }




		public TaskService(IRepository<Models.Task> taskRepository, UserManager userManager, RoleManager roleManager, IAbpSession AbpSession)
		{
			_taskRepository = taskRepository;
			_userManager = userManager;
			_AbpSession = AbpSession;
			_roleManager = roleManager;

		}
		public async Task<Models.Task> Create(Models.Task model)
		{
			return await _taskRepository.InsertAsync(model);

		}

		public void Delete(int id)
		{
			_taskRepository.Delete(id);
		}

		public async Task<IEnumerable<Models.Task>> GetAll()
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
						var managerTasks = await _taskRepository.GetAll().Where(i => i.TaskType.ToString() == manageRole.Name).ToListAsync();
						return managerTasks;
					}
				}
				else if (roles.Contains("Employee"))
				{
					var employeeRole = await _roleManager.GetRoleByNameAsync("Employee");
					if (employeeRole != null)
					{
						var employeeTasks = new List<Models.Task>();
						if (roles.Contains("Shelver"))
						{
							var shelverRole = await _roleManager.GetRoleByNameAsync("Shelver");
							if (shelverRole != null)
							{
								var shelverTasks = await _taskRepository.GetAll().Where(i => i.TaskType.ToString() == shelverRole.Name).ToListAsync();
								employeeTasks.AddRange(shelverTasks);
							}
						}
						if (roles.Contains("Accountant"))
						{
							var accountantRole = await _roleManager.GetRoleByNameAsync("Accountant");
							if (accountantRole != null)
							{
								var accountantTasks = await _taskRepository.GetAll().Where(i => i.TaskType.ToString() == accountantRole.Name).ToListAsync();
								employeeTasks.AddRange(accountantTasks);
							}
						}
						return employeeTasks;
					}
				}
			}
			return null;
		}

		public IEnumerable<Models.Task> GetByRole(string name)
		{
			return _taskRepository.GetAll().Where(x => x.TaskType.ToString() == name);
		}

		public async void Update(Models.Task task)
		{
			await _taskRepository.UpdateAsync(task);
		}

		public async Task<Models.Task> AssignTask(int userId, int taskId)
		{
			var user = await _userManager.GetUserByIdAsync(userId);
			var task =await _taskRepository.GetAsync(taskId);
			var roles = await _userManager.GetRolesAsync(user);
			if ((roles.Contains(TaskType.Accountant.ToString()) && task.TaskType == TaskType.Accountant) ||
		       (roles.Contains(TaskType.Shelver.ToString()) && task.TaskType == TaskType.Shelver))
			{
				task.AssigneeUserId = userId;
				task.Status = Models.LookUps.Status.Assigned;
				user.AssignedTasks.Add(task);
				return  task;
			}
			throw new Exception("Task cant be assigned to this user");
		}

		public async Task<Models.Task> EditTaskStatus(int userId, int taskId, Status status)
		{
			var user = await _userManager.GetUserByIdAsync(userId);
			var task = await _taskRepository.GetAsync(taskId);
			var roles = await _userManager.GetRolesAsync(user);
			if (!user.AssignedTasks.Any(t => t.Id == taskId))
				throw new Exception("Not allowed,Your not assigned to this task");
			task.Status = status;
			return task;
						
		}
	}

}


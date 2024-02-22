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
			var user =await _userManager.GetUserByIdAsync(model.AssigneeId);
			var task=  await _taskRepository.InsertAsync(model);
			if (task != null)
			{
			 user.AssignedTasks.Add(task);
				UnitOfWorkManager.Current.SaveChanges();
			 
			}
			return task;
		}

		public void Delete(int id)
		{
			_taskRepository.Delete(id);
		}

		public async Task<IList<Entities.Task>> GetAll()
		{
			var user = await _userManager.GetUserByIdAsync((long)_AbpSession.UserId);
			var employeeTasks = new List<Entities.Task>();

			if (user != null)
			{
				var roles = await _userManager.GetRolesAsync(user);


				switch (roles.FirstOrDefault())
				{
					case "Manager":

						var managerTasks = _taskRepository.GetAll().ToList();
						return managerTasks;

						break;
					case "Shelver":

						var shelverTasks = _taskRepository.GetAll().Where(i => i.TaskType.Name == "Shelver").ToList();
						if (shelverTasks != null && shelverTasks.Any())
						{
							employeeTasks.AddRange(shelverTasks);
						}
						else
						{
							throw new Exception("No tasks found for the shelver role.");
						}

						break;
					case "Accountant":

						var accountantTasks = await _taskRepository.GetAll().Where(i => i.TaskType.Name == "").ToListAsync();
						if (accountantTasks != null && accountantTasks.Any())
						{
							employeeTasks.AddRange(accountantTasks);
						}
						else
						{
							throw new Exception("No tasks found for the accountant role.");
						}

						break;
				}

			}

			return employeeTasks;
		}

		public IList<Entities.Task> GetByEmployeeTaskType(long TypeId)
		{
			var tasks = _taskRepository.GetAll().Where(x => x.TaskType.Id == TypeId).ToList();
			if (tasks.Any())
				return tasks;
			else
				return null;

		}

		public async void Update(Entities.Task task)
		{
			await _taskRepository.UpdateAsync(task);
		}

		public async Task<Entities.Task> AssignTask(long userId, long taskId)
		{
			var task = await _taskRepository.GetAsync(taskId);
			if (task == null) throw new Exception("Task not found");
			var user = await _userManager.GetUserByIdAsync(userId);
			if (user == null) throw new Exception("User not found");
			var roles = await _userManager.GetRolesAsync(user);
			if (roles.Any())
			{
				if ((roles.FirstOrDefault() == "Shelver" && task.TaskType.Name == "Shelver")
				|| (roles.FirstOrDefault() == "Accountant" && task.TaskType.Name == "Accountant"))
				{
					task.AssigneeId = userId;
					user.AssignedTasks.Add(task);
					await _taskRepository.UpdateAsync(task);
					await _userManager.UpdateAsync(user);
					return task;
				}
				else
				{
					throw new Exception("Task cant be assigned to this user");

				}
			}
			else
			{
				throw new Exception("User has no roles");

			}
		}

		public async Task<Entities.Task> EditTaskStatus(long userId, long taskId, long statusId)
		{	
			var task = await _taskRepository.GetAsync(taskId);
			if(task == null) throw new Exception("Task Not Found");

			var user = await _userManager.GetUserByIdAsync(userId);
			if (user == null) throw new Exception("user Not Found");


			var roles = await _userManager.GetRolesAsync(user);
			if (!roles.Any()) throw new Exception("user has no roles");


			if (!user.AssignedTasks.Any(t => t.Id == taskId))
				throw new Exception("Not allowed,Your not assigned to this task");

			task.StatusId = statusId;
			await _taskRepository.UpdateAsync(task);
			return task;
				
		}

		public IList<Entities.Task> GetTaskByDeadlineDate(DateTime from, DateTime to)
		{
			var tasks = _taskRepository.GetAll().Where(task => task.Deadline.Date >= from.Date && task.Deadline.Date <= to.Date).ToList();
			if (tasks.Any())
				return tasks;
			else
				return null;
		}
	}

}


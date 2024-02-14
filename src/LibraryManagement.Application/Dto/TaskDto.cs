using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Runtime.Validation;
using LibraryManagement.Authorization.Roles;
using LibraryManagement.Models.enums;
using LibraryManagement.Models.LookUps;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Dto
{
	public class TaskDto : ICustomValidate
	{
		public int Id { get; set; }
		[Required]
		public string Name { get; set; }
		[Required]

		public string Description { get; set; }
		[Required]

		public DateTime Deadline { get; set; }
		[Required]

		public Status Status { get; set; }
		[Required]
		public long CreatorUserId { get; set; }

		public long AssigneeUserId { get; set; }
		public TaskType TaskType { get; set; }


		public void AddValidationErrors(CustomValidationContext context)
		{
			using (var scope = context.IocResolver.CreateScope())
			{
				using (var uow = scope.Resolve<IUnitOfWorkManager>().Begin())
				{
					var taskRepository = scope.Resolve<IRepository<Models.Task, int>>();

					var nameExists = taskRepository.GetAll()
						.Where(s => s.Name == Name)
						.Any();

					if (nameExists)
					{
						var errorMessage = "A task with the same name already exists";
						var memberNames = new[] { nameof(Name) };
						context.Results.Add(new ValidationResult(errorMessage, memberNames));
					}

					uow.Complete();
				}
			}
		}
	}
}

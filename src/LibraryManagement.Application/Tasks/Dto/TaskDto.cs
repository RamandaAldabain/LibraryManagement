using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Runtime.Validation;
using LibraryManagement.Models.LookUps;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace LibraryManagement.Tasks.Dto
{
	public class TaskDto : ICustomValidate
	{
		public long Id { get; set; }
		[Required]
		public string Name { get; set; }
		[Required]

		public string Description { get; set; }
		[Required]

		public DateTime Deadline { get; set; }

		[Required]
		public long TaskTypeId { get; set; }

		[Required]
		public long StatusId { get; set; }
		[Required]
		public long CreatorId { get; set; }

		public long AssigneeId { get; set; }


		public void AddValidationErrors(CustomValidationContext context)
		{
			using (var scope = context.IocResolver.CreateScope())
			{
				using (var uow = scope.Resolve<IUnitOfWorkManager>().Begin())
				{
					var taskRepository = scope.Resolve<IRepository<Entities.Task, long>>();

					var nameExists = taskRepository.GetAll()
						.Where(s => s.Name == Name && s.Id != Id)
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

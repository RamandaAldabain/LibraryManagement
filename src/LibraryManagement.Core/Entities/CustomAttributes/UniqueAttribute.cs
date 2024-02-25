using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace LibraryManagement.Entities.CustomAttributes8
{
	public class UniqueAttribute : ValidationAttribute
	{
		private readonly IRepository<Entities.Task, long> _taskRepository;
		public UniqueAttribute(IRepository<Entities.Task, long> taskRepository)
		{
			_taskRepository = taskRepository;
		}

		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			if (value == null)
			{
				return ValidationResult.Success;
			}
			var task = _taskRepository.GetAll().Where(i => i.Name == value).FirstOrDefault();

			if (task != null)
			{
				return ValidationResult.Success;
			}
			return new ValidationResult("A task with the same name already exists");

		}
	}
}
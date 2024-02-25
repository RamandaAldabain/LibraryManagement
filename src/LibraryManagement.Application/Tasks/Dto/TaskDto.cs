using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Runtime.Validation;
using LibraryManagement.Entities.CustomAttributes;
using LibraryManagement.Models.LookUps;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace LibraryManagement.Tasks.Dto
{
	public class TaskDto 
		{
		public long Id { get; set; }
		[Required]
		[Unique]
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


	}
}

using Abp.Domain.Entities.Auditing;
using LibraryManagement.Authorization.Users;
using LibraryManagement.Models.LookUps;
using System;


namespace LibraryManagement.Entities
{

	public class Task : FullAuditedEntity<long>
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public DateTime Deadline { get; set; }

		//Relations
		public long CreatorId { get; set; }
		public User Creator { get; set; }

		public long AssigneeId { get; set; }
		public User Assignee { get; set; }

		public long StatusId { get; set; }
		public Status Status { get; set; }
		public long TaskTypeId { get; set; }
		public TaskType TaskType { get; set; }
	}
}

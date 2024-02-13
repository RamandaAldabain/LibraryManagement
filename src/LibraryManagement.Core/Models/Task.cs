using Abp.Domain.Entities.Auditing;
using LibraryManagement.Authorization.Roles;
using LibraryManagement.Authorization.Users;
using LibraryManagement.Models.enums;
using LibraryManagement.Models.LookUps;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Models
{

	public class Task : FullAuditedEntity
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public DateTime Deadline { get; set; }
		public Status Status { get; set; }
		[ForeignKey("CreatorUserId")]
		public long CreatorUserId { get; set; }
		[ForeignKey("AssigneeUserId")]

		public long AssigneeUserId { get; set; }
		public TaskType TaskType { get; set; }
	}
}

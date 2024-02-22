using LibraryManagement.Models.LookUps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Users.Dto
{
	public class TaskStatusDto
	{
		public int Id { get; set; }
		public int TaskId { get; set; }
		public int UserId { get; set; }
		public long StatusId { get; set; }
	}
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CustomIdentity.Models
{
	public class Job
	{
		[Key]
		public Guid Id { get; set; }

		[Required]
		[Display(Name = "Tên công việc")]
		public string Name { get; set; }

		public string Note { get; set; }

		[Display(Name = "Trạng thái")]
		public JobStatus JobStatus { get; set; }

		[Display(Name = "Ngày bắt đầu")]
		public DateTime DateStart { get; set; }

		[Display(Name = "Ngày hoàn thành")]
		public DateTime DeadLine { get; set; }

		[Display(Name = "Người thực hiện")]
		public string AssignTo { get; set; }

		public string UserId { get; set; }
		public ApplicationUser ApplicationUser { get; set; }

	}

	public enum JobStatus {
		Open = 0,
		Progress = 1,
		Review = 2,
		Done = 3
	}


}

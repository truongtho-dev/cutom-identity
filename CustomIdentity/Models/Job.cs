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

		[Display(Name = "Job name")]
		public string Name { get; set; }
		public string Note { get; set; }
		[Display(Name = "Job status")]
		public JobStatus JobStatus { get; set; }
		[Display(Name = "Date start")]
		public DateTime DateStart { get; set; }

		[Display(Name = "Date end")]
		public DateTime DeadLine { get; set; }

		[Display(Name = "Assign to")]
		public string CurrentApplicationUserId {get; set;}
		public ApplicationUser ApplicationUser { get; set; }


	}

	public enum JobStatus {
		Open = 0,
		Progress = 1,
		Review = 2,
		Done = 3
	}


}

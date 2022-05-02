using CustomIdentity.Data;
using CustomIdentity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomIdentity.Controllers
{
	public class JobController : Controller
	{
		private readonly ApplicationDbContext _context;
		private readonly UserManager<ApplicationUser> _userManager;

		public JobController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
		{
			_context = context;
			_userManager = userManager;
		}
		public async Task<IActionResult> Index()
		{
			var result = await _context.Jobs.ToListAsync();

			return View(result);
		}

		// Job/Create
		public async Task<IActionResult> Create()
		{
			var users = await _userManager.Users.ToListAsync();

			var usersFilter = User.IsInRole("Admin");
			var selectOptions = users.Select(user => new SelectListItem()
			{
				Value = user.Id.ToString(),
				Text = $"{user.FirstName} {user.LastName}"
			}).ToList();
			ViewBag.SelectOptions = selectOptions;
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create ([Bind("Id, Name, Note, JobStatus, DateStart, DeadLine, CurrentApplicationUserId")] Job job)
		{
			if (ModelState.IsValid)
			{
				job.Id = Guid.NewGuid();
				await _context.AddAsync(job);
				await _context.SaveChangesAsync();
			}
			else
			{
				throw new Exception("Job is invalid");
			}
			return View(job);
		}
	}
}

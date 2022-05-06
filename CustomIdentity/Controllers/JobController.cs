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
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authorization;

namespace CustomIdentity.Controllers
{
	[Authorize]
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
			var selectOptions = await SelectAssignTo();
			ViewBag.SelectOptions = selectOptions;
			return View();
		}

		public async Task<List<SelectListItem>> SelectAssignTo()
		{
			var users = await _userManager.Users.ToListAsync();
			var selectOptions = users.Select(user => new SelectListItem()
			{
				Value = $"{user.FirstName} {user.LastName}",
				Text = $"{user.FirstName} {user.LastName}"
			}).ToList();

			return selectOptions;
		}

		[HttpPost]
		public async Task<IActionResult> Create ([Bind("Id, Name, Note, JobStatus, DateStart, DeadLine, AssignTo")] Job job)
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
			return RedirectToAction("Index");
		}

		// GET: Job/Delete/5
		[HttpGet("[controller]/delete/{jobId}")]
		public async Task<IActionResult> Delete (Guid jobId)
		{
			var job = await _context.Jobs.FirstOrDefaultAsync(j => j.Id == jobId);

			if (job == null) return NotFound();
			return View(job);
		}

		// POST: Job/Delete/5
		[HttpPost("[controller]/delete/{jobId}"), ActionName("Delete")]
		public async Task<IActionResult> DeleteConfirmed (Guid jobId)
		{
			var job = await _context.Jobs.FindAsync(jobId);
			_context.Jobs.Remove(job);
			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		// GET: Job/Edit/5
		//[HttpGet("[controller]/edit/{jobId}")]
		public async Task<IActionResult> Edit (Guid id)
		{
			if (id == null) return NotFound();
			var job = await _context.Jobs.FindAsync(id);
			if (job == null) return NotFound();
			var selectOptions = await SelectAssignTo();
			ViewBag.SelectOptions = selectOptions;
			return View(job);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(Guid id, [Bind ("Id, Name, Note, JobStatus, DateStart, DeadLine, AssignTo")] Job job)
		{
			if (id != job.Id) return NotFound();
			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(job);
					await _context.SaveChangesAsync();
				}
				catch (Exception err)
				{

					throw new Exception(err.Message);
				}
				return RedirectToAction(nameof(Index));
			}
			return View(job);
		}

		// GET: Movies/Details/5
		public async Task<IActionResult> Details(Guid id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var job = await _context.Jobs
				.FirstOrDefaultAsync(j => j.Id == id);
			if (job == null)
			{
				return NotFound();
			}

			return View(job);
		}

	}
}

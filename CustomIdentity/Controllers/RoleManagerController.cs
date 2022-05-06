using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomIdentity.Controllers
{
	[Authorize(Roles = "Admin")]
	public class RoleManagerController : Controller
	{
		private readonly RoleManager<IdentityRole> _roleManager;

		public RoleManagerController(RoleManager<IdentityRole> roleManager)
		{
			_roleManager = roleManager;
		}
		public async Task<IActionResult> Index()
		{
			var roles = await _roleManager.Roles.ToListAsync();

			return View(roles);
		}


		[HttpPost]
		public async Task<IActionResult> AddRole(string roleName)
		{
			if (!String.IsNullOrEmpty(roleName))
			{
				await _roleManager.CreateAsync(new IdentityRole(roleName.Trim()));
			}
			return RedirectToAction("Index");
		}


		// GET: RoleManager/Delete/sdfst
		[HttpGet("[controller]/delete/{id}")]
		public async Task<IActionResult> Delete(string id)
		{
			var role = await _roleManager.FindByIdAsync(id);
			return View(role);
		}

		[HttpPost, ActionName("Delete")]
		public async Task<IActionResult> DeleteConfirmed(string id)
		{
			var role = await _roleManager.FindByIdAsync(id);
			if(role.Name != "Admin") await _roleManager.DeleteAsync(role);
			return RedirectToAction(nameof(Index));
		}

		// GET: RoleManager/Edit/kjkjsdsdjs
		[HttpGet("[controller]/edit/{id}")]
		public async Task<IActionResult> Edit(string id)
		{
			if (id == null) return NotFound();
			var role = await _roleManager.FindByIdAsync(id);
			if (role == null) return NotFound();
			return View(role);
		}

		[HttpPost("[controller]/edit/{id}")]
		public async Task<IActionResult> Edit (string id, [Bind("Id, Name")] IdentityRole role)
		{
			if (id != role.Id) return NotFound();
			if(ModelState.IsValid)
			{
				try
				{
					await _roleManager.UpdateAsync(role);
					return RedirectToAction(nameof(Index));

				}
				catch (Exception err)
				{
					throw new Exception(err.Message);
				}
				
			}
			return View(role);
		}

	}
}

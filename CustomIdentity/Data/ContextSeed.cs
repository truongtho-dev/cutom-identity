using CustomIdentity.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomIdentity.Data
{
	public static class ContextSeed
	{
		public static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
		{
			await roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
			await roleManager.CreateAsync(new IdentityRole(Roles.Member.ToString()));
		}

		public static async Task SeedSuperAdminAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
		{
			var defaultUser = new ApplicationUser
			{
				UserName = "admin",
				Email = "admin@gmail.com",
				FirstName = "Ad",
				LastName = "Min",
				EmailConfirmed = true,
				PhoneNumberConfirmed = true
			};
			
			if(userManager.Users.All(u => u.Id != defaultUser.Id))
			{
				var user = await userManager.FindByEmailAsync(defaultUser.Email);
				if (user == null) 
				{
					await userManager.CreateAsync(defaultUser, "Password1@");
					await userManager.AddToRoleAsync(defaultUser, Roles.Member.ToString());
					await userManager.AddToRoleAsync(defaultUser, Roles.Admin.ToString());
				}
			}
		}
	}

	public enum Roles
	{
		Admin,
		Member
	}
}

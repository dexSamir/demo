using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using UniqloProject.DataAccess;
using UniqloProject.Enums;
using UniqloProject.Models;

namespace UniqloProject.Extension
{
	public static class SeedExtension
	{
		public static void UseUserSeed(this IApplicationBuilder app)
		{
			using (var scope = app.ApplicationServices.CreateScope())
			{
				var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
				var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
				if (!roleManager.Roles.Any())
				{
					foreach (var item in Enum.GetValues(typeof(Roles)))
					{
						roleManager.CreateAsync(new IdentityRole(item.ToString())).Wait();
					}
				}
                if (userManager.Users.Any(x => x.NormalizedUserName == "ADMIN"))
				{
					User user = new User
					{
                        Fullname = "admin",
                        UserName = "admin",
                        Email = "admin@gmail.com",
                        ProfileImageUrl = "photo.jpg",
                    };
					userManager.CreateAsync(user, "123").Wait();
					userManager.AddToRoleAsync(user, nameof(Roles.Admin)).Wait();
				}
			}
		}
	}
}


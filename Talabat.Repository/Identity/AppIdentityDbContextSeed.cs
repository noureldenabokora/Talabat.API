using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entites.Identity;

namespace Talabat.Repository.Identity
{
    public class AppIdentityDbContextSeed
    {
        public static async  Task SeedUserAsync(UserManager<AppUser> userManager) 
        {
            if (!userManager.Users.Any()) 
            {
                var user = new AppUser()
                {
                    DisplayName ="Nour AboKoura",
                     Email = "nourabokora@gmail.com",
                     UserName = "nour.abokoura",
                     PhoneNumber = "01110702310"
                };
                await userManager.CreateAsync(user,"Nour@88");
            }
        }

    }
}

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Talabat.Core.Entites.Identity;
using Talabat.Repository.Identity;

namespace Talabat.ApIs.Extension
{
    public static class IdentityServicesExtensions
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services,IConfiguration _configuration)
        {
            services.AddIdentity<AppUser, IdentityRole>( options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = true;

            }).AddEntityFrameworkStores<AppIdentityDbContext>();

                                      // دي عشان يحفظ عنده اسكيما معينه بحيث يعمل اتشيك علي
                                      // التوين الي جيله هل هو نفس الاسكبما ولا لا 
             services.AddAuthentication(options => 
             {
                 //عشان مش فوق كل يند بوينت هكتب اسم الاسكيما 
                 options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                 options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

             })
                .AddJwtBearer(options => 
                {
                    //هنا بحدد البارماترات اللي هعمل عليها فاليديت 
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidIssuer= _configuration["JWT:ValidIssuer"],
                        ValidateAudience = true,
                        ValidAudience = _configuration["JWT:ValidAudience"],
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"])),

                    };
                });
            
            return services;
        }

    }
}

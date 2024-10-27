
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using Talabat.ApIs.Extension;
using Talabat.ApIs.Helpers;
using Talabat.Core.Entites.Identity;
using Talabat.Core.Repositories;
using Talabat.Repository;
using Talabat.Repository.Data;
using Talabat.Repository.Identity;

namespace Talabat.ApIs
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            #region Configure Services

            builder.Services.AddControllers();//API Services
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            
            builder.Services.AddSwaggerServices();

            builder.Services.AddDbContext<StroreContext>( options => 
            {
                //Configuration here related with anything related by APP SETING
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            } );

            //allow dependecy injection to AppIdentityDbContext
            builder.Services.AddDbContext<AppIdentityDbContext>(options =>
            {
                //Configuration here related with anything related by APP SETING
                options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));
            });


            // here to alloe dI for redis
            builder.Services.AddSingleton<IConnectionMultiplexer>(s =>
            {
                //get connection string first that related by redis 
                var connection = builder.Configuration.GetConnectionString("Redis");

                //return object as when any one ask for object form conmultipeer 
                return ConnectionMultiplexer.Connect(connection);
            });


            builder.Services.AddApplicationServices();

            builder.Services.AddIdentityServices(builder.Configuration);


            #endregion

            // build app to contol for which midell ware that request throught it
            var app = builder.Build();
       
            #region Handle Async Migrations 

            var scope = app.Services.CreateScope();

            var services = scope.ServiceProvider;

            var loggerFactory = services.GetRequiredService<ILoggerFactory>();
            try
            {
                var dbcontext = services.GetRequiredService<StroreContext>();
                await dbcontext.Database.MigrateAsync();

                // to seed data 
              await  StoredContextSeed.SeedAsync(dbcontext);

                // to update database Automaticlly by run 
                var identityContext = services.GetRequiredService<AppIdentityDbContext>();
                await identityContext.Database.MigrateAsync();

                // to allow dependence injection to identity 
                var userManger = services.GetRequiredService<UserManager<AppUser>>(); 
                await AppIdentityDbContextSeed.SeedUserAsync(userManger);

            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "an error ocuerd during apply Migiration");
            }

            #endregion
           
            // Configure the HTTP request pipeline.
            //time as i devleop only  just deploy the app it's not work
            #region Configure MiddleWares

            if (app.Environment.IsDevelopment())
            {
                app.UseSwaggerMidlleWares();
            }

            app.UseStatusCodePagesWithRedirects("/errors/{0}");

            app.UseHttpsRedirection();

            app.UseAuthentication(); 
            app.UseAuthorization();
            


            app.MapControllers();

            app.Run(); 
            #endregion
        }
    }
}

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;
using PikchaWebApp.Data;
using PikchaWebApp.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System;
using Microsoft.Extensions.FileProviders;
using System.IO;

namespace PikchaWebApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<PikchaDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<PikchaUser>()
                .AddEntityFrameworkStores<PikchaDbContext>();

            services.AddIdentityServer()
                .AddApiAuthorization<PikchaUser, PikchaDbContext>();

            services.AddAuthentication()
                .AddIdentityServerJwt();

            services.AddControllersWithViews();
            services.AddRazorPages();

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseIdentityServer();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });

            app.UseStaticFiles();

            /* app.UseStaticFiles(new StaticFileOptions
             {
                 FileProvider = new PhysicalFileProvider(
             Path.Combine(Directory.GetCurrentDirectory(), "Public")),
                 RequestPath = "/Public"
             }); */

            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<PikchaDbContext>();
                context.Database.Migrate();


                try
                {
                    var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<PikchaUser>>();

                    SeedData(userManager, env, context);

                }
                catch
                {

                }


            }
        }

        private void SeedData(UserManager<PikchaUser> userManager, IWebHostEnvironment env, PikchaDbContext dbContext)
        {


            // clear images
             var imgs =  dbContext.PikchaImages.ToListAsync().Result;
            dbContext.RemoveRange(imgs);
            dbContext.SaveChanges();
          

            Random rnd = new Random();

            List<string> locations = new List<string>();
            locations.Add("Melbourne");
            locations.Add("California");
            locations.Add("France");
            locations.Add("London");
            locations.Add("Tokyo");

            List<string> imageIds = new List<string>();
            imageIds.Add("7c8ad81f-4151-4b90-b7e6-6780993818dc");
            imageIds.Add("7ad336a1-d2e4-44b6-8250-b45abbfa2674");
            imageIds.Add("4e60c9d7-5bd6-4f4f-90f2-92c2f738a27f");
            imageIds.Add("1d956190-1e93-4e9a-b910-e680291dc23f");
            imageIds.Add("01d7bc34-afae-4fa6-b3ec-8767dba1f256");
            imageIds.Add("9f44ab01-436d-4229-be6d-e1e45ac92b11");
            imageIds.Add("18e5bbff-53b3-43bd-9f79-9063f853ecf3");
            imageIds.Add("00407b73-f27b-4e70-96b2-c38834de6b91");
            imageIds.Add("3488c522-e9c4-4e99-b4f5-b6f0745be342");
            imageIds.Add("7110a629-0a3e-4a69-be0e-630e5d9f51d1");
            imageIds.Add("95285bd7-bb07-4299-aef0-3b421f3a8fee");
            imageIds.Add("1185454b-ae2b-4700-9abb-d5e6b3ee3503");
            imageIds.Add("51082402-2bd4-441f-8271-751df7d0993d");
            imageIds.Add("a2a6297b-73fe-43dc-a142-508e739e99f9");
            imageIds.Add("b50aa243-70a4-4003-8c00-16256f66b813");
            imageIds.Add("bc3a3067-ddf8-45a4-a612-1a9c16dd0e6d");
            imageIds.Add("c2a5dc7e-c973-48fe-a50c-158966fcffbf");
            imageIds.Add("cc2096d6-ed36-4446-9b02-7f42454124c4");
            imageIds.Add("d873bdb6-c994-4876-94a5-2b1f8c676642");
            imageIds.Add("daaa2209-719c-424d-88f5-2ad5be3790b3");
            imageIds.Add("f466feff-5e42-44bd-8418-3902a05e797f");

            List<PikchaUser> lstUsers = new List<PikchaUser>();
            for (int i = 1; i < 4; i++)
            {
                var user = new PikchaUser()
                {
                    UserName = "testuser" + i + "@pikcha.com",
                    Email = "testuser" + i + "@pikcha.com",
                    BioInfo = "test user" + i + " bio info",
                    FirstName = "test " + i + " FName",
                    LastName = "test " + i + " LName"
                };
                var exUsr = userManager.FindByEmailAsync(user.Email).Result;
                if(exUsr == null)
                {
                    var resl = userManager.CreateAsync(user).Result;
                    lstUsers.Add(user);
                }
                else
                {
                    lstUsers.Add(exUsr);

                }
            }


            for (int i = 0; i < imageIds.Count; i++)
            {
                string imgId = imageIds[i];
                var img = new PikchaImage()
                {
                    Artist = lstUsers[rnd.Next(lstUsers.Count)],
                    Caption = "Caption " + i,
                    Title = "Title " + i,
                    Location = locations[rnd.Next(locations.Count)],
                    Id = (uint)i +1,
                    PikchaImageId = imgId,
                    WatermarkedFile = "Uploads/Images/Watermarks/" + imgId + ".jpg",
                    ThumbnailFile = "Uploads/Images/Thumbnail/" + imgId + ".jpg",
                    UploadedAt = DateTime.Now.AddDays(-rnd.Next(20) ).AddHours(-5).AddSeconds(rnd.Next(10) * 100)

                };

                dbContext.PikchaImages.Add(img);
            }

            dbContext.SaveChanges();
        }


    }
}

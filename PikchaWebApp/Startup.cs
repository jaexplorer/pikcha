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
using AutoMapper;
using Microsoft.AspNetCore.Identity.UI.Services;
using PikchaWebApp.Drivers.Email;
using PikchaWebApp.Managers;

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

           // services.AddDefaultIdentity<PikchaUser>()  
            services.AddIdentity<PikchaUser, PikchaRole>()  
                //.AddRoles<IdentityRole>()
                //.AddRoleManager<IdentityRole>()
                // services.AddIdentity(PikchaUser, PikchaRo>()
                .AddEntityFrameworkStores<PikchaDbContext>()
                .AddDefaultTokenProviders(); ;

            services.AddIdentityServer()
                .AddApiAuthorization<PikchaUser, PikchaDbContext>();

            services.AddAuthentication()
                .AddIdentityServerJwt();

            services.AddControllersWithViews();
            services.AddRazorPages();

            /*services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            })); */

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });

            /*services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {

                        builder.WithOrigins("http://localhost.com",
                                            "http://localhost").AllowAnyHeader().AllowAnyMethod(); 
                    });

            }); */

            // Auto Mapper Configurations
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new PikchaDTOProfiles());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            // email
            services.AddTransient<IEmailSender, EmailSender>();
            services.Configure<AuthMessageSenderOptions>(Configuration);

            //services.AddIdentity<PikchaUser, PikchaRole>();
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
            app.UseCors("MyPolicy");
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
                endpoints.MapRazorPages();

                endpoints.MapControllers().RequireCors("DefaultPolicy");
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
                    
                    var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<PikchaRole>>();

                    InitDB(userManager, roleManager, context);
                    SeedData(userManager, env, context);                    
                        
                }
                catch(Exception ex)
                {

                }


            }
        }

        private void SeedData(UserManager<PikchaUser> userManager, IWebHostEnvironment env, PikchaDbContext dbContext)
        {

            

            Random rnd = new Random();

            List<string> locations = new List<string>();
            locations.Add("Melbourne, Australia");
            locations.Add("California, USA");
            locations.Add("Paris, France");
            locations.Add("London, UK");
            locations.Add("Tokyo, Japan");

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
                    LastName = "test " + i + " LName",
                    AvatarFileName = "Uploads/Avatars/Test/profile" + i + ".jpg",
                    PerCountry = locations[rnd.Next(locations.Count)]
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

                int daysSince = rnd.Next(20);
                //List<ImageView> imgViews = new List<ImageView>();

                

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
                    UploadedAt = DateTime.Now.AddDays(-daysSince).AddHours(-5).AddSeconds(rnd.Next(10) * 100),
                };
                dbContext.PikchaImages.Add(img);
                dbContext.SaveChanges();

                for (int j = 0; j < daysSince; j++)
                {
                    var imgv = new ImageView() { PikchaImageViewId = (uint) (img.Id * 100 + j + 1), Date = DateTime.Now.AddDays(-j), Count = (uint)rnd.Next(100), PikchaImage= img };
                    dbContext.ImageViews.Add(imgv);
                }
                dbContext.SaveChanges();

            }

        }


        private void InitDB(UserManager<PikchaUser> userManager, RoleManager<PikchaRole> roleManager, PikchaDbContext dbContext)
        {
            // remove all data if there is
            // clear image views
            var imgvws = dbContext.ImageViews.ToListAsync().Result;
            dbContext.RemoveRange(imgvws);
            dbContext.SaveChanges();

            // clear images
            var imgs = dbContext.PikchaImages.ToListAsync().Result;
            dbContext.RemoveRange(imgs);
            dbContext.SaveChanges();

            // clear users
            var usrs = dbContext.PikchaUsers.ToListAsync().Result;
            dbContext.RemoveRange(usrs);
            dbContext.SaveChanges();


            // create roles
            bool userExist = roleManager.RoleExistsAsync(PikchaConstants.PIKCHA_ROLES_USER_NAME).Result;
            if (!userExist)
            {
                // first we create Admin rool    
                var role = new PikchaRole();
                role.Id = Guid.NewGuid().ToString();
                role.Name = PikchaConstants.PIKCHA_ROLES_USER_NAME;
                var t = roleManager.CreateAsync(role).Result;
            }
            bool photographerExist = roleManager.RoleExistsAsync(PikchaConstants.PIKCHA_ROLES_PHOTOGRAPHER_NAME).Result;
            if (!photographerExist)
            {
                // first we create Admin rool    
                var role = new PikchaRole();
                role.Id = Guid.NewGuid().ToString();
                role.Name = PikchaConstants.PIKCHA_ROLES_PHOTOGRAPHER_NAME;
                var s = roleManager.CreateAsync(role).Result;
            }

            bool adminExist = roleManager.RoleExistsAsync(PikchaConstants.PIKCHA_ROLES_ADMIN_NAME).Result;
            if (!adminExist)
            {
                // first we create Admin rool    
                var role = new PikchaRole();
                role.Id = Guid.NewGuid().ToString();
                role.Name = PikchaConstants.PIKCHA_ROLES_ADMIN_NAME;
                var d = roleManager.CreateAsync(role).Result;
            }

            // create admin user
            var user = new PikchaUser()
            {
                UserName = "PikchaAdmin",
                Email = "admin@pikcha.com",
                BioInfo = "I am the super admin of Pikcha Web",
                FirstName = "Pikcha admin",
                LastName = "Super Admin"
            };
                        //Here we create a Admin super user who will maintain the website                   
           string userPWD = "p1KcAd!N0o7";

           IdentityResult chkUser = userManager.CreateAsync(user, userPWD).Result;

            //Add default User to Role Admin    
            if (chkUser.Succeeded)
            {
                var result1 = userManager.AddToRoleAsync(user, PikchaConstants.PIKCHA_ROLES_ADMIN_NAME).Result;
            }

            //var usert = userManager.FindByEmailAsync("admin@pikcha.com").Result;

            //bool isadm = userManager.IsInRoleAsync(usert, PikchaConstants.PIKCHA_ROLES_ADMIN_NAME).Result;

            //var roles =  userManager.GetRolesAsync(usert).Result;
        }

    }
}

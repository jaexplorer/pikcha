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
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Serilog;

namespace PikchaWebApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins,
                builder =>
                {
                    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                });
            });

            /*services.AddDbContext<PikchaDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection"))); */

            // for mysql db
            // other service configurations go here
            services.AddDbContext<PikchaDbContext>( // replace "YourDbContext" with the class name of your DbContext
                (serviceProvider, options) => options.UseMySql(Configuration.GetConnectionString("DefaultConnection"), // replace with your Connection String
                    mySqlOptions =>
                    {
                        mySqlOptions.ServerVersion(new Version(8, 0, 17), ServerType.MySql); // replace with your Server Version and Type
                    }
            ));

            services.AddIdentity<PikchaUser, IdentityRole>()
                .AddEntityFrameworkStores<PikchaDbContext>()
                .AddDefaultTokenProviders(); ;

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
                       

            // Auto Mapper Configurations
            var mappingConfig = new MapperConfiguration(mc =>
            {

                mc.AddProfile(new PikchaDTOProfiles());
                mc.ForAllMaps((obj, cnfg) => cnfg.ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null)));
            });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            // email
            services.AddTransient<IEmailSender, EmailSender>();
            services.Configure<AuthMessageSenderOptions>(Configuration);

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Identity/Account/Login";
            });

            // password policy
            services.Configure<IdentityOptions>(options =>
            {
                // Default Password settings.
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;
            });

            services.AddHttpClient(); // for external api requests

           
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
            app.UseCors(MyAllowSpecificOrigins);
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

            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<PikchaDbContext>();

                context.Database.Migrate();


                try
                {
                    bool populateDB = true;
                    if (populateDB)
                    {
                        var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<PikchaUser>>();

                        var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                        try
                        {
                            Task intTask = InitDB(userManager, roleManager, context);
                            Task.WaitAll(intTask);
                        }
                        catch (Exception ex)
                        {
                            Log.Error(ex, " Startup, InitDB");

                        }

                        try
                        {
                            Task intTask = SeedData(userManager, env, context);
                            Task.WaitAll(intTask);
                        }
                        catch (Exception ex)
                        {
                            Log.Error(ex, " Startup, SeedData");

                        }
                    }


                }
                catch (Exception ex)
                {
                    Log.Error(ex, " Startup, populateDB");
                }
            }
        }

        private async Task SeedData(UserManager<PikchaUser> userManager, IWebHostEnvironment env, PikchaDbContext dbContext)
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
            var userCount = await userManager.Users.CountAsync();
            if (userCount < 10)
            {
                for (int i = 1; i < 10; i++)
                {
                    string email = "testuser" + i + "@pikcha.com";
                    
                    var exUsr = await userManager.FindByEmailAsync(email);
                    if (exUsr == null)
                    {
                        var user = new PikchaUser()
                        {
                            UserName = "Pikcha-testuser" + i + "@pikcha.com",
                            Email = email,
                            Bio = "test user" + i + " bio info",
                            FName = "test " + i + " FName",
                            LName = "test " + i + " LName",
                            Country = locations[rnd.Next(locations.Count)],
                            Avatar = PikchaConstants.PIKCHA_USER_DEFAULT_AVATAR
                        };
                        var resl = await userManager.CreateAsync(user);
                        lstUsers.Add(user);
                    }
                    else
                    {
                        lstUsers.Add(exUsr);
                    }
                }
            }
            else
            {
                lstUsers = userManager.Users.ToListAsync().Result;
            }

            // add followers 
            for (int i = 1; i < 5; i++)
            {
                var artist = lstUsers[i];
                // make these to artist
                await userManager.AddToRoleAsync(artist, PikchaConstants.PIKCHA_ROLES_ARTIST_NAME);
                int count = rnd.Next(3, 8);
                for (int j = 1; j < count; j++)
                {
                    if (i == j)
                    {
                        continue;
                    }
                    var pkUs = lstUsers[j];
                    dbContext.Followers.Add(new PikchaArtistFollower() { PikchaArtist = artist, PikchaUser = pkUs });

                }
            }
            await dbContext.SaveChangesAsync();

            //Thread.Sleep(1000);

            for (int i = 0; i < imageIds.Count; i++)
            {
                string imgId = imageIds[i];

                int daysSince = rnd.Next(20);

                var img = new PikchaImage()
                {
                    Artist = lstUsers[rnd.Next(lstUsers.Count)],
                    Caption = "Caption " + i,
                    Title = "Title " + i,
                    Location = locations[rnd.Next(locations.Count)],
                    //Id = i +1,
                    Id = imgId,
                    Watermark = "Uploads/Images/Watermarks/" + imgId + ".jpg",
                    Thumbnail = "Uploads/Images/Thumbnail/" + imgId + ".jpg",
                    UploadedAt = DateTime.Now.AddDays(-daysSince).AddHours(-5).AddSeconds(rnd.Next(10) * 100),
                };
                dbContext.PikchaImages.Add(img);
                //dbContext.SaveChanges();

                for (int j = 0; j < daysSince; j++)
                {
                    var imgv = new ImageView() { Date = DateTime.Now.AddDays(-j), Count = rnd.Next(100), PikchaImage = img };
                    dbContext.ImageViews.Add(imgv);
                }
                //dbContext.SaveChanges();

                ImageProduct imgownProd = new ImageProduct()
                {
                    Image = img,
                    IsSale = true,
                    Price = rnd.Next(100, 10000),
                    Type = PikchaConstants.PIKCHA_PRODUCT_TYPE_OWNER,
                    Seller = img.Artist
                };
                dbContext.ImageProducts.Add(imgownProd);

                // set up some resellers
                int count = rnd.Next(0, 5);
                for (int l = 0; l < count; l++)
                {
                    int rndBool = rnd.Next(0, 100);
                    ImageProduct imgPrd = new ImageProduct()
                    {
                        Image = img,
                        IsSale = rndBool <50 ? true : false,
                        Price = rnd.Next(100, 10000),
                        Type = PikchaConstants.PIKCHA_PRODUCT_TYPE_SELLER,
                        Seller = lstUsers[rnd.Next(lstUsers.Count)]
                    };
                    dbContext.ImageProducts.Add(imgPrd);
                }
            }
            await dbContext.SaveChangesAsync();

        }
        private async Task InitDB(UserManager<PikchaUser> userManager, RoleManager<IdentityRole> roleManager, PikchaDbContext dbContext)
        {
            // remove all data if there is

            // clear image views
            var folls = await dbContext.Followers.ToListAsync();
            dbContext.RemoveRange(folls);
            await dbContext.SaveChangesAsync();

            var imPrds = await dbContext.ImageProducts.ToListAsync();
            dbContext.RemoveRange(imPrds);
            await dbContext.SaveChangesAsync();

            // clear image views
            var imgvws = await dbContext.ImageViews.ToListAsync();
            dbContext.RemoveRange(imgvws);
            await dbContext.SaveChangesAsync();

            // clear image tags
            var imgtags = await dbContext.ImageTags.ToListAsync();
            dbContext.RemoveRange(imgtags);
            await dbContext.SaveChangesAsync();

            // clear images
            var imgs = await dbContext.PikchaImages.ToListAsync();
            dbContext.RemoveRange(imgs);
            await dbContext.SaveChangesAsync();

            // clear users
            //var usrs = dbContext.PikchaUsers.ToListAsync().Result;
            //dbContext.RemoveRange(usrs);
            //dbContext.SaveChanges();


            // create roles
            bool userExist = roleManager.RoleExistsAsync(PikchaConstants.PIKCHA_ROLES_USER_NAME).Result;
            if (!userExist)
            {
                // first we create Admin rool    
                var role = new IdentityRole();
                role.Name = PikchaConstants.PIKCHA_ROLES_USER_NAME;
                var t = roleManager.CreateAsync(role).Result;
            }
            bool photographerExist = roleManager.RoleExistsAsync(PikchaConstants.PIKCHA_ROLES_ARTIST_NAME).Result;
            if (!photographerExist)
            {
                // first we create Admin rool    
                var role = new IdentityRole();
                //role.Id = Guid.NewGuid().ToString();
                role.Name = PikchaConstants.PIKCHA_ROLES_ARTIST_NAME;
                var s = roleManager.CreateAsync(role).Result;
            }

            bool adminExist = roleManager.RoleExistsAsync(PikchaConstants.PIKCHA_ROLES_ADMIN_NAME).Result;
            if (!adminExist)
            {
                // first we create Admin rool    
                var role = new IdentityRole();
                //role.Id = Guid.NewGuid().ToString();
                role.Name = PikchaConstants.PIKCHA_ROLES_ADMIN_NAME;
                var d = roleManager.CreateAsync(role).Result;
            }

            string email = "admin@pikcha.com";
            var usr1 = userManager.FindByEmailAsync(email).Result;
            if (usr1 == null)
            {
                // create admin user
                var user = new PikchaUser()
                {
                    UserName = "PikchaAdmin",
                    Email = email,
                    Bio = "I am the super admin of Pikcha Web",
                    FName = "Pikcha admin",
                    LName = "Super Admin",
                    Avatar = PikchaConstants.PIKCHA_USER_DEFAULT_AVATAR
                };
                //Here we create a Admin super user who will maintain the website                   
                string userPWD = "p1KcAd!N0o7";

                IdentityResult chkUser = userManager.CreateAsync(user, userPWD).Result;

                //Add default User to Role Admin    
                if (chkUser.Succeeded)
                {
                    var result1 = userManager.AddToRoleAsync(user, PikchaConstants.PIKCHA_ROLES_ADMIN_NAME).Result;
                }
            }


            //var usert = userManager.FindByEmailAsync("admin@pikcha.com").Result;

            //bool isadm = userManager.IsInRoleAsync(usert, PikchaConstants.PIKCHA_ROLES_ADMIN_NAME).Result;

            //var roles =  userManager.GetRolesAsync(usert).Result;
        }

    }
}

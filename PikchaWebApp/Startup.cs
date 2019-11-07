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

                var _mapper = serviceScope.ServiceProvider.GetRequiredService<IMapper>();

                /*var images = context.PikchaUsers.Include("Images.Views").Include("Images.Artist")
                    .OrderByDescending(u => u.Images.Sum(i => i.Views.Sum(v => v.Count)))
                    .Skip(0).Take(3)
                    //.Select(u => u.Images.OrderByDescending(i => i.Views.Sum(v => v.Count)).First())
                    .SelectMany(u => u.Images.OrderByDescending(i => i.Views.Sum(v => v.Count)).Take(1))
                    //.ToList(); 
                    ;
                var imagesDTOs = _mapper.ProjectTo<PikchaImageFilterDTO>(images).ToList(); */



                try
                {
                    bool populateDB = false;
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
            locations.Add("Angel Falls, Venezuela");
            locations.Add("The Azores, Portugal");
            locations.Add("Banff National Park, Canada");
            locations.Add("Cappadocia, Turkey");
            locations.Add("Halong Bay, Vietnam");
            locations.Add("Jaffna, Sri Lanka");
            
            // images
            string imgsString = @"06210a25-032b-444f-ac10-06e74e8d7806.jpg, 0780ae32-473a-4541-8cfc-b7e04bb81c1a.jpg, 078536b9-dbf2-4729-ae96-09be37917f85.jpg, 07c81590-744d-4c78-abcf-a62111879ca0.jpg, 10d45792-a082-4c9d-adbd-3f0ed0fb85d9.jpg, 188564df-3404-4ea3-acce-b58c762c6008.jpg, 1e67c1c1-59f7-426d-bacc-a7a67a31c303.jpg, 21330b51-3be6-4248-a576-6a5765a3a608.jpg, 269c20e6-301f-4841-90d6-671cd4366982.jpg, 272c1bca-3c24-444d-b337-36720c0a2db4.jpg, 27df668d-fc7c-4889-9b43-04afeaffe37f.jpg, 2b2bbbde-931e-4623-a253-063fe8bee217.jpg, 3df8bf62-150f-48e1-be2f-c7a0aa98a2d9.jpg, 3e6feeeb-c79f-4e9a-a824-fce0fc3f543f.jpg, 410b5504-4751-44cc-bf10-8af96100ec9e.jpg, 4115df83-d061-49fb-8f92-52953b5454d5.jpg, 44068b65-6d3a-4526-8c03-31c7519d8771.jpg, 4a66fa9d-c0db-46ee-842a-e158f0b0583b.jpg, 4a74e53f-4c91-4c3c-b2a5-3e5628c11df2.jpg, 50101612-8b1d-4d44-ac79-eed1427ea2ef.jpg, 531ae856-18d4-46da-8639-0911a3bf4cc2.jpg, 53dc1d0b-6a62-4bfb-bcd5-b1d06b3aeb62.jpg, 594201a6-4041-492e-923b-972fa8e24c59.jpg, 5ac964f0-5176-4466-a42f-e843cd33d2cd.jpg, 60b2c714-4c66-4e95-b22a-00a7c79b64c4.jpg, 61b170cb-da28-4bd9-a6d4-02d0282fb0c4.jpg, 62dda62f-75cd-4870-9c5e-034528c42c57.jpg, 63610115-8537-4dc7-8875-91b6fa12d9fa.jpg, 64947a64-19fb-441b-83df-0ad4760e8f07.jpg, 65b74a59-99ed-445b-bea7-1c09220be661.jpg, 6ed53b48-3fb9-49a1-934b-027deb0a773b.jpg, 71e31572-6450-455b-9394-619c5c1f905c.jpg, 74d81aba-f935-4a1b-8718-e847a2dbfb31.jpg, 751cab53-95b0-4e47-9f04-a6ea878c634d.jpg, 7bc76edb-badd-4d19-aca4-4a5a17ef79c8.jpg, 7d0dc49d-d79e-4683-a50a-84bfa400a85a.jpg, 7df9e08b-2a59-4f1a-a0ac-bb3803e1c1c1.jpg, 80d4c144-77c5-4069-8529-fd111ee56c11.jpg, 837b96eb-9716-4bb2-b075-f44a7aa16dd5.jpg, 84a81045-d7d1-4a43-974b-c01f1f7577ab.jpg, 85afccc5-3dbb-43c1-9acf-eb2e925ffc1e.jpg, 87bb195a-3390-44c1-83f0-c96dc5f5d4a8.jpg, 9f9165af-5d48-4f37-b580-e1ac893b4394.jpg, a5a6b7bf-518f-46ed-91ba-06bdabf20552.jpg, aa47c8a2-937e-4502-9656-50622039703f.jpg, bdcd0e10-4f1c-431c-9e94-7e25a729862e.jpg, c0339f2e-e174-4d3b-af2a-c6be2b78ccf3.jpg, c0e0aab5-20cd-44b3-9a0e-1aca2ad68fb9.jpg, ca10f142-26b5-4cfd-b4e8-31829e1bfe9a.jpg, ca18520a-6565-41a3-8caa-a6b61dba59d8.jpg, cdfb83da-5f7f-45cd-a85c-a60d8771c079.jpg, cf9bc7d6-d89f-445f-bbf7-8ed4579dd203.jpg, db833381-d594-45b4-a541-4a77d0eb3af9.jpg, e64500c6-8b68-4971-b753-f53402de714f.jpg, eb36eb34-f9aa-45f1-b331-1d0cbf08a24c.jpg, ed7f1b48-3832-409c-a70d-54657bc5034b.jpg, ee96cd4f-a1c9-4756-a994-1930753510e8.jpg, f0fd0dfd-59d9-46e9-9264-85ff628d9b88.jpg, f2e58a56-94d4-407c-9ec6-9fa1df9999cc.jpg, f73cf985-456b-4e41-b118-6f462ed31e44.jpg, f7f0f8fe-9200-4dc0-91a9-1aad2cba7c5b.jpg, f97badc1-4aa7-46f9-8195-0138892ea140.jpg";

            imgsString = imgsString.Replace(".jpg", string.Empty);
            imgsString = imgsString.Replace(" ", string.Empty);

            List<string> imageIds = new List<string>();
            imageIds = imgsString.Split(',').ToList();
            /*
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
            */
            List<PikchaUser> lstUsers = new List<PikchaUser>();
            var userCount = await userManager.Users.CountAsync();
            var users = GenerateUsers();

            if (userCount < users.Count)
            {

                for (int i = 1; i < users.Count; i++)
                {
                    string email = "pikcha-testuser" + i + "@pikcha.com";
                    
                    var exUsr = await userManager.FindByEmailAsync(email);
                    if (exUsr == null)
                    {
                        //int addId = rnd.Next(users.Count);
                        var dumUser = users[i];
                        var user = new PikchaUser()
                        {
                            UserName = email,
                            Email = email,
                            Bio = "Hi, I am " + dumUser.FName + "Lorem ipsum dolor sit amet," 
                            + "consectetur adipiscing elit,"
                            + " sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. " ,
                            FName = dumUser.FName,
                            LName = dumUser.LName,
                            City = dumUser.City,
                            Country = dumUser.Country,
                            Avatar = PikchaConstants.PIKCHA_USER_DEFAULT_AVATAR,
                             
                        };
                        var resl = await userManager.CreateAsync(user, "Pk123456");
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
            for (int i = 1; i < users.Count/2; i++)
            {
                var artist = lstUsers[i];
                // make these to artist
                await userManager.AddToRoleAsync(artist, PikchaConstants.PIKCHA_ROLES_ARTIST_NAME);
                int count = rnd.Next(2, users.Count / 2);
                for (int j = 1; j < count; j++)
                {
                    if (i == j)
                    {
                        continue;
                    }
                    var pkUs = lstUsers[j];
                    dbContext.Followers.Add(new ArtistFollower() { Artist = artist, PikchaUser = pkUs });
                }
            }
            await dbContext.SaveChangesAsync();


            //Thread.Sleep(1000);

            List<string> captions = new List<string>();
            captions.Add("Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium, totam rem aperiam, eaque ipsa quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt explicabo. Nemo enim ipsam voluptatem quia voluptas sit aspernatur aut odit aut fugit, sed quia consequuntur magni dolores eos qui ratione voluptatem sequi nesciunt. Neque porro quisquam est, qui dolorem ipsum quia dolor sit amet, consectetur, adipisci velit, sed quia non numquam eius modi tempora incidunt ut labore et dolore magnam aliquam quaerat voluptatem. Ut enim ad minima veniam, quis nostrum exercitationem ullam corporis suscipit laboriosam, nisi ut aliquid ex ea commodi consequatur?");
            captions.Add("But I must explain to you how all this mistaken idea of denouncing pleasure and praising pain was born and I will give you a complete account of the system, and expound the actual teachings of the great explorer of the truth, the master-builder of human happiness. No one rejects, dislikes, or avoids pleasure itself, because it is pleasure, but because those who do not know how to pursue pleasure rationally encounter consequences that are extremely painful. Nor again is there anyone who loves or pursues or desires to obtain pain of itself, because it is pain, but because occasionally circumstances occur in which toil and pain can procure him some great pleasure. To take a trivial example, which of us ever undertakes laborious physical exercise, except to obtain some advantage from it? But who has any right to find fault with a man who chooses to enjoy a pleasure that has no annoying consequences, or one who avoids a pain that produces no resultant pleasure?");
            captions.Add("At vero eos et accusamus et iusto odio dignissimos ducimus qui blanditiis praesentium voluptatum deleniti atque corrupti quos dolores et quas molestias excepturi sint occaecati cupiditate non provident, similique sunt in culpa qui officia deserunt mollitia animi, id est laborum et dolorum fuga. Et harum quidem rerum facilis est et expedita distinctio. Nam libero tempore, cum soluta nobis est eligendi optio cumque nihil impedit quo minus id quod maxime placeat facere possimus, omnis voluptas assumenda est, omnis dolor repellendus. Temporibus autem quibusdam et aut officiis debitis aut rerum necessitatibus saepe eveniet ut et voluptates repudiandae sint et molestiae non recusandae. Itaque earum rerum hic tenetur a sapiente delectus, ut aut reiciendis voluptatibus maiores alias consequatur aut perferendis doloribus asperiores repellat");
            captions.Add("On the other hand, we denounce with righteous indignation and dislike men who are so beguiled and demoralized by the charms of pleasure of the moment, so blinded by desire, that they cannot foresee the pain and trouble that are bound to ensue; and equal blame belongs to those who fail in their duty through weakness of will, which is the same as saying through shrinking from toil and pain. These cases are perfectly simple and easy to distinguish. In a free hour, when our power of choice is untrammelled and when nothing prevents our being able to do what we like best, every pleasure is to be welcomed and every pain avoided. But in certain circumstances and owing to the claims of duty or the obligations of business it will frequently occur that pleasures have to be repudiated and annoyances accepted. The wise man therefore always holds in these matters to this principle of selection: he rejects pleasures to secure other greater pleasures, or else he endures pains to avoid worse pains");
            captions.Add("Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.");
            captions.Add("Contrary to popular belief, Lorem Ipsum is not simply random text. It has roots in a piece of classical Latin literature from 45 BC, making it over 2000 years old. Richard McClintock, a Latin professor at Hampden-Sydney College in Virginia, looked up one of the more obscure Latin words, consectetur, from a Lorem Ipsum passage, and going through the cites of the word in classical literature, discovered the undoubtable source. Lorem Ipsum comes from sections 1.10.32 and 1.10.33 of de Finibus Bonorum et Malorum (The Extremes of Good and Evil) by Cicero, written in 45 BC. This book is a treatise on the theory of ethics, very popular during the Renaissance. The first line of Lorem Ipsum, Lorem ipsum dolor sit amet.., comes from a line in section 1.10.32.");


            List<string> titles = new List<string>();
            titles.Add("The standard Lorem Ipsum passage, used since the 1500s");
            titles.Add("Section 1.10.32 of de Finibus Bonorum et Malorum, written by Cicero in 45 BC");
            titles.Add("1914 translation by H. Rackham");
            titles.Add("From its medieval origins to the digital era");
            titles.Add("Neque porro quisquam est qui dolorem ipsum quia dolor sit amet, consectetur, adipisci velit");

            for (int i = 0; i < imageIds.Count; i++)
            {
                string imgId = imageIds[i];

                int daysSince = rnd.Next(30);

                var date_at = DateTime.Now.AddDays(-daysSince).AddHours(-5).AddSeconds(rnd.Next(10) * 100);
                var img = new PikchaImage()
                {
                    Artist = lstUsers[rnd.Next(lstUsers.Count)],
                    Caption = captions[rnd.Next(captions.Count)],
                    Title = titles[rnd.Next(titles.Count)],
                    Location = locations[rnd.Next(locations.Count)],
                    Id = imgId,
                    Watermark = "Uploads/Images/Watermarks/" + imgId + ".jpg",
                    Thumbnail = "Uploads/Images/Thumbnail/" + imgId + ".jpg",
                    UploadedAt = date_at,
                    ModifiedAt = date_at
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


        private List<UserObj> GenerateUsers()
        {
            List<UserObj> lstAddr = new List<UserObj>()
            {
                new UserObj(){ FName = "Stevie", LName ="Hallo", City = "Melbourne", Country ="Australia"},
                new UserObj(){ FName = "Lenora", LName ="Delacruz",  City = "California", Country = "USA"},
                new UserObj(){ FName = "Aide", LName ="Ghera",  City = "Paris", Country = "France"},
                new UserObj(){ FName = "Florinda", LName ="Gudgel",  City = "London", Country = "UK"},
                new UserObj(){ FName = "Stephaine", LName ="Manin",  City = "Tokyo", Country = "Japan"},
                new UserObj(){ FName = "Barb", LName ="Latina",  City = "Sydney", Country = "Australia"},
                new UserObj(){ FName = "Santos", LName ="Wisenbaker",  City = "Jaffna", Country = "Sri Lanka"},
                new UserObj(){ FName = "King", LName ="Picton",  City = "Toronto", Country = "Canada"},
                new UserObj(){ FName = "Mary", LName ="Dingler",  City = "Warragul", Country = "Australia"},
                new UserObj(){ FName = "Dylan", LName ="Chaleun",  City = "Geelong", Country = "Australia"},
                new UserObj(){ FName = "Tien", LName ="Kinney",  City = "	Columbia", Country = "USA"},
                new UserObj(){ FName = "Jeffrey", LName ="Leuenberger",  City = "Los Angeles", Country = "USA"},
                new UserObj(){ FName = "Ben", LName ="Kellman",  City = "Miami", Country = "USA"},
                new UserObj(){ FName = "Jesus", LName ="Merkt",  City = "San Diego", Country = "USA"},
                new UserObj(){ FName = "Matilda", LName ="Peleg",  City = "Dallas", Country = "USA"},
                new UserObj(){ FName = "Dorian", LName ="Eischens",  City = "Adelaide", Country = "Australia"},
                new UserObj(){ FName = "Ariel", LName ="Stavely",  City = "Perth", Country = "Australia"},
                new UserObj(){ FName = "Nida", LName ="Fitz",  City = "Hobart", Country = "Australia"},
                new UserObj(){ FName = "Alpha", LName ="Prudhomme",  City = "Sunshine Coast", Country = "Australia"},
                new UserObj(){ FName = "Felix", LName ="Bumby",  City = "	Port Macquarie", Country = "Australia"},
            };

            return lstAddr;
        }

        private class UserObj
        {
            public string FName { get; set; }
            public string LName { get; set; }
            public string City { get; set; }
            public string Country { get; set; }
        }

       
    }
}

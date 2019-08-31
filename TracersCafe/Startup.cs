using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TracersCafe.Data;

namespace TracersCafe
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            var model = new Model();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //Destroy previous Db
                model.Database.EnsureDeleted();
                //Ensure the db is created
                model.Database.EnsureCreated();
                //Add test data
                model.Information.AddRange(new List<PersonInformation>()
                {
                    new PersonInformation()
                    {
                        Title = "Mr",
                        Firstname = "John",
                        Surname = "Doe",
                        AddressLineOne = "123 Brad Lane",
                        AddressLineTwo = "Somewhere",
                        AddressLineThree = "Out",
                        AddressLineFour = "There",
                        Age = 25,
                        Postcode = "WS11 2TY",
                        Telephone = 12345678901
                    },

                    new PersonInformation()
                    {
                        Title = "Miss",
                        Firstname = "Jane",
                        Surname = "Doe",
                        AddressLineOne = "123 Brad Lane",
                        AddressLineTwo = "Somewhere",
                        AddressLineThree = "Out",
                        AddressLineFour = "There",
                        Age = 25,
                        Postcode = "WS11 2TY",
                        Telephone = 12345678901
                    }
                });
                model.SaveChanges();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
                //Ensure the db is created
                model.Database.EnsureCreated();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}





//Only took me 5hrs. x)
//Think this was mainly because I had to mess about with Net Core 2.1 not wanting to work then
//setting up and googling stuff I had no clue about.
//Also the fact I did this overnight. Probably that. /s
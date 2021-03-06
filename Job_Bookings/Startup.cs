using Job_Bookings.Services;
using Job_Bookings.Services.Helper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;

namespace Job_Bookings
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
            services.AddControllersWithViews().AddJsonOptions(op => {
                JsonConvert.DefaultSettings = () => new JsonSerializerSettings() { DateFormatString = "yyyy-MM-ddTHH:mm:ss" };
            });
            

            //Singletons
            services.AddSingleton<IRetryPolicy, RetryPolicy>();

            //Repos
            services.AddTransient<ICustomerRepo, CustomerRepo>();
            services.AddTransient<IAppointmentRepo, AppointmentRepo>();
            services.AddTransient<ICustomerRatesRepo, CustomerRatesRepo>();
            services.AddTransient<IUserRepo, UserRepo>();

            //Services
            services.AddTransient<ICustomerService, CustomerService>();
            services.AddTransient<IAppointmentService, AppointmentService>();
            services.AddTransient<ICustomerRatesService, CustomerRatesService>();
            services.AddTransient<IUserService, UserService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
            

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}

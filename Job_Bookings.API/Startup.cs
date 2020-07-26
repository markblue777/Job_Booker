using Job_Bookings.Services;
using Job_Bookings.Services.Helper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

namespace Job_Bookings.API
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
            services.AddControllers().AddJsonOptions(op => {
                JsonConvert.DefaultSettings = () => new JsonSerializerSettings() { DateFormatString = "yyyy-MM-ddTHH:mm:ss" };
            });

            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Job Booker API",
                    Description = "",
                    TermsOfService = new System.Uri("http://www.mandppcs.co.uk/ToS"),
                    Contact = new OpenApiContact { Name = "Support", Email = "support@mandppcs.co.uk", Url = new System.Uri("http://www.mandppcs.co.uk/Support") },
                    License = new OpenApiLicense { Name = "TBD", Url = new System.Uri("http://www.mandppcs.co.uk/Job_Booker_License") }
                });
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
                
                //enable middleware to serve generated swagger as JSON endpoint
                app.UseSwagger();

                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Job Booker API"));
            }

            //need to add middleware for api key connection and ssl pass through

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

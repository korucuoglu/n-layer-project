using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using UdemyNLayerProject.Web.ApiService;
using UdemyNLayerProject.Web.Filters;

namespace UdemyNLayerProject.Web
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
            //services.AddHttpClient<GenericApiService>(opt =>
            //{
            //    opt.BaseAddress = new Uri(Configuration["baseUrl"]);
            //    //Basic authentication gerektiren servisler için gerekli kapatýlabilir.
            //    //opt.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes("username:sifre")));

            //});
          
            services.AddHttpClient("ApiServiceClient", opt =>
            {
                opt.BaseAddress = new Uri(Configuration["baseUrl"].ToString());
            });
            services.AddSingleton(typeof(IApiService<>), typeof(ApiService<>));

            services.AddScoped(typeof(NotFoundFilter<>));

            services.AddControllersWithViews();
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
                    pattern: "{controller=Categories}/{action=Index}/{id?}");
            });
        }
    }
}

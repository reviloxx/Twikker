using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Twikker.Data;
using Twikker.Service;

namespace Twikker.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddMvc();
            services.AddDistributedMemoryCache();
            services.AddSession();
            services.AddSingleton(Configuration);
            services.AddScoped<IComment, CommentService>();
            services.AddScoped<IPost, PostService>();
            services.AddScoped<IUserText, UserTextService>();
            services.AddScoped<IReaction, ReactionService>();
            services.AddScoped<IUser, UserService>();
            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.AddSeq(Configuration.GetSection("Seq"));
            });
            services.AddDbContext<TwikkerContext>(options =>
                  options.UseSqlite(this.Configuration.GetConnectionString("TwikkerConnection")));

            return services.BuildServiceProvider();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseBrowserLink();
            app.UseDeveloperExceptionPage();                       

            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                if (!serviceScope.ServiceProvider.GetService<TwikkerContext>().AllMigrationsApplied())
                {
                    serviceScope.ServiceProvider.GetService<TwikkerContext>().Database.Migrate();
                    serviceScope.ServiceProvider.GetService<TwikkerContext>().EnsureSeeded();
                }
            }

            app.UseStaticFiles();
            app.UseSession();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}");
            });
        }
    }
}

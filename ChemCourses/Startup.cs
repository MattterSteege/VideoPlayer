using BetterPages.utilities;

namespace ChemCourses
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
            services.AddControllersWithViews(); 
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            app.Use(async (context, next) =>
            {
                await next();
                if (context.Response is {StatusCode: 404, HasStarted: false})
                {
                    //log the error
                    loggerFactory.CreateLogger("404").LogWarning($"404 error, user {context.User.FindFirst("email")} requested: \"{context.Request.Path}\", but page wasn't found");
                    
                    context.Response.Redirect("/404");
                }
            });
            
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                app.UseExceptionHandler("/Error");

            app.UseBetterPagesMiddleware("/Dashboard"); //MAKE SURE THIS IS ADDED!
            
            app.UseForwardedHeaders();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(name: "default", pattern: "{controller=BetterPages}/{action=Index}/{id?}");
            });
        }
    }
}